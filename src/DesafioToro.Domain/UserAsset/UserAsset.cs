namespace DesafioToro.Domain.Asset
{
    public class UserAsset
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public int StockId { get; set; }
    }
}
