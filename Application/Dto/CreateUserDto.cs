using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class CreateUserDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public required int UserRole { get; set; }

    }
}
