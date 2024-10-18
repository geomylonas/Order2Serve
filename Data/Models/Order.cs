using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public virtual required ICollection<Orderable> Orderables { get; set; }
        public virtual required ICollection<Table> Table { get; set; }
        public virtual required User User {get;set;}
        public required DateTime OrderReceived { get; set; } = DateTime.Now;
        public Boolean IsReady { get; set; } = false;
        public Boolean IsDelivered { get; set; } = false;
        public Boolean IsCompleted { get; set; } = false;
    }
}
