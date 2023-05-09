export interface MS_QR_Order {
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
    tolcls: string;
    article: string;
    style: string;
    size: string;
    tsize: string;
    qty: number;
    pqty: number;
    cqty: number;
    addqty: number;
    lessqty: number;
    endcod: string;
    cycle_flag: string;
    update_time: string;
    updated_by: string;
    biz_Key: string;
    wkshno: string;
    prtno: string;
    wkshqty: number;
    mdat: string;
    uscod: string;
    remark: string;
}