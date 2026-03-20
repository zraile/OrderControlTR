export enum TableStatus {
  Available = 1,
  Occupied = 2,
  Reserved = 3
}
export interface Table {
  id: number;
  name: string;
  capacity: number;
  branchId: number;
  status: TableStatus;
}
export interface CreateTableDto {
  name: string;
  capacity: number;
  branchId: number;
  status: TableStatus;
}
