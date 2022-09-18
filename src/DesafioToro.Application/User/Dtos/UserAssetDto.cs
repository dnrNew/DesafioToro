namespace DesafioToro.Application.Dtos
{
    public class UserAssetDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
