namespace musingo_backend.Dtos;

public class TransactionUpdateDto
{
    public int TransactionId { get; set; }
    public double Cost { get; set; }
    public string TransactionStatus { get; set; }
}