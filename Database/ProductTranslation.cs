using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DataImportProj.Database
{
    [PrimaryKey(nameof(ProductId), nameof(LanguageCode))]
    public class ProductTranslation
    {
        [Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [Column(Order = 1)]
        [Required]
        public string LanguageCode { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(ProductId))]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual Product Product { get; set; }
    }
}
