using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Data.Models
{
    using static DataConstants;
    public class Product
    {
        [Key]
        [StringLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [StringLength(ProductNameMaxLength)]
        [Required]
        public string Name { get; set; }

        [Range(0.05, 1000)]
        [Required]
        public decimal Price { get; set; }

        [StringLength(IdMaxLength)]
        public string CartId { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }
    }
}
