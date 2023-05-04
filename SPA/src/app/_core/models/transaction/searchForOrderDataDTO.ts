export interface SearchForOrderDataDTO {
  brandname: string;
  rmodel: string;
  bitnbr: string;
  size: string;
  cusid: string;
  manno: string;
  purno: string;
  endcod: string;
  wkshno: string;
  cusna: string;
  kind: string;
  eta: string ;
  article: string;
  qty: number;
  wkshqty: number;
  addqty: number;
  lessqty: number;
  apqty: number;
  pqty: number;
  cqty: number;
  ritnbr: string;
  style: string ;
  tsize: string;
  remark: string ;
  prtno: string;
  update_time: string;
  updated_by: string;
  avaiablePqty: number;
  isChecked: boolean;
  isDisabled: boolean;
}
export interface OrderDataPrint {
  items: OrderDataItem[];
  packageQty: number;
  printQty: number;
  balance: boolean;
  empno: string;
  isB_grade: boolean;
  grade: string;
  userName: string;
  remark: boolean;
}

export interface OrderDataItem {
  purno: string;
  manno: string;
  size: string;
  wkshno: string;
  wkshqty: number;
  prtno: string;
  maxPqty: number;
}

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
  remark :string;
}

export interface CompareQtyResult {
  qty: number;
  pqty: number;
  printQty: number;
}