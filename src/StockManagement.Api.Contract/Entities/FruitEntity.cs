using System.Collections.Generic;

namespace StockManagement.Api.Contract.Entities
{
    public class FruitEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VarietyId { get; set; }
        public VarietyEntity Variety { get; set; }
        public ICollection<BatchEntity> Batches { get; set; } = new List<BatchEntity>();
    }
}