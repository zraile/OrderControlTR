export enum PaymentType {
  Cash = 1,
  CreditCard = 2,
  Online = 3
}
export interface Payment {
  id: number;
  orderId: number;
  amount: number;
  paymentType: PaymentType;
  createdAt: string;
}
export interface CreatePaymentDto {
  orderId: number;
  amount: number;
  paymentType: PaymentType;
}
