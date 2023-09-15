
using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.ShiftDataMaintain;
using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services
{
    public class ShiftDataMaintainService : IShiftDataMaintainService
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public ShiftDataMaintainService(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<PaginationUtility<MS_Shift>> GetDataPagination(PaginationParam pagination, ShiftDataMaintainParam param)
        {
            var data = await GetData(param);
            return PaginationUtility<MS_Shift>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public Task<List<MS_Shift>> GetData(ShiftDataMaintainParam param)
        {
            var pred = PredicateBuilder.New<MS_Shift>(true);
            if (!string.IsNullOrEmpty(param.Shift))
                pred.And(x => x.Shift.Trim() == param.Shift.Trim());
            if (!string.IsNullOrEmpty(param.ShiftName))
                pred.And(x => x.ShiftName.Trim() == param.ShiftName.Trim());

            var data = _repositoryAccessor.MS_Shift.FindAll(pred).ToListAsync();

            return data;
        }

        public async Task<OperationResult> Create(MS_ShiftDTO dto)
        {
            var item = await _repositoryAccessor.MS_Shift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == dto.Shift.Trim());

            if (item != null)
                return new OperationResult(false, "Data Already Exists");

            var dataCreate = Mapper.Map(dto).ToANew<MS_Shift>(x => x.MapEntityKeys());

            _repositoryAccessor.MS_Shift.Add(dataCreate);
            await _repositoryAccessor.Save();

            return new OperationResult(true);
        }

        public async Task<OperationResult> Update(MS_ShiftDTO dto)
        {
            var item = await _repositoryAccessor.MS_Shift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == dto.Shift);

            if (item == null)
            {
                return new OperationResult(false, "No Data");
            }

            item = Mapper.Map(dto).Over(item);

            _repositoryAccessor.MS_Shift.Update(item);
            await _repositoryAccessor.Save();

            return new OperationResult(true);
        }

        public async Task<OperationResult> Delete(MS_ShiftDTO dto)
        {
            var data = await _repositoryAccessor.MS_Shift.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == dto.Shift);
            if (data == null)
                return new OperationResult { IsSuccess = false };
                
            _repositoryAccessor.MS_Shift.Remove(data);
            return new OperationResult { IsSuccess = await _repositoryAccessor.Save() };
        }

        public async Task<byte[]> DownloadFileExcel(ShiftDataMaintainParam param)
        {
            return ExcelUtility.DownloadExcel(await GetData(param), "Resources\\Template\\Maintain\\1_1_Shift_Data_Maintain\\1_1_Shift_Data_Maintain_Download.xlsx");
        }

        public async Task<OperationResult> UploadFileExcel(ShiftDataMaintainUploadParam param, string userName)
        {
            ExcelResult resp = ExcelUtility.CheckExcel(param.File, "Resources\\Template\\Maintain\\1_1_Shift_Data_Maintain\\1_1_Shift_Data_Maintain_Upload.xlsx");
            if (!resp.IsSuccess)
                return new OperationResult(false, resp.Error);

            List<MS_Shift> dataAdd = new();
            List<MS_Shift> dataUpdate = new();
            List<MS_Shift> dataDelete = new();

            for (int i = resp.wsTemp.Cells.Rows.Count; i < resp.ws.Cells.Rows.Count; i++)
            {
                if (resp.ws.Cells[i, 0].Value == null || resp.ws.Cells[i, 0].StringValue.Length > 1)
                    return new SDCores.OperationResult { IsSuccess = false, Error = $"Model in row {i + 1} invalid" };
                if (resp.ws.Cells[i, 1].Value == null || resp.ws.Cells[i, 0].StringValue.Length > 1)
                    return new SDCores.OperationResult { IsSuccess = false, Error = $"Model in row {i + 1} invalid" };
                if (resp.ws.Cells[i, 2].Value == null || resp.ws.Cells[i, 1].StringValue.Length > 10)
                    return new SDCores.OperationResult { IsSuccess = false, Error = $"Model in row {i + 1} invalid" };
                string state = resp.ws.Cells[i, 3].StringValue.Trim();

                var existingData = await _repositoryAccessor.MS_Shift.FindAll(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == param.Shift)
                                   .AsNoTracking().FirstOrDefaultAsync();

                if (state.ToUpper() == "A")
                {
                    if (await _repositoryAccessor.MS_Shift.AnyAsync(x => x.Manuf.Trim() == "N" && x.Shift.Trim() == param.Shift))
                        return new OperationResult { IsSuccess = false, Error = $"Data in row {i + 1} already exists" };

                    MS_Shift dataNew = new();
                    dataNew.Manuf = resp.ws.Cells[i, 0].StringValue?.Trim();
                    dataNew.Shift = resp.ws.Cells[i, 1].StringValue?.Trim();
                    dataNew.ShiftName = resp.ws.Cells[i, 2].StringValue?.Trim();

                    dataAdd.Add(dataNew);
                }

                else if (state.ToUpper() == "U")
                {
                    if (existingData == null)
                        return new OperationResult { IsSuccess = false, Error = $"Data does not exist" };

                    existingData.ShiftName = resp.ws.Cells[i, 2].StringValue?.Trim();

                    dataUpdate.Add(existingData);
                }
                else if (state.ToUpper() == "D")
                {
                    if (existingData == null)
                        return new OperationResult { IsSuccess = false, Error = $"Data does not exist" };

                    dataDelete.Add(existingData);
                }
            }

            using (var _maintain = await _repositoryAccessor.BeginTransactionAsync())
            {
                try
                {
                    _repositoryAccessor.MS_Shift.AddMultiple(dataAdd);
                    _repositoryAccessor.MS_Shift.UpdateMultiple(dataUpdate);
                    _repositoryAccessor.MS_Shift.RemoveMultiple(dataDelete);

                    await _repositoryAccessor.Save();
                    await _maintain.CommitAsync();

                    string path = @"uploaded/excels/Maintain/1_1_Shift_Data_Maintain/1_1_Shift_Data_Maintain_Create";
                    await FilesUtility.UploadAsync(param.File, path, $"ShiftDataMaintain_{DateTime.Now.ToString("yyyyMMddHHmmss")}"); ;

                    return new OperationResult { IsSuccess = true };
                }
                catch (System.Exception e)
                {
                    await _maintain.RollbackAsync();
                    return new OperationResult { IsSuccess = false, Error = e.ToString() };
                }
            }
        }
    }
}