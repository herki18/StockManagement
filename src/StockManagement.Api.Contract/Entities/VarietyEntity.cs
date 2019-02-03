using System.Collections.Generic;

namespace StockManagement.Api.Contract.Entities
{
    public class VarietyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<FruitEntity> Fruits { get; set; }
    }
}