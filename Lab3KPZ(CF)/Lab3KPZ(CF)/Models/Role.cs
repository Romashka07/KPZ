using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab3KPZ_CF_.Data
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        [Required, MaxLength(50)]
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
