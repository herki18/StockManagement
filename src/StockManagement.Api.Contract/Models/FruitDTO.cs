namespace StockManagement.Api.Contract.Models
{
    public class FruitDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public VarietyDTO Variety { get; set; }
    }
}