using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        [Required]
        public  string Password { get; set; } 

        [Required]
        public virtual UserRole Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();


        public User() { }

        public User(string email, string name, string surname, string password, UserRole role)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Password = password;
            Role = role;
        }
    }
}
