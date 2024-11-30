using System.ComponentModel.DataAnnotations;

namespace Lab3KPZ_CF_.Data
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        [Required, MaxLength(100)]
        public string CourseName { get; set; }
        public string? Description { get; set; }

        public ICollection<Stream> Streams { get; set; }
    }
}
