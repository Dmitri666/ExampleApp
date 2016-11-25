using System;
using System.Collections.Generic;


namespace Example.Data.Contract.AuditModel
{
    using QData.Common;

    public class User : IModelEntity
    {
        public User()
        {
            this.Customers = new List<CustomerDto>();
            this.Contacts = new List<ContactDto>();
            
        }

        public int Id { get; set; }

        public string Name { get; set; }

        string Password { get; set; }

        public virtual IEnumerable<CustomerDto> Customers { get; set; }

        public virtual IEnumerable<ContactDto> Contacts { get; set; }

        
    }
}
