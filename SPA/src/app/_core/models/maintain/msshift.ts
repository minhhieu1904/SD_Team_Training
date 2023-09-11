export interface MS_Shift {
  manuf: string;
  shift: string;
  shiftName: string
}
export interface ShiftDataMaintainParam {
  shift: string;
  shiftName: string;
}

export interface ShiftDataMaintainUploadParam {
  shift: string;
  file: File;
}
