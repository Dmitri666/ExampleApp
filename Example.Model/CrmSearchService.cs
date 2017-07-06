// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The project repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System.Linq;
using Example.DB;

namespace Example.Service
{
    /// <summary>
    ///     The project repository.
    /// </summary>
    public class CrmSearchService

    {
        private readonly CrmDataModel ctx;

        public CrmSearchService()
        {
            ctx = new CrmDataModel();
        }

        public IQueryable<Customer> Customers

        {
            get { return ctx.Customers.AsQueryable(); }
        }
    }
}