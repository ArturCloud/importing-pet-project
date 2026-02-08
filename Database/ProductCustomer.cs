using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataImportProj.Database
{
    [PrimaryKey(nameof(CustomerId), nameof(ProductId))]
    public class ProductCustomer
    {
        [Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerId { get; set; }

        [Column(Order = 1)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } 

        [ForeignKey(nameof(ProductId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Customer Customer { get; set; }
    }
}
