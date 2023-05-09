export interface PackingScanDTO {
  shift: string;
  listData: string[];
}
export interface PackingScanExportDTO {
  manNo: string;
  listItemPerPage: ViewDataPackingScan[];
  qty: number;
}
export interface ViewDataPackingScan {
  manuf: string;
  sDat: string;
  trNo: string;
  shift: string;
  grade: string;
  qRCodeID: string;
  manNo: string;
  purNo: string;
  serial: number;
  size: string;
  qty: number;
  crUsr: string;
  crDay: string;
  endCod: string;
  rModel: string;
  style: string;
  article: string;
  cyNo: string;
  bitnbr: string;
  seq: string;
  printTime: string;
}
