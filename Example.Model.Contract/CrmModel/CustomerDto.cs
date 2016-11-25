namespace Example.Data.Contract.CrmModel
{
    using System.Collections.Generic;

    using AdminModul.Contracts;

    using QData.Common;

    public class CustomerDto : IModelEntity
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

        public User CreatedBy { get; set; }
    }
}