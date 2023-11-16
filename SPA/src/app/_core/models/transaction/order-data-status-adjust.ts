import { PaginationResult } from "../../utilities/pagination-utility";

export interface OrderDataStatusAdjust {
  brand: string;
  moldNumber: string;
  tolcls: string;
  materialCodeBottom: string;
  size: string;
  codeOfCustomer: string;
  planningNo: string;
  purchaseNo: string;
  status: string;
  wkshNo: string;
  nameOfCustomer: string;
  typeOfOrder: string;
  confirmedDeliveryDate: string | null;
  article: string;
  orderQuantity: number;
  wkshQty: number;
  increaseOrderQuantity: number;
  reduceOrderQuantity: number;
  availableQuantityForPrint: number;
  printedQuantity: number;
  cancelPritingQuantity: number;
  materialCodeShoes: string;
  modelName: string;
  toolingSize: string;
  remark: string;
  timeOfUpdating: string;
  updatedBy: string;
  prtno: string; // khoá chính

  // Checked
  isChecked: boolean; // nút được chọn
  isDisabledReOpen: boolean; // [True] không được phép chọn ReOpen
}

export interface OrderDataStatusAdjustResponse {
  allData: OrderDataStatusAdjust[];
  pagination: PaginationResult<OrderDataStatusAdjust>;
}

export interface OrderDataStatusAdjustParam {
  planningNo: string;
  purno: string;
  tolcls: string;
}

export interface OrderDataStatusAdjustUpdate {
  endCod: string;
  orderDataStatusAdjusts: OrderDataStatusAdjust[];
}
