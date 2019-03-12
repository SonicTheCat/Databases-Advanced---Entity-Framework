namespace P01_BillsPaymentSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(80)")]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "varchar(25)")]
        [Required]
        public string Password { get; set; }

        [InverseProperty("User")]
        public ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
    }
}