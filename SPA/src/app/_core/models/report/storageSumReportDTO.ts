export interface StorageSumReportDTO {
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
  kind: string;
  article: string;
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