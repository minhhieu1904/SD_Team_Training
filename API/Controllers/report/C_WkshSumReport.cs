using API._Services.Interfaces.report;
using API.DTOs.report;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.report
{
    public class C_WkshSumReport : APIController
    {
       private I_WkshSumReport _services;
       private readonly IWebHostEnvironment _webHostEnvironment; 
        public C_WkshSumReport ( I_WkshSumReport services,IWebHostEnvironment webHostEnvironment )
        {
            _services = services;
            _webHostEnvironment = webHostEnvironment;
        } 
        [HttpGet("getData")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination ,[FromQuery] WkshSumReport param,bool isPaging = true)
        {
            var result = await _services.GetData(pagination,param,isPaging);
            return Ok(result);
        }
        [HttpGet("getBrand")]
        public async Task<IActionResult> GetBrand()
        {
            var result = await _services.GetBrand();
            return Ok(result);
        }
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery]PaginationParam pagination ,[FromQuery] WkshSumReport param)
        {

            var data = await _services.GetData(pagination,param,false);
            MemoryStream stream = new MemoryStream();
            if(data.Result.Count > 0){
                // Lấy đường dẫn file excel
                var path = Path.Combine(_webHostEnvironment.ContentRootPath,
                "Resources\\Template\\Development_Orders.xlsx");
                // Khỏi tạo biến kiểu dữ liệu WorkbookDesigner dùng xử lý Excel
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                // Tạo Sheet
                Worksheet ws = designer.Workbook.Worksheets[0];
                // Lấy giá trị lưu vào Excel
                var abc = data.Result;
                designer.SetDataSource("result", data.Result);
                designer.Process();
                designer.Workbook.Save(stream, SaveFormat.Xlsx);
                
                // Câu lênh format excel - Luôn để sau phần save => có dữ liệu thì mới format đc
                // Giusp Wrap text => wrap text là  gì ??? là khi dữ liệu dài hơn ô thì nó sẽ bị ẩn đi - wrap text giúp hiển thị ra hết
                ws.AutoFitColumns();
                // Căn giữa ô
                ws.PageSetup.CenterHorizontally = true;
                // FitToPagesWide Wrap text chiều ngang, FitToPagesTall Wrap text chiều dọc - 0 1 như true false có hoặc k
                // Vd ở đây dữ liệu sẽ Wrap text theo chiều ngang - chiều ngang sẽ tự chỉnh để hiển thị hết dữ liệu
                ws.PageSetup.FitToPagesWide = 1;
                ws.PageSetup.FitToPagesTall = 0;
            }
            // Hiểu nôm na là cho dễ chuyển đi , kiểu nguyên file excel thì nó k đem lên trên đc 
            byte[] result = stream.ToArray();
            return File(result," application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy-HH_mm_SS")}.xlsx");
        }
    }
}