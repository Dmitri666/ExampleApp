namespace Example.Data.Contract.CrmModel
{
    using AdminModul.Contracts;

    using QData.Common;

    public class ContactDto : IModelEntity
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

        public CustomerDto Customer { get; set; }

        public User CreatedBy { get; set; }
    }
}