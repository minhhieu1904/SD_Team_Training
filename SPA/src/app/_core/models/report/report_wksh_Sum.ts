export interface ReportwkshSum {
    mdat_start: string;
    mdat_end: string;
    close_status: string;
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
export interface MSQROrder {
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

export interface Brand {
    id: string;
    brandname: string
}