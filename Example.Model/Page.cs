using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service
{
    [Serializable]
    public class Page
    {
        public int Total { get; set; }

        public object Data { get; set; }
    }
}
