using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
