export interface MsQrOrder {
    manuf: string;
    brandname: string;
    cusid: string;
    cusna: string;
    kind: string;
    purno: string;
    manno: string;
    eta: string | null;
    bitnbr: string;
    ritnbr: string;
    rmodel: string;
    tolcls: string | null;
    article: string;
    style: string | null;
    size: string;
    tsize: string;
    qty: number;
    pqty: number;
    cqty: number;
    addqty: number;
    lessqty: number;
    endcod: string;
    cycleFlag: string;
    updateTime: string;
    updatedBy: string | null;
    bizKey: string;
    wkshno: string;
    prtno: string;
    wkshqty: number;
    mdat: string;
    uscod: string | null;
}

export interface WkshSumReport {
    mdat_start: string | null;
    mdat_end: string | null;
    close_status: string;
    brandname: string;
    cusna: string;
    manno: string;
    rmodel: string;
    tolcls: string;
    purno: string;
    bitnbr: string;
    kind: string;
    eta_start: string | null;
    eta_end: string | null;
    size: string;
}