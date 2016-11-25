using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Repo
{
    using System.Data.Entity;

    using AutoMapper;

    using Example.DB;

    using Qdata.Json.Contract;

    using QData.Common;
    using QData.Model;

    public class BaseModel
    {
        protected MapperConfiguration Mapping { get; set; }

        protected BaseModel()
        {
        }
        
        public object Find<TM>(QDescriptor param)
            where TM : IModelEntity
        {
            using (var ctx = new CrmDataModel())
            {

                var typeMap =
                this.Mapping.GetAllTypeMaps()
                    .FirstOrDefault(x => x.DestinationType == typeof(TM));

                var query = ctx.Set(typeMap.SourceType).AsQueryable();
                var repo = new Model<TM>(this.Mapping);
                var result = repo.Find(param, query);
                return result;
            }
        }

        

    }
}
