using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Log
    {
        [Key]
        public int ID { get; set; }
        public string? Level { get; set; }
        public string? OuterError { get; set; }
        public string? InnerError { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
