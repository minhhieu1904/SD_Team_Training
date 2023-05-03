export interface CheckSumMissHeaderDTO {
    yymmdd: string;
    sumPackage: number | null;
    sumPairs: number | null;
    sumSortPack: number | null;
    sumSortPairs: number | null;
    sumLossPairs: number | null;
    sumStoragePack: number | null;
    sumStoragePairs: number | null;
    missPackage: number | null;
    missPairs: number | null;
    proPortion: number | null;
}

export interface CheckSumMissDetailDTO {
    yymmdd: string;
    manuf: string;
    type: string;
    manno: string;
    purno: string;
    prtno: string;
    wkshno: string;
    qRCodeID: string;
    rmodel: string;
    tolcls: string;
    bitnbr: string;
    eta: string | null;
    empno: string;
    serial: number | null;
    size: string;
    qty: number | null;
    crusr: string;
    crday: string | null;
    softFlag: string;
    storageFlag: string;
}