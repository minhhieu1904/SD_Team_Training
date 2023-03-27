export interface ReprintPackingScanParam {
  startDate: string;
  endDate: string;
  trno: string;
  shift: string;
  manNo: string;
  purNo: string;
  size: string;
}

export interface ReprintPackingScanModel {
  sDat: string;
  trNo: string;
  sortQty: number;
  shiftName: string;
  manno: string;
  purno: string;
  size: string;
  serial: number;
  pqty: number;
  wkshno: string;
  wkshqty: number;
  prtno: string;
  rmodel: string;
  bitnbr: string;
  shift: string;
  crUsr: string;
  check: boolean;
}
