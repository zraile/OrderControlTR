using OrderControlTR.Domain.Enums;

namespace OrderControlTR.Application.DTOs.Payment;

public class PaymentDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public DateTime PaymentDate { get; set; }
}

public class CreatePaymentDto
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
}
