namespace StockManagement.Api.Contract.Models.Batch
{
    public class BatchDTO
    {
        public int Id { get; set; }
        public string Fruit { get; set; }
        public int FruitId { get; set; }
        public string Variety { get; set; }
        public int VarietyId { get; set; }
        public int Quantity { get; set; }
    }
}