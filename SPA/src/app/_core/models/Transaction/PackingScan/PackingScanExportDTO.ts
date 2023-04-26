import { PackingScanViewDTO } from "./PackingScanViewDTO";

export interface PackingScanExportDTO {
  manNo: string;
  listItemPerPage: PackingScanViewDTO[];
  qty: number;
}
