using System;
using Aspose.Cells;

namespace API.Helper.Utilities
{
    public class ExportExcelUtility<T>
    {
        public void ExportData(List<T> data, string path, Stream stream)
        {
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Workbook = new Workbook(path);
            Worksheet ws = designer.Workbook.Worksheets[0];
            designer.SetDataSource("result", data);
            designer.Process();
            ws.AutoFitColumns();
            ws.PageSetup.CenterHorizontally = true;
            ws.PageSetup.FitToPagesWide = 1;
            ws.PageSetup.FitToPagesTall = 0;
            designer.Workbook.Save(stream, SaveFormat.Xlsx);
        }
    }
}