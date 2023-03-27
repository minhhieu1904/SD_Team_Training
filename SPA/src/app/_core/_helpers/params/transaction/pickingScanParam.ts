export interface PickingSCanBaseParam {
    manuf: string;
    iono: string;
    manNo: string;
    purNo: string;
    size: string;
}

export interface PickingScanParam extends PickingSCanBaseParam {
    declarationNo: string;
    invno: string;
    releaseDate: string;
    startReleaseDate: string;
    endReleaseDate: string;

    isEditOrDetail?: boolean; // Nếu là True: Edit , False: Detail
}

export interface PickingScanSource {
    currentPage: number;
    param: PickingScanParam,
    source: PickingScanParam
}
export interface PickingDetailParam {
    iono: string;
    releaseDate: string;
    startReleaseDate: string;
    endReleaseDate:string;
    storageOutDate: string;
    size: string;
    manno: string;
    purno: string;
}