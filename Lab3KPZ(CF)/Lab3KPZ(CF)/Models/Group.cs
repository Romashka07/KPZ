using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab3KPZ_CF_.Data
{
    public class Group
    {
        [Key]
        public int GroupID { get; set; }
        [Required, MaxLength(50)]
        public string GroupName { get; set; }
        public int? StreamID { get; set; }

        [ForeignKey("StreamID")]
        public Stream Stream { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
