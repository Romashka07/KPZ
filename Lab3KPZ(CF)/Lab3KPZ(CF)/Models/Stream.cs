using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Lab3KPZ_CF_.Data
{

    public class Stream
    {
        [Key]
        public int StreamID { get; set; }
        [Required, MaxLength(50)]
        public string StreamName { get; set; }
        public int? Year { get; set; }
        public int? CourseID { get; set; }

        [ForeignKey("CourseID")]
        public Course Course { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
