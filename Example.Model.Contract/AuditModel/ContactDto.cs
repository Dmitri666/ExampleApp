using AdminModul.Contracts;
using QData.Common;

namespace Example.Data.Contract.AuditModel
{
    using System;

    public class ContactDto : IModelEntity
    {
        public ContactDto()
        {
        }

        public long Id { get; set; }

        public int EdvNr { get; set; }

        public User CreatedBy { get; set; }
    }
}