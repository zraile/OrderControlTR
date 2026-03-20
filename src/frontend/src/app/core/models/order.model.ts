export enum OrderStatus {
  New = 1,
  Preparing = 2,
  Ready = 3,
  Delivered = 4,
  Cancelled = 5
}
export enum OrderType {
  DineIn = 1,
  TakeAway = 2,
  Delivery = 3
}
export interface OrderItem {
  id: number;
  orderId: number;
  menuItemId: number;
  menuItemName?: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  notes?: string;
}
export interface Order {
  id: number;
  tableId?: number;
  tableName?: string;
  branchId: number;
  orderType: OrderType;
  status: OrderStatus;
  notes?: string;
  totalAmount: number;
  createdAt: string;
  orderItems?: OrderItem[];
}
export interface CreateOrderDto {
  tableId?: number;
  branchId: number;
  orderType: OrderType;
  notes?: string;
  totalAmount: number;
}
export interface CreateOrderItemDto {
  orderId: number;
  menuItemId: number;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  notes?: string;
}
