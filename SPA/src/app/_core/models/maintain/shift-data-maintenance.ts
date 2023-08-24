import { MS_Shift } from './../common/mS_Shift_DTO';
export interface ShiftParam {
  shift: string,
  shiftName: string
}

export interface ShiftType {
  ms_Shift: MS_Shift
  type: string;
}
