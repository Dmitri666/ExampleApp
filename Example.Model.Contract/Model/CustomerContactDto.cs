using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data.Contract.Model
{
    using QData.Common;

    public class CustomerContactDto : IModelEntity
    {
        public string FirstName { get; set; }

        public string Firma { get; set; }
    }
}
