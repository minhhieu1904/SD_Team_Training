export interface CheckSumMissReportDTO {
  lHeader: CheckSumMissHeaderReportDTO[];
  lDetail: CheckSumMissDetailReportDTO[];
}

export interface CheckSumMissHeaderReportDTO {
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

export interface CheckSumMissDetailReportDTO {
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
  sortFlag: string;
  storageFlag: string;
}