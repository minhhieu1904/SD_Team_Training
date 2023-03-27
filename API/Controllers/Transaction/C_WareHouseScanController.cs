using API._Services.Interfaces.Transaction;
using API.DTOs.Transaction.WarehouseScan;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Transaction
{
    public class C_WareHouseScanController : APIController
    {
        private readonly I_WareHouseScanService _service;
        private readonly IWebHostEnvironment _environment;
        public C_WareHouseScanController(I_WareHouseScanService service, IWebHostEnvironment environment)
        {
            _environment = environment;
            _service = service;
        }

        [HttpPost("CheckScanCode")]
        public async Task<IActionResult> CheckScanCode(string target)
        {
            return Ok(await _service.CheckScanCode(target));
        }

        [HttpPost("SaveQRStorage")]
        public async Task<IActionResult> SaveQRStorage([FromBody] WarehouseScanDto data)
        {
            data.Current_User = userName;
            return Ok(await _service.SaveQRStorage(data));
        }

        [HttpGet("GetListQRStorage")]
        public async Task<IActionResult> GetListQRStorage([FromQuery] PaginationParam pagiantion, string trNo)
        {
            return Ok(await _service.GetListQRStorage(pagiantion, trNo));
        }

        [HttpPost("ExportExcel")]
        public async Task<IActionResult> ExportExcel(string trNo)
        {
            var data = await _service.GetQRStorageExport(trNo);
            string currentDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string path = Path.Combine(_environment.ContentRootPath, "Resources\\Template\\Transaction\\WarehouseScan\\Export_Report.xlsx");

            WorkbookDesigner designer = new WorkbookDesigner();
            Workbook workbook = new Workbook(path);

            foreach (var item in data)
            {
                var dataWareHouseScan = item.ListWarehouseScan.FirstOrDefault();
                int sheetIndex = workbook.Worksheets.Add();
                Worksheet ws = workbook.Worksheets[sheetIndex];
                ws.Copy(workbook.Worksheets[0]);
                ws.PageSetup.PaperSize = PaperSizeType.PaperA4;

                ws.Cells[3, 0].PutValue("Transac_No: " + dataWareHouseScan.TrNo);
                ws.Cells[3, 2].PutValue("Scan Date: " + dataWareHouseScan.SDat.ToString("yyyy/MM/dd"));
                ws.Cells[4, 0].PutValue("Warehouse: " + dataWareHouseScan.StoreH);
                ws.Cells[4, 1].PutValue("Department: " + dataWareHouseScan.ParNo);
                ws.Cells[4, 2].PutValue("Print Time: " + currentDate);
                ws.Cells[4, 5].PutValue("Page: " + sheetIndex + "/" + data.Count());

                ws.Cells[7, 0].PutValue("Warehouse: ");
                ws.Cells[7, 2].PutValue("Print User: " + userName);
                ws.Cells[7, 5].PutValue("Total: " + item.Qty);

                designer.SetDataSource("result", item.ListWarehouseScan);
                designer.Workbook = workbook;
                designer.Process(sheetIndex, true);
            }
            designer.Workbook.Worksheets.RemoveAt(0);

            MemoryStream stream = new MemoryStream();
            designer.Workbook.Save(stream, SaveFormat.Pdf);
            byte[] result = stream.ToArray();

            return File(result, "application/xlsx");
        }
    }
}