using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3KPZ_CF_.Data
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(255)]
        public string PasswordHash { get; set; }
        public int? RoleID { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string SecondName { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; set; }
        public ICollection<FileImport> FileImports { get; set; }
        public Student Student { get; set; }
    }
}
