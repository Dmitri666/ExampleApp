using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qdata.Json.Contract;

namespace Example.Data.Contract
{
    [Serializable]
    public class ProjectionRequest
    {
        public QDescriptor SearchDescriptor { get; set; }

        public QDescriptor ProjectionDescriptor { get; set; }
    }
}
