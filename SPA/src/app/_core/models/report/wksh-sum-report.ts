export interface Report_Wksh_SumResult {
  mdat: string;
  is_close: string;
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
  eta: string | null;
  size: string;
  qty: number | null;
  wkshqty: number | null;
  pqty: number | null;
  sort_qty: number;
  storage_qty: number;
  diff_qty: number | null;
  cqty: number | null;
}

export interface WkshSumReportParam {
  mdat_start: string | null;
  mdat_end: string | null;
  close_status: string;
  brandname: string;
  cusna: string;
  manno: string;
  purno: string;
  rmodel: string;
  bitnbr: string;
  kind: string;
  eta_start: string | null;
  eta_end: string | null;
  size: string;
  tolcls: string;
}
