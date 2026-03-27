using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WpfDiDay.Models
{
    [Table("Foods")]
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodID { get; set; } // Primary Key
        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; } // Foreign Key to User.

        public string? FoodName { get; set; }
        public string? WhenEaten { get; set; }
        public int Calories { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!; // Navigation property to User
    }
}
