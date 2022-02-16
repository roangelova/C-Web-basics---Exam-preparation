using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Data.Models
{

    using static DataConstants;

    public class User
    {
        [Key]
        [StringLength(IdMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [StringLength(UserNameMaxLength)]
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(UserEmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [StringLength(UserPasswordMaxLength)]
        public string Password { get; set; }

        [StringLength(IdMaxLength)]
        public string CardId { get; set; }

        [Required]
        [ForeignKey(nameof(CardId))]
        public Cart Cart { get; set; }

    }
}
