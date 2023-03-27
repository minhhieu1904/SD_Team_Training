export interface CheckSumMissHeaderReportDTO {
    yymmdd: string;
    sumPackage: number;
    sumPairs: number;
    sumSortPack: number;
    sumSortPairs: number;
    sumLossPairs: number;
    sumStoragePack: number;
    sumStoragePairs: number;
    missPackage: number;
    missPairs: number;
    proPortion: number;
}

export interface CheckSumMissDetailReportDTO {
    yymmdd: string;
    manuf: string;
    type: string;
    manNo: string;
    purNo: string;
    prtNo: string;
    wkshNo: string;
    qrCodeID: string;
    rmodel: string;
    tolcls: string;
    bitnbr: string;
    eta: string;
    empNo: string;
    serial: number;
    size: string;
    qty: number;
    crUsr: string;
    crdaY: string;
    crdaY_str: string;
    sortFlag: string;
    storageFlag: string;
}

export interface CheckSumMissOtherReportDTO {
    curDate: string;
    totalSumPackage: number;
    totalSumPairs: number;
    totalSumSortPack: number;
    totalSumSortPairs: number;
    totalSumLossPairs: number;
    totalSumStoragePack: number;
    totalSumStoragePairs: number;
    totalMissPackage: number;
    totalMissPairs: number;
    totalProPortion: number;
}

export interface CheckSumMissReportParam {
    rmodel: string;
    tolcls: string;
    mdat_start: string;
    mdat_end: string;
}