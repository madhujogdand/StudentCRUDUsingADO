using System.ComponentModel.DataAnnotations;

namespace StudentCRUDUsingADO.Models
{
    public class Student
    {
        [Key]
        public int RollNo { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Course{ get; set;}

        [Required]
        public double Fees { get; set; }
    }
}
