export interface SortSumReportDetail {
    isScanSort: string;
    crDay: string;
    brandname: string;
    qRCodeID: string;
    manNo: string;
    purNo: string;
    size: string;
    serial: number;
    qty: number;
    cusna: string;
    rmodel: string;
    tolcls: string;
    ritnbr: string;
    bitnbr: string;
    article: string;
    kind: string;
    eta: string | null;
}

export interface SortSumDetailReportParam {
    manno: string;
    purno: string;
    size: string;
}