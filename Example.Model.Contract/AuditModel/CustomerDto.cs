
using AdminModul.Contracts;
using QData.Common;

namespace Example.Data.Contract.AuditModel
{
    using System;
    using System.Collections.Generic;

    public class CustomerDto : IModelEntity
    {
        public CustomerDto()
        {
            
        }

        public long Id { get; set; }

        public int EdvNr { get; set; }

        public User CreatedBy { get; set; }
    }
}