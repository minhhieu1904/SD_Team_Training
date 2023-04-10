export interface StorageSumReportParam {
    date_kind: string;
    date_start: string;
    date_end: string;
    brandname: string;
    cusid: string;
    manno: string;
    rmodel: string;
    tolcls: string;
    purno: string;
    material: string;
    kind: string;
    etd_start: string;
    etd_end: string;
    size: string;
}

export interface StorageSumDetailReportParam {
    manno: string;
    purno: string;
    size: string;
}