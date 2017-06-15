using System;

namespace Example.Data.Contract.CrmModel
{
    using QData.Common;

    public class ContactDto
    {
        public ContactDto()
        {
        }

        public long Id { get; set; }

        public int EdvNr { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }

        public string Ort { get; set; }

        public DateTime? Birfsday { get; set; }

        public CustomerDto Customer { get; set; }

        
    }
}