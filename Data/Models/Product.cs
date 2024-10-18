using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public Decimal ProductPrice { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Extra> Extras { get; set; } = new List<Extra>();
        public virtual ICollection<Orderable> Orderables { get; set; } = new List<Orderable>();
    }
}
