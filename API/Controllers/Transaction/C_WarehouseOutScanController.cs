using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces.transaction;
using API.Helpers.Params.Transaction;
using API.Models;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Transaction
{
    public class C_WarehouseOutScanController : APIController
    {
        private readonly I_WarehouseOutScanService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public C_WarehouseOutScanController(I_WarehouseOutScanService service, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _service = service;
        }

        [HttpGet("GetDataMain")]
        public async Task<IActionResult> GetDataMain([FromQuery] PaginationParam pagination, [FromQuery] WarehouseOutScanParam param)
        {
            var data = await _service.GetDataMain(pagination, param);
            return Ok(data);
        }

        [HttpGet("GetDataPickingMain")]
        public async Task<IActionResult> GetDataPickingMain([FromQuery] PaginationParam pagination, [FromQuery] PickingScanItemParam param)
        {
            var data = await _service.GetPickingFromMainWarehouse(pagination, param);
            return Ok(data);
        }

        [HttpPut("StorageOut")]
        public async Task<IActionResult> StorageOut([FromBody] List<MS_QR_PickingMain> listStorageOut)
        {
            var data = await _service.StorageOut(listStorageOut, userName);
            return Ok();
        }

        [HttpPost("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromBody] List<MS_QR_PickingMain> exportData)
        {
            var data = await _service.ExportExcel(exportData);
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, @"Resources\Template\Transaction\WarehouseOutScan\2.10StorageOutExport.xlsx");
            var designer = new WorkbookDesigner();
            Workbook workbook = new Workbook(path);
            foreach (var item in data)
            {
                int sheetIndex = workbook.Worksheets.Add();
                Worksheet ws = workbook.Worksheets[sheetIndex];
                ws.Name = item.iono;
                ws.Copy(workbook.Worksheets[0]);
                ws.Cells[0, 1].PutValue(userName);
                ws.Cells[1, 1].PutValue(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                designer.SetDataSource("result", item.listData);
                designer.Workbook = workbook;
                designer.Process(sheetIndex, true);
            }
            designer.Workbook.Worksheets.RemoveAt(0);
            workbook.Worksheets.ActiveSheetIndex = 0;
            var stream = new MemoryStream();
            designer.Workbook.Save(stream, SaveFormat.Xlsx);
            byte[] result = stream.ToArray();
            return File(result, "application/xlsx", $"2.10StorageOutExport_{DateTime.Now.ToString("yyyy/MM/dd HH:mm")}.xlsx");
        }
    }
}