using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagement.Api.Contract.Entities
{
    public class BatchEntity
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int FruitId { get; set; }

        [Required] public FruitEntity Fruit { get; set; }

        public int Quantity { get; set; }
    }
}