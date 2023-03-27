import { MSLocation } from './../common/mS_Location';

export interface LocationParam {
  storeH: string,
  locationName: string
}

export interface LocationType {
  ms_Location: MSLocation
  type: string;
}
