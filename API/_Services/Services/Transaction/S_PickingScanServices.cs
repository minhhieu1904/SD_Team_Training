using AgileObjects.AgileMapper;
using AgileObjects.AgileMapper.Extensions;
using API._Repositories;
using API._Services.Interfaces.Transaction;
using API.Dtos.Transaction.PickingScan;
using API.Helpers.Params.Transaction;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.Transaction
{
    public class PickingScanService : I_PickingScanServices
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IConfiguration _configuration;
        protected readonly string _manuf;

        public PickingScanService(IRepositoryAccessor repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _manuf = _configuration.GetSection("Appsettings:FactoryCode").Value;
        }

        public async Task<PaginationUtility<PickingMainDto>> GetData(PaginationParam pagination, PickingScanParam filterParam)
        {
            var predMS_QR_PickingMain = PredicateBuilder.New<MS_QR_PickingMain>(x => x.manuf == _manuf);

            if (!string.IsNullOrWhiteSpace(filterParam.DeclarationNo))
                predMS_QR_PickingMain.And(x => x.declaration_no.Trim().ToLower() == filterParam.DeclarationNo.Trim().ToLower());

            if (!string.IsNullOrWhiteSpace(filterParam.Invno))
                predMS_QR_PickingMain.And(x => x.invno.Trim().ToLower() == filterParam.Invno.Trim().ToLower());

            if (!string.IsNullOrWhiteSpace(filterParam.Iono))
                predMS_QR_PickingMain.And(x => x.iono.Trim().ToLower() == filterParam.Iono.Trim().ToLower());

            if (!string.IsNullOrWhiteSpace(filterParam.StartReleaseDate) && !string.IsNullOrWhiteSpace(filterParam.EndReleaseDate))
            {
                var startTime = Convert.ToDateTime(filterParam.StartReleaseDate + " 00:00:00.000");
                var endTime = Convert.ToDateTime(filterParam.EndReleaseDate + " 23:59:59.997");

                predMS_QR_PickingMain.And(x => x.ReleaseDate >= startTime && x.ReleaseDate <= endTime);
            }

            if (!string.IsNullOrWhiteSpace(filterParam.ManNo))
                predMS_QR_PickingMain.And(x => x.manno.Trim().ToLower() == filterParam.ManNo.Trim().ToLower());

            if (!string.IsNullOrWhiteSpace(filterParam.PurNo))
                predMS_QR_PickingMain.And(x => x.purno.Trim().ToLower() == filterParam.PurNo.Trim().ToLower());

            if (!string.IsNullOrWhiteSpace(filterParam.Size))
                predMS_QR_PickingMain.And(x => x.size.Trim().ToLower() == filterParam.Size.Trim().ToLower());

            // Query 
            var query = _repository.MS_QR_PickingMain.FindAll(predMS_QR_PickingMain).Project().To<PickingMainDto>().Distinct();

            return await PaginationUtility<PickingMainDto>.CreateAsync(query, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<GetScanPickingMainDto> GetDataByScan(string QRCodeValue)
        {
            string[] listQRCodeValue = QRCodeValue.Split("_");
            var declarationNo = await GenerateCodeDeclarationNo();
            //main
            var main = new PickingMainDto()
            {
                manuf = _manuf,
                declaration_no = declarationNo,
                status = "N",
                ReleaseDate = null,
            };

            //list detail
            var listDetail = new List<PickingDetailDto>();
            foreach (var QRCode in listQRCodeValue)
            {
                var dataLabel = await _repository.MS_QR_Label.FirstOrDefaultAsync(x => x.QRCodeValue.Trim() == QRCode.Trim() && x.Manuf == _manuf);
                var data = await _repository.MS_QR_Storage.FindAll(x => x.QRCodeID.Trim() == dataLabel.QRCodeID.Trim() && x.manuf == _manuf)
                                .Join(_repository.MS_QR_PickingMain.FindAll(),
                                    x => new { x.manuf, x.manno, x.purno, x.size },
                                    y => new { y.manuf, y.manno, y.purno, y.size },
                                    (x, y) => new { storage = x, pickingMain = y })
                                .GroupJoin(_repository.MS_QR_Label.FindAll(),
                                    x => new { x.storage.manuf, x.storage.QRCodeID },
                                    y => new { manuf = y.Manuf, y.QRCodeID },
                                    (x, y) => new { storage = x.storage, pickingMain = x.pickingMain, label = y })
                                .SelectMany(x => x.label.DefaultIfEmpty(), (x, y) => new { storage = x.storage, pickingMain = x.pickingMain, label = y })
                                .Select(x => new PickingDetailDto()
                                {
                                    manuf = x.storage.manuf,
                                    Sdat = DateTime.Now,
                                    iodat = x.storage.iodat,
                                    iono = x.pickingMain.iono,
                                    declaration_no = x.pickingMain.declaration_no,
                                    storageTrNo = x.storage.trno,
                                    Grade = x.storage.Grade,
                                    QRCodeID = x.storage.QRCodeID,
                                    QRCodeValue = x.label.QRCodeValue,
                                    manno = x.pickingMain.manno,
                                    serial = x.storage.serial,
                                    size = x.pickingMain.size,
                                    qty = x.storage.qty,
                                    crusr = x.storage.crusr,
                                    crday = x.storage.crday,
                                    wkshno = x.storage.wkshno,
                                    prtno = x.storage.prtno,
                                    wkshqty = x.storage.wkshqty,
                                    mdat = x.storage.mdat,
                                    empno = x.storage.empno,
                                    bitnbr = x.storage.bitnbr,
                                    ritnbr = x.storage.ritnbr,
                                    purno = x.pickingMain.purno,
                                }).FirstOrDefaultAsync();
                listDetail.Add(data);
            }

            var result = new GetScanPickingMainDto()
            {
                pickingMain = main,
                listPickingDetail = listDetail,
            };
            return result;
        }



        public async Task<OperationResult> Update(GetScanPickingMainDto listPickingDetail, string userName)
        {
            var pickingMain = await _repository.MS_QR_PickingMain.FirstOrDefaultAsync(x => x.manuf == _manuf
                                                                && x.manno == listPickingDetail.pickingMain.manno
                                                                && x.purno == listPickingDetail.pickingMain.purno
                                                                && x.size == listPickingDetail.pickingMain.size);
            if (pickingMain == null)
                return new OperationResult(false);

            foreach (var item in listPickingDetail.listPickingDetail)
            {
                //remove
                if (item.isDelete == true)
                {
                    var modelCheckDel = await _repository.MS_QR_PickingDetail.FirstOrDefaultAsync(
                        x => x.manuf.Trim() == item.manuf.Trim()
                        && x.iono.Trim() == item.iono.Trim()
                        && x.manno.Trim() == item.manno.Trim()
                        && x.purno.Trim() == item.purno.Trim()
                        && x.size.Trim() == item.size.Trim()
                        && x.QRCodeID.Trim() == item.QRCodeID.Trim()
                        );
                    if (modelCheckDel != null) _repository.MS_QR_PickingDetail.Remove(modelCheckDel);
                }
                else
                {
                    //add new detail
                    var modelCheck = await _repository.MS_QR_PickingDetail.FirstOrDefaultAsync(
                        x => x.manuf.Trim() == item.manuf.Trim()
                        && x.iono.Trim() == item.iono.Trim()
                        && x.manno.Trim() == item.manno.Trim()
                        && x.purno.Trim() == item.purno.Trim()
                        && x.size.Trim() == item.size.Trim()
                        && x.QRCodeID.Trim() == item.QRCodeID.Trim()
                        );
                    if (modelCheck == null)
                    {
                        item.Sdat = DateTime.Now;
                        item.crday = DateTime.Now;
                        item.crusr = userName;
                        var modelAdd = Mapper.Map(item).ToANew<MS_QR_PickingDetail>(x => x.MapEntityKeys());

                        _repository.MS_QR_PickingDetail.Add(modelAdd);
                    }
                }
            }

            // Cập nhật Picking Main
            pickingMain.ActualQTY = listPickingDetail.listPickingDetail.Where(x => !x.isDelete).Select(x => x.qty).Sum();
            _repository.MS_QR_PickingMain.Update(pickingMain);

            await _repository.Save();
            return new OperationResult(true);
        }

        /// <summary>
        /// Lấy PickingDetail.QRCodeID của mỗi PickingMain -> Cập nhật Storage.IsPicking=Y
        /// Cập nhật PickingMain.ReleaseDate=Getdate() && PickingMain.status=Release where iono
        /// </summary>
        /// <param name="releasePickingMain"> Các danh sách có Status: Picking</param>
        /// <returns>Trả về trạng thái cập nhật</returns>
        public async Task<OperationResult> Release(List<PickingMainDto> releasePickingMain, string currentUser)
        {
            using var transaction = await _repository.BeginTransactionAsync();
            foreach (var item in releasePickingMain)
            {
                // Lấy Picking Main
                var pickingMain = await _repository.MS_QR_PickingMain.FirstOrDefaultAsync(x => x.manuf == _manuf &&
                                                                     x.declaration_no.Trim() == item.declaration_no.Trim() &&
                                                                     x.iono == item.iono &&
                                                                     x.manno == item.manno &&
                                                                     x.purno == item.purno &&
                                                                     x.size == item.size
                                                          );

                if (pickingMain == null)
                    return new OperationResult(false);


                var pickingDetails = await _repository.MS_QR_PickingDetail.FindAll(x =>
                          x.manuf == _manuf
                          && x.iono == item.iono
                          && x.manno == item.manno
                          && x.purno == item.purno
                          && x.size == item.size
                        ).ToListAsync();

                bool isUpdateRelease = false;
                foreach (var pickingDetail in pickingDetails)
                {
                    var storegeUpdate = await _repository.MS_QR_Storage.FirstOrDefaultAsync(x => x.QRCodeID == pickingDetail.QRCodeID && x.manuf == _manuf);
                    if (storegeUpdate != null)
                    {
                        if (!isUpdateRelease) isUpdateRelease = true;
                        storegeUpdate.IsPicking = "Y";
                        _repository.MS_QR_Storage.Update(storegeUpdate);
                    }
                }

                if (isUpdateRelease)
                {
                    pickingMain.ReleaseDate = DateTime.Now;
                    pickingMain.updated_by = currentUser;
                    pickingMain.update_time = DateTime.Now;
                    pickingMain.status = "Release";
                    _repository.MS_QR_PickingMain.Update(pickingMain);
                }

            }
            await _repository.Save();
            try
            {
                await transaction.CommitAsync();
                return new OperationResult(true);
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                return new OperationResult(false);
            }
        }

        public async Task<GetScanPickingMainDto> GetDataDetail(PickingScanParam param)
        {
            var pickingMainPredicate = PredicateBuilder.New<MS_QR_PickingMain>(true);
            var pickingDetailPredicate = PredicateBuilder.New<MS_QR_PickingDetail>(true);

            pickingMainPredicate.And(x => x.manuf == _manuf);
            pickingDetailPredicate.And(x => x.manuf == _manuf);

            if (!string.IsNullOrEmpty(param.Iono))
            {
                pickingMainPredicate.And(x => x.iono == param.Iono);
                pickingDetailPredicate.And(x => x.iono == param.Iono);
            }
            if (!string.IsNullOrEmpty(param.ManNo))
            {
                pickingMainPredicate.And(x => x.manno == param.ManNo);
                pickingDetailPredicate.And(x => x.manno == param.ManNo);
            }
            if (!string.IsNullOrEmpty(param.PurNo))
            {
                pickingMainPredicate.And(x => x.purno == param.PurNo);
                pickingDetailPredicate.And(x => x.purno == param.PurNo);
            }
            if (!string.IsNullOrEmpty(param.Size))
            {
                pickingMainPredicate.And(x => x.size == param.Size);
                pickingDetailPredicate.And(x => x.size == param.Size);
            }

            var pickingMain = await _repository.MS_QR_PickingMain.FindAll(pickingMainPredicate).Project().To<PickingMainDto>().FirstOrDefaultAsync();

            // lấy item theo declarationNO và iono
            var pickingDetail = await _repository.MS_QR_PickingDetail.FindAll(pickingDetailPredicate)
                                                .GroupJoin(_repository.MS_QR_PickingMain.FindAll(),
                                                    x => new { x.manuf, x.iono, x.manno, x.purno, x.size },
                                                    y => new { y.manuf, y.iono, y.manno, y.purno, y.size },
                                                    (x, y) => new { pickingDetail = x, pickingMain = y })
                                                    .SelectMany(x => x.pickingMain.DefaultIfEmpty(),
                                                    (x, y) => new { pickingDetail = x.pickingDetail, pickingMain = y })
                                                .GroupJoin(_repository.MS_QR_Storage.FindAll(),
                                                    x => new { x.pickingDetail.manuf, x.pickingDetail.QRCodeID },
                                                    y => new { y.manuf, y.QRCodeID },
                                                    (x, y) => new { pickingDetail = x.pickingDetail, pickingMain = x.pickingMain, storage = y })
                                                    .SelectMany(x => x.storage.DefaultIfEmpty(),
                                                    (x, y) => new { pickingDetail = x.pickingDetail, pickingMain = x.pickingMain, storage = y })
                                                 .GroupJoin(_repository.MS_QR_Label.FindAll(),
                                                    x => new { x.pickingDetail.manuf, x.pickingDetail.QRCodeID },
                                                    y => new { manuf = y.Manuf, y.QRCodeID },
                                                    (x, y) => new { pickingDetail = x.pickingDetail, pickingMain = x.pickingMain, storage = x.storage, label = y })
                                                    .SelectMany(x => x.label.DefaultIfEmpty(),
                                                    (x, y) => new { pickingDetail = x.pickingDetail, pickingMain = x.pickingMain, storage = x.storage, label = y })


                                                .Select(x => new PickingDetailDto()
                                                {
                                                    Sdat = x.pickingDetail.Sdat,
                                                    manuf = x.pickingDetail.manuf,
                                                    declaration_no = x.pickingMain.declaration_no,
                                                    storageTrNo = x.storage.trno,
                                                    iono = x.pickingDetail.iono,
                                                    Grade = x.pickingDetail.Grade,
                                                    QRCodeID = x.pickingDetail.QRCodeID,
                                                    QRCodeValue = x.label.QRCodeValue,
                                                    manno = x.pickingDetail.manno,
                                                    purno = x.pickingDetail.purno,
                                                    serial = x.pickingDetail.serial,
                                                    size = x.pickingDetail.size,
                                                    qty = x.pickingDetail.qty,

                                                    wkshno = x.pickingDetail.wkshno,
                                                    prtno = x.pickingDetail.prtno,
                                                    wkshqty = x.pickingDetail.wkshqty,
                                                    mdat = x.pickingDetail.mdat,
                                                    empno = x.pickingDetail.empno,
                                                    bitnbr = x.pickingDetail.bitnbr,
                                                    ritnbr = x.pickingDetail.ritnbr,
                                                })
                                                .ToListAsync();

            var result = new GetScanPickingMainDto()
            {
                pickingMain = pickingMain,
                listPickingDetail = pickingDetail
            };

            return result;
        }
        public async Task<List<DataExportExcel>> ExportExcel(List<PickingMainDto> dataPickingMain)
        {
            // lấy danh sách sheets
            var sheets = dataPickingMain.GroupBy(x => x.iono);

            var result = new List<DataExportExcel>() { };
            foreach (var sheet in sheets)
            {
                var tabPage = new DataExportExcel() { };
                tabPage.sheetName = sheet.Key.Trim(); // Set tên SheetName
                tabPage.listPickingDetail = new List<ScanPickingExport>();

                var pickingMains = sheet.Select(data => data).ToList();

                foreach (var pickingMain in pickingMains)
                {
                    var pickingDetailPredicate = PredicateBuilder.New<MS_QR_PickingDetail>(true);
                    pickingDetailPredicate.And(x => x.manuf.Trim().ToLower() == pickingMain.manuf.Trim().ToLower()
                                                && x.iono.Trim().ToLower() == pickingMain.iono.Trim().ToLower()
                                                && x.manno.Trim().ToLower() == pickingMain.manno.Trim().ToLower()
                                                && x.purno.Trim().ToLower() == pickingMain.purno.Trim().ToLower()
                                                && x.size.Trim().ToLower() == pickingMain.size.Trim().ToLower());

                    // lấy danh sách pickingDetail theo Picking Main
                    var pickingDetail = await _repository.MS_QR_PickingDetail.FindAll(pickingDetailPredicate)
                        .GroupJoin(_repository.MS_QR_PickingMain.FindAll(x => x.status != "N"),
                                            x => new { x.manuf, x.iono, x.manno, x.purno, x.size },
                                            y => new { y.manuf, y.iono, y.manno, y.purno, y.size },
                                        (x, y) => new { pickingDetail = x, pickingMain = y })
                                        .SelectMany(x => x.pickingMain.DefaultIfEmpty(),
                                        (x, y) => new { pickingDetail = x.pickingDetail, pickingMain = y })
                        .GroupJoin(_repository.MS_QR_Storage.FindAll(),

                                            x => new { x.pickingDetail.manuf, x.pickingDetail.QRCodeID },
                                            y => new { y.manuf, y.QRCodeID },
                                        (x, y) => new { pickingDetail = x.pickingDetail, pickingMain = x.pickingMain, storage = y })
                                        .SelectMany(x => x.storage.DefaultIfEmpty(),
                                        (x, y) => new { pickingDetail = x.pickingDetail, pickingMain = x.pickingMain, storage = y })
                        .Select(x => new ScanPickingExport()
                        {
                            manuf = x.pickingDetail.manuf,
                            Sdat = x.pickingDetail.Sdat,
                            ReleaseDate = x.pickingMain.ReleaseDate.HasValue ? x.pickingMain.ReleaseDate.Value.ToString("yyyy/MM/dd") : string.Empty,
                            declaration_no = x.pickingMain.declaration_no,
                            invno = x.pickingMain.invno,
                            iono = x.pickingDetail.iono,
                            Grade = x.pickingDetail.Grade,
                            QRCodeID = x.pickingDetail.QRCodeID,
                            manno = x.pickingDetail.manno,
                            purno = x.pickingDetail.purno,
                            serial = x.pickingDetail.serial,
                            size = x.pickingDetail.size,
                            qty = x.pickingDetail.qty,
                            crusr = x.pickingDetail.crusr,
                            crday = x.pickingDetail.crday,
                            wkshno = x.pickingDetail.wkshno,
                            prtno = x.pickingDetail.prtno,
                            wkshqty = x.pickingDetail.wkshqty,
                            mdat = x.pickingDetail.mdat,
                            empno = x.pickingDetail.empno,
                            ritnbr = x.storage.ritnbr,
                            bitnbr = x.storage.bitnbr
                        })
                        .ToListAsync();
                    tabPage.listPickingDetail.AddRange(pickingDetail);
                };
                result.Add(tabPage);
            }
            return result;
        }

        public async Task<string> GenerateCodeDeclarationNo()
        {
            var declarationNo = "P" + DateTime.Now.ToString("yyyyMMdd");
            var mS_QR_PickingMain = await _repository.MS_QR_PickingMain.FindAll(x => x.declaration_no.Contains(declarationNo) && x.manuf == _manuf).OrderByDescending(x => x.ReleaseDate).FirstOrDefaultAsync();
            if (mS_QR_PickingMain != null)
            {
                string newString = mS_QR_PickingMain.declaration_no.Substring(mS_QR_PickingMain.declaration_no.Length - 3, 3);
                declarationNo = declarationNo + (Convert.ToInt32(newString) + 1).ToString().PadLeft(3, '0');
            }
            else
            {
                declarationNo = declarationNo + "001";
            }
            return declarationNo;
        }


        /// <summary>
        /// Chức năng [Picking]
        /// Thêm mới vào bảng [MS_QR_PickingDetail]
        /// Cập nhật: ActualQTY, status [MS_QR_PickingMain]
        /// </summary>
        /// <param name="data"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<OperationResult> UpdatePickingQrCode(GetScanPickingMainDto data, PickingScanParam param)
        {
            var pickingMain = await _repository.MS_QR_PickingMain.FindAll(x => x.manuf == _manuf
                                                            && x.iono.Trim().ToLower() == param.Iono.Trim().ToLower()
                                                            && x.manno.Trim().ToLower() == param.ManNo.Trim().ToLower()
                                                            && x.purno.Trim().ToLower() == param.PurNo.Trim().ToLower()
                                                            && x.size.Trim().ToLower() == param.Size.Trim().ToLower()).FirstOrDefaultAsync();
            if (pickingMain == null)
                return new OperationResult(false, "NoData");

            // Kiểm tra số lượng hàng thực tế <= số lượng hàng dự kiến
            decimal totalActualQTY = data.listPickingDetail.Select(x => x.qty).Sum();
            if (totalActualQTY > pickingMain.ExpectQTY)
                return new OperationResult(false, "ActualQtyCanNotThanExpectQty");

            // Cập nhật PickingMain
            pickingMain.ActualQTY = data.listPickingDetail.Select(x => x.qty).Sum();
            pickingMain.status = "Picking";

            var pickingDetails = new List<MS_QR_PickingDetail>();
            foreach (var pickingDetailDto in data.listPickingDetail)
            {
                if (pickingDetailDto.iono.Trim().ToLower() == param.Iono.Trim().ToLower() &&
                    pickingDetailDto.manno.Trim().ToLower() == param.ManNo.Trim().ToLower() &&
                    pickingDetailDto.purno.Trim().ToLower() == param.PurNo.Trim().ToLower() &&
                    pickingDetailDto.size.Trim().ToLower() == param.Size.Trim().ToLower())
                {
                    var checkPickingDetail = await _repository.MS_QR_PickingDetail.FirstOrDefaultAsync(x =>
                                           x.manuf.Trim().ToLower() == pickingDetailDto.manuf.Trim().ToLower() &&
                                           x.iono.Trim().ToLower() == pickingDetailDto.iono.Trim().ToLower() &&
                                           x.manno.Trim().ToLower() == pickingDetailDto.manno.Trim().ToLower() &&
                                           x.purno.Trim().ToLower() == pickingDetailDto.purno.Trim().ToLower() &&
                                           x.size.Trim().ToLower() == pickingDetailDto.size.Trim().ToLower()
                                       );

                    if (checkPickingDetail == null)
                    {
                        pickingDetailDto.Sdat = DateTime.Now;
                        pickingDetailDto.crday = DateTime.Now;
                        pickingDetailDto.crusr = param.CurrentUser;

                        // Nếu không trùng
                        pickingDetails.Add(Mapper.Map(pickingDetailDto).ToANew<MS_QR_PickingDetail>(x => x.MapEntityKeys()));
                    }
                }
            }

            if (!pickingDetails.Any())
                return new OperationResult(false, "NoData");

            _repository.MS_QR_PickingDetail.AddMultiple(pickingDetails);
            _repository.MS_QR_PickingMain.Update(pickingMain);

            await _repository.Save();
            return new OperationResult(true);
        }

        /// <summary>
        /// Khi Quét Code, Kiểm tra dữ liệu trùng khớp với dữ liệu param hay không ?
        /// </summary>
        /// <param name="param"></param>
        /// <param name="scanCode"></param>
        /// <returns></returns>
        public async Task<OperationResult> CheckPickingScanCode(PickingScanParam param, string scanCode)
        {

            var item = scanCode.Split(',');
            string manNo = item[0];
            string purNo = item[1];
            string size = item[2];
            short serial = short.Parse(item[4]);

            // Kiểm  tra với Param xem hợp lệ hay không ?
            if (manNo.Trim() != param.ManNo.Trim() ||
                purNo.Trim() != param.PurNo.Trim() ||
                size.Trim() != param.Size.Trim()
            ) return new OperationResult(false, "QRCodeInvalid"); // Mã code không hợp lệ

            var predicateLabel = PredicateBuilder.New<MS_QR_Label>(true);
            predicateLabel.And(x => x.Manuf.Trim() == _manuf.Trim());
            if (!string.IsNullOrWhiteSpace(manNo)) predicateLabel.And(x => x.ManNo.Trim() == manNo.Trim());
            if (!string.IsNullOrWhiteSpace(purNo)) predicateLabel.And(x => x.PurNo.Trim() == purNo.Trim());
            if (!string.IsNullOrWhiteSpace(size)) predicateLabel.And(x => x.Size.Trim() == size.Trim());
            if (serial > 0) predicateLabel.And(x => x.Serial == serial);

            var labelItem = await _repository.MS_QR_Label.FindAll(predicateLabel).FirstOrDefaultAsync();
            if (labelItem != null)
            {
                var storage = await _repository.MS_QR_Storage.FirstOrDefaultAsync(x => x.QRCodeID == labelItem.QRCodeID && x.manuf == _manuf);

                if (storage?.IsStorageOut == "Y")
                    return new OperationResult(false, "QRCODEAlreadyStorageOut");

                var data_PickingDetail = await _repository.MS_QR_PickingDetail.FirstOrDefaultAsync(x => x.QRCodeID == labelItem.QRCodeID && x.manuf == _manuf);
                if (data_PickingDetail != null)
                    return new OperationResult(false, "29AlreadyPickingQRCODE");

                // Nếu trạng thái đã được chọn  -> Đã phát hành
                if (storage?.IsPicking == "Y")
                    return new OperationResult(false, "QRCODEAlreadyRelease");

                var checkData = await GetDataByScanCode(param, scanCode);
                if (checkData.listPickingDetail[0] == null)
                    return new OperationResult(false, "NoData");

                return new OperationResult(true, labelItem.QRCodeID as object);
            }
            else return new OperationResult(false);

        }

        /// <summary>
        /// Lấy danh sách theo điều kiện Manno, Purno, Size
        /// </summary>
        /// <param name="param">Manno, Purno, Size</param>
        /// <param name="scanCodes">mã code quét được</param>
        /// <returns></returns>
        public async Task<GetScanPickingMainDto> GetDataByScanCode(PickingScanParam param, string scanCodes)
        {
            string[] listQRCodeValue = scanCodes.Split("_");
            var declarationNo = await GenerateCodeDeclarationNo();

            //main
            var main = new PickingMainDto()
            {
                manuf = _manuf,
                declaration_no = declarationNo,
                status = "N",
                ReleaseDate = null,
            };

            //list detail
            var pickingDetails = new List<PickingDetailDto>();
            foreach (var QRCode in listQRCodeValue)
            {
                var qrLabel = await _repository.MS_QR_Label.FirstOrDefaultAsync(x => x.QRCodeValue.Trim() == QRCode.Trim() && x.Manuf == _manuf);
                var pickingDetail = await _repository.MS_QR_Storage
                                .FindAll(x => x.QRCodeID.Trim() == qrLabel.QRCodeID.Trim()
                                                && x.manuf == _manuf
                                                && x.manno == param.ManNo
                                                && x.purno == param.PurNo
                                                && x.size == param.Size)
                                .Join(_repository.MS_QR_PickingMain.FindAll(),
                                    x => new { x.manuf, x.manno, x.purno, x.size },
                                    y => new { y.manuf, y.manno, y.purno, y.size },
                                    (x, y) => new { storage = x, pickingMain = y })
                                .GroupJoin(_repository.MS_QR_Label.FindAll(),
                                    x => new { x.storage.manuf, x.storage.QRCodeID },
                                    y => new { manuf = y.Manuf, y.QRCodeID },
                                    (x, y) => new { storage = x.storage, pickingMain = x.pickingMain, label = y })
                                .SelectMany(x => x.label.DefaultIfEmpty(),
                                    (x, y) => new { storage = x.storage, pickingMain = x.pickingMain, label = y })
                                    .Select(x => new PickingDetailDto()
                                    {
                                        manuf = x.storage.manuf,
                                        Sdat = DateTime.Now,
                                        iodat = x.storage.iodat,
                                        iono = x.pickingMain.iono,
                                        declaration_no = x.pickingMain.declaration_no,
                                        storageTrNo = x.storage.trno,
                                        Grade = x.storage.Grade,
                                        QRCodeID = x.storage.QRCodeID,
                                        QRCodeValue = x.label.QRCodeValue,
                                        manno = x.pickingMain.manno,
                                        serial = x.storage.serial,
                                        size = x.pickingMain.size,
                                        qty = x.storage.qty,
                                        crusr = x.storage.crusr,
                                        crday = x.storage.crday,
                                        wkshno = x.storage.wkshno,
                                        prtno = x.storage.prtno,
                                        wkshqty = x.storage.wkshqty,
                                        mdat = x.storage.mdat,
                                        empno = x.storage.empno,
                                        bitnbr = x.storage.bitnbr,
                                        ritnbr = x.storage.ritnbr,
                                        purno = x.pickingMain.purno,
                                    }).FirstOrDefaultAsync();

                if (pickingDetail != null)
                    pickingDetails.Add(pickingDetail);
            }

            var result = new GetScanPickingMainDto()
            {
                pickingMain = main,
                listPickingDetail = pickingDetails,
            };
            return result;
        }

    }
}