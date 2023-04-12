export interface StorageSumReportParam {
    date_kind: string;
    date_start: string ;
    date_end: string;
    brandname: string;
    cusna: string;
    manno: string;
    rmodel: string;
    tolcls: string;
    purno: string;
    bitnbr: string;
    kind: string;
    eta_start: string;
    eta_end: string;
    size: string;
}
export interface StorageSumReportResult {
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
    eta: string ;
    size: string;
    qty: number ;
    wkshqty: number ;
    pqty: number ;
    sort_qty: number;
    storage_qty: number;
    diff_qty: number ;
    cqty: number ;
}
export interface Brand {
    id: string;
    name: string
}
export interface StorageSumDetailReportParam {
    manno: string;
    purno: string;
    size: string;
}

