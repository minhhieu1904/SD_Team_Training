export interface ReportSortSumResult {
  crday: string | null;
  mdat: string;
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
  diff_qty: number | null;
  cqty: number | null;
  check: boolean
}

export interface SearchSortSumReportParams {
  crday: string;
  dateStart: string;
  dateEnd: string;
  brandname: string;
  cusna: string;
  manno: string;
  tolcls: string;
  rmodel: string;
  purno: string;
  bitnbr: string;
  kind: string;
  etaFrom: string;
  etaTo: string;
  size: string;
}
