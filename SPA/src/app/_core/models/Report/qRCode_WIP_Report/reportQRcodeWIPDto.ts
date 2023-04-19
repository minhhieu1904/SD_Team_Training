import { PaginationResult } from './../../../utilities/pagination-utility';

export interface Report_Check_Sum_Miss {
    headers: ReportCheckSumMissHeader[];
    details: PaginationResult<ReportCheckSumMissDetail>;
}

export interface ReportCheckSumMissHeader {
    yymmdd: string;
    sumpackage: number | null;
    sumpairs: number | null;
    sumSortPack: number | null;
    sumSortPairs: number | null;
    sumlossPairs: number | null;
    sumStoragePack: number | null;
    sumStoragePairs: number | null;
    missPackage: number | null;
    missPairs: number | null;
    proportion: number | null;
}

export interface ReportCheckSumMissDetail {
    yymmdd: string;
    manuf: string;
    type: string;
    manNo: string;
    purNo: string;
    wkshno: string;
    prtno: string;
    qRCodeID: string;
    model: string;
    tolcls: string;
    bitnbr: string;
    eta: string | null;
    empno: string;
    serial: number | null;
    size: string;
    qty: number | null;
    crUsr: string;
    crdaY: string | null;
    crdaY_Format: string;
    sortflag: string;
    storageflag: string;
}
