using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.DB
{
    [Table("Rolle")]
    public class Rolle
    {
        public Rolle()
        {
            this.UserRoles = new HashSet<UserRole>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
