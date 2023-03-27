import { MS_QR_Order } from "../common/ms_QR_Order";

// List data view
export interface SearchForOrderDataViewModel extends MS_QR_Order {
  available_quantity_for_Print: number;
  isChecked: boolean;
  isDisabled: boolean;
}
// End list data view

// Order data print
export interface OrderDataPrint {
  items: OrderDataItem[];
  packageQty: number;
  printQty: number;
  balance: boolean;
  empno: string;
  isB_grade: boolean;
  userName: string;
  is_Remark: boolean;
}

export interface OrderDataItem {
  purno: string;
  manno: string;
  size: string;
  wkshno: string;
  wkshqty: number;
  prtno: string;
  max_qty_can_print: number;
}
// End order data print

// Order print result
export interface OrderPrintResult extends OrderDataItem {
  rmodel: string;
  serial: number;
  article: string;
  qty: number;
  bitnbr: string;
  custid: string;
  kind: string;
  printDate: string;
  qrCodeName: string;
  endcod: string;
  update_by: string;
  update_time: string;
  remark: string;
}

export interface CompareQtyResult {
  qty: number;
  pqty: number;
  printQty: number;
}
// End order print result
