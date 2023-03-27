export interface ReprintStickerParam {
  fromDate: string;
  toDate: string;
  prtno: string;
  serial: number;
  manNo: string;
  size: string;
  purNo: string;
}
export interface ReprintStickerModel {
  manuf: string;
  qrCodeID: string;
  mdat: string | null;
  grade: string;
  prtno: string;
  wkshno: string;
  manNo: string;
  purNo: string;
  size: string;
  serial: number;
  qty: number;
  prt_Cnt: number;
  qrCodeValue: string;
  brandname: string;
  cusid: string;
  cusna: string;
  bitnbr: string;
  ritnbr: string;
  rmodel: string;
  article: string;
  empno: string;
  crUsr: string;
  prtDay: string | null;
  kind: string;
  check: boolean;
  remark: string;
}