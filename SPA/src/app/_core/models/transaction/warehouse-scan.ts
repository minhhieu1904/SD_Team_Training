export interface MS_QR_Storage {
    manuf: string;
    sDat: string;
    trNo: string;
    storeH: string;
    parNo: string;
    manNo: string;
    purNo: string;
    serial: number;
    size: string;
    qty: number;
    rModel: string;
    bitNbr: string;
    style: string;
    article: string;
    cyNo: string;
    seq: string;
}
export interface WarehouseScanDto {
  label: string[];
  location : string;
  department : string;
  current_User : string;
  scan_Date : string;
}
