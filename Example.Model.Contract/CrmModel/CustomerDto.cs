namespace Example.Data.Contract.CrmModel
{
    using System.Collections.Generic;

    

    public class CustomerDto 
    {
        public CustomerDto()
        {
            this.Contacts = new HashSet<ContactDto>();
        }

        public long Id { get; set; }

        public int EdvNr { get; set; }

        public string Firma11 { get; set; }

        public string Firma21 { get; set; }

        public string Street { get; set; }

        public IEnumerable<ContactDto> Contacts { get; set; }

        public int ContactsCount { get; set; }
    }
}