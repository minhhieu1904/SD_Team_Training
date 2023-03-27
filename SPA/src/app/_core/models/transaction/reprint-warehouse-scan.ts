export interface ReprintWarehouseScan {
  sDat: string;
  trNo: string;
  storageQty: number;
  storeH: string;
  location: string;
  parNo: string;
  department: string;
  manNo: string;
  purNo: string;
  size: string;
  serial: number;
  pQty: number;
  wkshNo: string;
  wkshQty: number;
  prtNo: string;
  mDat: string;
  bitNbr: string;
  rModel: string;
  check: boolean;
}

export interface ReprintWarehouseScanParam {
  dateFrom: string;
  dateTo: string;
  trno: string;
  location: string;
  department: string;
  manno: string;
  purno: string;
  size: string;
}
