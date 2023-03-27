using API._Services.Interfaces.Transaction;
using API.Dtos.Transaction.PickingScan;
using API.Helpers.Params.Transaction;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Transaction
{
    public class C_PickingScanController : APIController
    {
        private readonly I_PickingScanServices _services;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public C_PickingScanController(I_PickingScanServices services, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _services = services;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetDataPickingScan([FromQuery] PaginationParam pagination, [FromQuery] PickingScanParam filterParam)
        {
            var data = await _services.GetData(pagination, filterParam);
            return Ok(data);
        }

        [HttpGet("GetDataByScan")]
        public async Task<IActionResult> GetDataByScan([FromQuery] string QRCodeValue)
        {
            var data = await _services.GetDataByScan(QRCodeValue);
            return Ok(data);
        }

        [HttpGet("GetDataDetail")]
        public async Task<IActionResult> GetDataDetail([FromQuery] PickingScanParam param)
        {
            var data = await _services.GetDataDetail(param);
            return Ok(data);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] GetScanPickingMainDto listPickingDetail)
        {
            var data = await _services.Update(listPickingDetail, userName);
            return Ok(data);
        }

        [HttpPut("Release")]
        public async Task<IActionResult> Release([FromBody] List<PickingMainDto> releasePickingMan)
        {
            var data = await _services.Release(releasePickingMan, userName);
            return Ok(data);
        }

        [HttpPost("UpdatePickingQrCode")]
        public async Task<IActionResult> UpdatePickingQrCode([FromBody] PickingUpdate picking)
        {
            picking.param.CurrentUser = userName;
            var data = await _services.UpdatePickingQrCode(picking.data, picking.param);
            return Ok(data);
        }

        [HttpPost("CheckPickingScanCode")]
        public async Task<IActionResult> CheckPickingScanCode([FromBody] PickingScanParam param, string scanCode)
        {
            var data = await _services.CheckPickingScanCode(param, scanCode);
            return Ok(data);
        }

        [HttpPost("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromBody] List<PickingMainDto> releasePickingMan)
        {
            var data = await _services.ExportExcel(releasePickingMan);

            var path = Path.Combine(_webHostEnvironment.ContentRootPath, @"Resources\Template\Transaction\PickingScan\ScanPickingTemplate.xlsx");
            var designer = new WorkbookDesigner();
            designer.Workbook = new Workbook(path);
            int tab = 0;
            foreach (var item in data)
            {
                var ws = designer.Workbook.Worksheets[tab];
                ws.Name = item.sheetName;

                //title
                ws.Cells[0, 1].PutValue(userName);
                ws.Cells[1, 1].PutValue(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));


                var index = 3;
                foreach (var i in item.listPickingDetail)
                {
                    ws.Cells[index, 0].PutValue(i.manuf);
                    ws.Cells[index, 1].PutValue(i.Sdat.ToString("yyyy/MM/dd"));
                    ws.Cells[index, 2].PutValue(i.ReleaseDate);
                    ws.Cells[index, 3].PutValue(i.declaration_no);
                    ws.Cells[index, 4].PutValue(i.invno);
                    ws.Cells[index, 5].PutValue(i.iono);
                    ws.Cells[index, 6].PutValue(i.Grade);
                    ws.Cells[index, 7].PutValue(i.QRCodeID);
                    ws.Cells[index, 8].PutValue(i.manno);
                    ws.Cells[index, 9].PutValue(i.purno);
                    ws.Cells[index, 10].PutValue(i.serial);
                    ws.Cells[index, 11].PutValue(i.size);
                    ws.Cells[index, 12].PutValue(i.qty);
                    ws.Cells[index, 13].PutValue(i.crusr);
                    ws.Cells[index, 14].PutValue(i.crday?.ToString("yyyy/MM/dd"));
                    ws.Cells[index, 15].PutValue(i.wkshno);
                    ws.Cells[index, 16].PutValue(i.prtno);
                    ws.Cells[index, 17].PutValue(i.wkshqty);
                    ws.Cells[index, 18].PutValue(i.mdat.ToString("yyyy/MM/dd"));
                    ws.Cells[index, 19].PutValue(i.empno);
                    ws.Cells[index, 20].PutValue(i.ritnbr);
                    ws.Cells[index, 21].PutValue(i.bitnbr);

                    index++;
                }

                //add new sheet
                if (tab < data.Count - 1)
                    tab = designer.Workbook.Worksheets.Add();
            }

            var stream = new MemoryStream();
            designer.Workbook.Save(stream, SaveFormat.Xlsx);
            string fileKind = "application/xlsx";
            string fileExtension = ".xlsx";
            byte[] result = stream.ToArray();

            return File(result, fileKind, "PickingScan" + DateTime.Now.ToString("dd_MM_yyyy") + fileExtension);
        }
    }
}