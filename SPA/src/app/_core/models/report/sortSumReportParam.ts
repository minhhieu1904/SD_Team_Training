export interface SortSumReportParam {
    date_kind: string;
    date_start: string;
    date_end: string;
    brand: string;
    cusid: string;
    manno: string;
    rmodel: string;
    tolcls: string;
    purchase_no: string;
    material: string;
    kind: string;
    etd_start: string;
    etd_end: string;
    size: string;
}

export interface SortSumDetailReportParam {
    manno: string;
    purno: string;
    size: string;
}
