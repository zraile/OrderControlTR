export interface Restaurant {
  id: number;
  name: string;
  address: string;
  phoneNumber: string;
  taxNumber: string;
  logoUrl?: string;
}
export interface CreateRestaurantDto {
  name: string;
  address: string;
  phoneNumber: string;
  taxNumber: string;
  logoUrl?: string;
}
