import { MS_QR_Storage } from '../../models/common/mS_QR_Storage'
import { MS_QR_PickingMain } from '../../models/common/mS_QR_PickingMain'

export interface WarehouseOutScan extends MS_QR_PickingMain {
  checkStorageOut: boolean
}

export interface MS_QR_StorageDto extends MS_QR_Storage {
  odat: string | null
  action: string
  isDelete: boolean
}

export interface WarehouseOutScanPickingMainDto {
  sdat: string;
  pickingTrNo: string;
  storageTrNo: string;
  grade: string;
  qRCodeID: string;
  manno: string;
  purno: string;
  size: string;
  qty: number;
}
export interface StorageOut {
  iono: string
}
