// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectController.cs" company="">
//   
// </copyright>
// <summary>
//   The project controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Linq;
using Qdata.Json.Contract;


namespace Example.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Example.Data.Contract.CrmModel;
    using Example.Service;

    /// <summary>
    ///     The project controller.
    /// </summary>
    [RoutePrefix("api/crm")]
    public class CrmController : ApiController
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get.
        /// </summary>
        /// <returns>
        ///     The <see cref="HttpResponseMessage" />.
        /// </returns>
        [HttpGet]
        [Route("Metadata")]
        public object Get()
        {
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                new CustomerDto() { Contacts = new List<ContactDto>() { new ContactDto() } });
        }

        [HttpPost]
        [Route("customer")]
        public HttpResponseMessage PostCustomer([FromBody] QDescriptor param)
        {
            var service = CrmSearchService.GetInstance();
            var result = service.Find<CustomerDto>(param);
            var result1 = service.Page<CustomerDto>(param,1,2);
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("contact")]
        public HttpResponseMessage PostContact([FromBody] QDescriptor param)
        {
            var service = CrmSearchService.GetInstance();
            var result = service.Find<ContactDto>(param);
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("test")]
        public object Test()
        {
            
            var v = new List<ContactDto>().AsQueryable();
            //var ex = v.Where(x => !x.Birfsday.HasValue || x.Birfsday.Value.ToString().Contains("a")).Expression;
            //var a =  v.Select(x => new {a = x.Birfsday , c = new  { n = x.Customer.Street } }).AsQueryable();
            var service = CrmSearchService.GetInstance();
            
            //service.test1(a);
            service.test<ContactDto>();
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                new CustomerDto() { Contacts = new List<ContactDto>() { new ContactDto() } });
        }
        #endregion
    }
}