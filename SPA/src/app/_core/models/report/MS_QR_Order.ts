export interface WkshSumReport {
    mdat_start: string ;
    mdat_end: string ;
    close_status: string;
    brandname: string;
    cusna: string;
    manno: string;
    rmodel: string;
    tolcls: string;
    purno: string;
    bitnbr: string;
    kind: string;
    eta_start: string ;
    eta_end: string ;
    size: string;
}
export interface MS_QR_Order {
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
export interface Report_Sort_SumResult {
    crday: string ;
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
    eta: string ;
    size: string;
    qty: number ;
    wkshqty: number ;
    pqty: number ;
    sort_qty: number;
    diff_qty: number ;
    cqty: number;
}
export interface SortSumReport {
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
export interface Brand {
    id: string;
    name: string
}