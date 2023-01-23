namespace PaymentService.Data.Dtos;

public class PaymentDto
{
    public int Id { get; set; }
    public Guid PaymentUid { get; set; }
    public string Status { get; set; } = "";
    public int Price { get; set; }
}