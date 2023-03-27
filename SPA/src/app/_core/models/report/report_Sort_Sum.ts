export interface MSQRSortDto {
    manuf: string;
    sdat: string;
    trNo: string;
    shift: string;
    grade: string;
    qrcodeId: string;
    manNo: string;
    purNo: string;
    serial: number;
    size: string;
    qty: number;
    crUsr: string;
    crDay: string;
    endCod: string;
    check:boolean
}

export interface ReportSortSum {
    date_kind: string;
    date_start: string;
    date_end: string;
    brandname: string;
    cusna: string;
    manno: string;
    purno: string;
    rmodel: string;
    tolcls: string;
    bitnbr: string;
    kind: string;
    etd_start: string;
    etd_end: string;
    size: string;
}

export interface ReportSortSumExcelDetail {
    isScanSort: string;
    qrcodeId: string;
    crDay: string | null;
    brandname: string;
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

export interface Brand {
    id: string;
    brandname: string
}