using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab3KPZ_CF_.Data
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public int? UserID { get; set; }
        public int? GroupID { get; set; }
        public int Year { get; set; }
        public string Status { get; set; } = "active";

        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("GroupID")]
        public Group Group { get; set; }
    }
}
