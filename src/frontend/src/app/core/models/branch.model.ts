export interface Branch {
  id: number;
  name: string;
  address: string;
  phoneNumber: string;
  restaurantId: number;
}
export interface CreateBranchDto {
  name: string;
  address: string;
  phoneNumber: string;
  restaurantId: number;
}
