export interface MS_QR_Label {
    manuf: string;
    type: string;
    manNo: string;
    purNo: string;
    wkshno: string;
    prtno: string;
    grade: string;
    qrcodeId: string;
    qrcodeValue: string;
    empno: string;
    seq: string;
    cyNo: string;
    serial: number;
    size: string;
    qty: number;
    prtCnt: number;
    flag: string;
    crUsr: string;
    crDay: string;
    canUsr: string;
    canDay: string | null;
    prtDay: string;
    rptReason: string;
    rptUsr: string;
    rptDate: string | null;
}