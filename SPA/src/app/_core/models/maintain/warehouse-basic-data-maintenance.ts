import { MS_Location } from '../common/mS_Location_DTO';
export interface WarehouseParam {
  storeH: string,
  locationName: string
}

export interface WarehouseType {
  ms_Location: MS_Location
  type: string;
}
