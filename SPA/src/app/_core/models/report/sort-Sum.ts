export interface Brand {
  id: string;
  name: string
}

export interface SortSumDTO {
  crDay: string;
  date_kind: string ;
  date_start: string ;
  date_end: string ;
  brandname: string;
  cusna: string;
  manno: string;
  purno: string;
  rmodel: string;
  tolcls: string;
  bitnbr: string;
  kind: string;
  eta_start: string ;
  eta_end: string ;
  size: string;
}

export interface Report_Sort_SumParam {
  crDay: string ;
  mdat: string ;
  brandname: string;
  cusid: string;
  cusna: string;
  manno: string;
  purno: string;
  rmodel: string;
  tolcls: string;
  ritnbr: string;
  bitnbr: string;
  article: string;
  kind: string;
  eta: string ;
  size: string;
  qty: number ;
  wkshqty: number ;
  pqty: number ;
  sort_qty: number ;
  diff_qty: number ;
  cqty: number ;
  ischeck: boolean;
}
export interface SortSumDeltailDTO {
  isScanSort: string;
  crDay: string ;
  brandname: string;
  qRCodeID: string;
  manno: string;
  purno: string;
  size: string;
  serial: number;
  label_Qty: number;
  cusna: string;
  rmodel: string;
  tolcls: string;
  ritnbr: string;
  bitnbr: string;
  article: string;
  kind: string;
  eta: string ;
}

export interface SortSumDeltailDTOParam {
  manno: string;
  purno: string;
  size: string;
}
