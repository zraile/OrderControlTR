export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  userName: string;
  phoneNumber?: string;
  role: string;
  restaurantId?: number;
}
