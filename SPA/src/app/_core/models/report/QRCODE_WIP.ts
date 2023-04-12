export interface ReportQRCODEWIPParam {
  rmodel: string;
  tolcls: string;
  mdat_start: string | null;
  mdat_end: string | null;
}

export interface QRCODEWIPHeaderReportDTO {
  yymmdd: string;
  sumPackage: number | null;
  sumPairs: number | null;
  sumSortPack: number | null;
  sumSortPairs: number | null;
  sumLossPairs: number | null;
  sumStoragePack: number | null;
  sumStoragePairs: number | null;
  missPackage: number | null;
  missPairs: number | null;
  proPortion: number | null;
}

export interface QRCODEWIPDetailReportDTO {
  yymmdd: string;
  manuf: string;
  type: string;
  manNo: string;
  purNo: string;
  prtNo: string;
  wkshNo: string;
  qrCodeID: string;
  rmodel: string;
  tolcls: string;
  bitnbr: string;
  eta: string | null;
  empNo: string;
  serial: number | null;
  size: string;
  qty: number | null;
  crUsr: string;
  crdaY: string | null;
  crdaY_str: string;
  sortFlag: string;
  storageFlag: string;
}
