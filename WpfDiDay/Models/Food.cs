using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDiDay.Models
{
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodId { get; set; } // Primary key 

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign key to User

        [Required]
        public string FoodName { get; set; } = "";

        [Required]
        public DateTime FoodEatingTime { get; set; } = DateTime.Now;

        [Required]
        public long FoodCalories { get; set; } = 0;

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!; // Navigation property to User
    }
}
