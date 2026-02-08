using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataImportProj.Database
{
    [Index(nameof(OldId), IsUnique = true)]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        public int? Age { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool Deleted { get; set; }

        public int? OldId { get; set; }

        public virtual ICollection<ProductCustomer> ProductCustomers { get; } = new List<ProductCustomer>();
    }
}
