using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab3KPZ_CF_.Data
{
    public class FileImport
    {
        [Key]
        public int FileImportID { get; set; }
        public int? AdminID { get; set; }
        [Required, MaxLength(255)]
        public string FileName { get; set; }
        public DateTime? ImportDate { get; set; } = DateTime.Now;

        [ForeignKey("AdminID")]
        public User Admin { get; set; }
    }
}
