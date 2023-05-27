using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Ms.Domain.Property.Models
{
    public class UserModel
    {
        public string Identification { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
