export interface SortSumReportParam {
    date_kind: string;
    date_start: string | null;
    date_end: string | null;
    brand: string;
    cusid: string;
    manno: string;
    rmodel: string;
    tolcls: string;
    purchase_no: string;
    material: string;
    kind: string;
    etd_start: string | null;
    etd_end: string | null;
    size: string;
}

export interface SortSumDetailReportParam {
    manno: string;
    purno: string;
    size: string;
}