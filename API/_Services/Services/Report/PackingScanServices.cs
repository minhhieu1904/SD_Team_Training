
using API._Repositories;
using API._Services.Interfaces.Report;
using API.DTOs.Report;
using LinqKit;
using SD3_API.Helpers.Utilities;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services.Report
{
    public class PackingScanServices : IPackingScanServices
    {
        private readonly IRepositoryAccessor _repository;

        public PackingScanServices(IRepositoryAccessor repoAccessor)
        {
            _repository = repoAccessor;
        }
   #region ExportExcel - Xuất dữ liệu excel
        public Task<byte[]> ExportExcel(OderParam param)
        {
            
            throw new NotImplementedException();
        }
        private async Task<List<OrderDTO>> GetData(OderParam param)
        {

            var pred = PredicateBuilder.New<MS_QR_Order>(true);
            if (!string.IsNullOrWhiteSpace(param.manuf))
            {
                pred.And(x => x.manuf == param.manuf.Trim());
            }
            if (!string.IsNullOrWhiteSpace(param.mdat_dateFrom) && !string.IsNullOrWhiteSpace(param.mdat_dateTo))
            {
                pred.And(x => x.mdat >= Convert.ToDateTime(param.mdat_dateFrom).Date && x.mdat <= Convert.ToDateTime(param.mdat_dateTo).Date);
            }
             if (param.qty != 0)
            {
                pred.And(x => x.qty == param.qty);
            }
             if (!string.IsNullOrWhiteSpace(param.brandname))
            {
                pred.And(x => x.brandname == param.brandname.Trim());
            }
             if (!string.IsNullOrWhiteSpace(param.cusna))
            {
                pred.And(x => x.cusna == param.cusna.Trim());
            }
            if (!string.IsNullOrWhiteSpace(param.manno))
            {
                pred.And(x => x.manno == param.manno.Trim());
            }
            if (!string.IsNullOrWhiteSpace(param.purno))
            {
                pred.And(x => x.purno == param.purno.Trim());
            }
             if (!string.IsNullOrWhiteSpace(param.rmodel))
            {
                pred.And(x => x.rmodel == param.rmodel.Trim());
            }
            if (!string.IsNullOrWhiteSpace(param.tolcls))
            {
                pred.And(x => x.tolcls == param.tolcls.Trim());
            }
             if (!string.IsNullOrWhiteSpace(param.bitnbr))
            {
                pred.And(x => x.bitnbr == param.bitnbr.Trim());
            }
            if (!string.IsNullOrWhiteSpace(param.kind))
            {
                pred.And(x => x.kind == param.kind.Trim());
            }
            if (!string.IsNullOrWhiteSpace(param.eta_dateFrom) && !string.IsNullOrWhiteSpace(param.eta_dateTo))
            {
                pred.And(x => x.eta >= Convert.ToDateTime(param.eta_dateFrom).Date && x.eta <= Convert.ToDateTime(param.eta_dateTo).Date);
            }
               if (!string.IsNullOrWhiteSpace(param.size))
            {
                pred.And(x => x.size == param.size.Trim());
            }

             var Data_Order = await _repository.MS_QR_Order.FindAll(pred).ToListAsync();
            // var Data_M_CUSTOMS_MATERIAL = await _repository.M_CUSTOMS_MATERIAL.FindAll().ToListAsync();
            // var result = Data_EDI_INBOUND.Join(Data_M_CUSTOMS_MATERIAL,
            //     x => new { x.Factory, x.CMatl_Code },
            //     y => new { y.Factory, y.CMatl_Code },
            //     (x, y) => new { left = x, right = y }
            // ).Select(x => new EDITransactionDto()
            // {
            //     Supplier_Code = x.left.Supplier_Code,
            //     Supplier_Declare_No = x.left.Supplier_Declare_No,
            //     Batch_No = x.left.Batch_No,
            //     Con_Type = x.left.Con_Type,
            //     Declare_No = x.left.Declare_No,
            //     Declare_Date = x.left.Declare_Date,
            //     Declare_Type = x.left.Declare_Type,
            //     Unloading_Location = x.left.Unloading_Location,
            //     Terms_Of_Trade = x.left.Terms_Of_Trade,
            //     CMatl_Code = x.left.CMatl_Code,
            //     CMatl_Name = x.right?.CMatl_Name,
            //     HS_Code = x.left.HS_Code,
            //     Declare_Quantity = x.left.Declare_Quantity,
            //     Declare_Unit_Price = x.left.Declare_Unit_Price,
            //     Amount_USD = x.left.Amount_USD,
            //     Amount_VND = x.left.Amount_VND,
            //     CMatl_Unit = x.left.CMatl_Unit,
            //     Insur_Fee = x.left.Insur_Fee,
            //     Trans_Fee = x.left.Trans_Fee,
            //     Invoice = x.left.Invoice,
            //     Invoice_Date = x.left.Invoice_Date,
            //     Close_State = x.left.Close_State,
            //     Con_No = x.left.Con_No
            // }).ToList();
            return result;
        }
  
    #endregion
        public Task<PaginationUtility<OrderDTO>> LoadData(PaginationParam paginationParams, string ParNo, string ParName)
        {
            throw new NotImplementedException();
        }
    }
}