export interface MSQRStorageDto {
    manuf: string;
    sdat: string;
    iodat: string;
    trno: string;
    storeh: string;
    parno: string;
    grade: string;
    qrcodeId: string;
    manno: string;
    purno: string;
    serial: number;
    size: string;
    qty: number;
    crusr: string;
    crday: string;
    bizFlag: string;
    bizTime: string | null;
    wkshno: string;
    prtno: string;
    wkshqty: number;
    mdat: string;
    uscod: string;
    empno: string;
    bitnbr: string;
    ritnbr: string;
    isPicking: string;
    isStorageOut: string;
    check: boolean;
}

export interface ReportStorageSum {
    date_kind: string
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

export interface ReportStorageSumDetail {
    isStorageSort: string;
    qrcodeId: string;
    crDay: string;
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
    eta: string;
}

export interface ReportStorageSumDetailParam {
    manno: string;
    purno: string;
    size: string;
}
export interface Brand {
    id: string;
    brandname: string
}