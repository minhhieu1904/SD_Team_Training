import { PickingScanParam } from "../../_helpers/params/transaction/pickingScanParam";

export interface PickingDetail {
  manuf: string;
  sdat: string;
  declaration_no: string;
  storageTrNo: string;
  grade: string;
  qrCodeID: string;
  qrCodeValue: string;
  manno: string;
  purno: string;
  serial: number;
  size: string;
  qty: number;
  crusr: string;
  crday: string;
  wkshno: string;
  prtno: string;
  wkshqty: number;
  mdat: string;
  empno: string;
  bitnbr: string;
  ritnbr: string;
  isDelete: boolean;

  isBlock: boolean; // Khi Edit là danh sách mặc định của DB
}

export interface PickingMainDto {
  manuf: string;
  declaration_no: string;
  invno: string;
  iono: string;
  expectStorageOutDate: string | null;
  manno: string;
  purno: string;
  size: string;
  expectQTY: number | null;
  actualQTY: number | null;
  releaseDate: string | null;
  storageOutDate: string | null;
  status: string;
  updated_by: string;
  update_time: string | null;
  checkRelease: boolean;
}

export interface PickingUpdate {
  data: GetScanPickingMainDto;
  param: PickingScanParam;
}

export interface GetScanPickingMainDto {
  pickingMain: PickingMainDto;
  listPickingDetail: PickingDetail[];
}
