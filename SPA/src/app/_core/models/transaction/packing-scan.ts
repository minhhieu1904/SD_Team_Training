export interface PackingScanViewDto {
  manuf: string;
  sDat: string;
  trNo: string;
  shift: string;
  grade: string;
  qrCodeID: string;
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

export interface PackingScanDto {
  shift: string;
  scan_Date: string;
  listData: string[];
}

export interface PackingScanExportDto {
  manNo: string;
  listItemPerPage: PackingScanViewDto[];
  qty: number;
}

