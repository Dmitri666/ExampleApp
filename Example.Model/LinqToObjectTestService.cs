// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The project repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Service
{
    using System.Linq;


    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Example.Data.Contract.CrmModel;
    using Example.DB;

    using Qdata.Json.Contract;

    using QData.Common;
    using QData.SearchService;
    using System.Collections.Generic;

    /// <summary>
    ///     The project repository.
    /// </summary>
    public class LinqToObjectSearchService 

    {


        public LinqToObjectSearchService()
        {
        }

       

        public object Find<TM>(QDescriptor<TM> param, List<TM> source)
           where TM : IModelEntity
        {
            var result = new SearchService().Search(param, source.AsQueryable());

            return result;

        }

        
    }
}