import { PickingDetailParam } from "./pickingScanParam";

export interface WarehouseOutScanParam {
  declarationNo: string;
  invno: string;
  iono: string;
  releaseStartDate: string;
  releaseEndDate: string;
  size: string;
  manno: string;
  purno: string;
}

export interface PickingListScanParam {
  pickingTrNo: string;
  startReleaseDate: string;
  endReleaseDate: string;
  storeH: string;
  parNo: string;
}

export interface PickingScanItemParam {
  iono: string;
  size: string;
  manno: string;
  purno: string;
}

export interface WarehouseOutScanSource {
  currentPage: number;
  param: WarehouseOutScanParam;
  wareHouseOutScan: PickingDetailParam
}
