export interface Storage_sumDTO {
  crDay: string ;
  date_kind: string;
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
export interface Report_Storage_SumParam {
  crDay: string | null;
  mdat: string | null;
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
  eta: string;
  size: string;
  qty: number ;
  wkshqty: number;
  pqty: number ;
  sort_qty: number ;
  storage_qty: number ;
  diff_qty: number ;
  cqty: number ;
  ischeck: boolean;
}
export interface StorageSumDeltailDTO {
  isStorageSort: string;
  crDay: string ;
  brandname: string;
  qRCodeID: string;
  manno: string;
  purno: string;
  size: string;
  serial: number;
  storage_Qty: number;
  cusid: string;
  cusna: string;
  rmodel: string;
  tolcls: string;
  ritnbr: string;
  bitnbr: string;
  article: string;
  kind: string;
  eta: string ;


}

export interface StorageSumDeltailDTOParam {
  manno: string;
  purno: string;
  size: string;
}
export interface Brand {
  id: string;
  name: string
}
