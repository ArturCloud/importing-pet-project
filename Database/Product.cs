using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataImportProj.Database
{
    [Index(nameof(OldId), IsUnique = true)]
    public class Product
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateOnly BestBefore { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool Deleted { get; set; }

        public int? OldId { get; set; }

        public virtual ICollection<ProductCustomer> ProductCustomers { get; } = new List<ProductCustomer>();

        public virtual ICollection<ProductTranslation> Translations { get; } = new List<ProductTranslation>();
    }
}
