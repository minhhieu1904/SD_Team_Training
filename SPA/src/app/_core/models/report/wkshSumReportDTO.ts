export interface WkshSumReportDTO {
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