using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.DB
{
    [Table("UserRole")]
    public class UserRole
    {
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual Rolle Role { get; set; }
    }
}
