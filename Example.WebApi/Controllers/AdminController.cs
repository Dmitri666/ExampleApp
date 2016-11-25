// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectController.cs" company="">
//   
// </copyright>
// <summary>
//   The project controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Example.Repo;
using Qdata.Json.Contract;


namespace Example.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Example.Data.Contract.AdminModel;
    using Example.Data.Contract.CrmModel;

    /// <summary>
    ///     The project controller.
    /// </summary>
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
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
        [Route("user")]
        public HttpResponseMessage PostCustomer([FromBody] QDescriptor param)
        {
            var model = AdminModel.GetInstance();
            var result = model.Find<UserDto>(param);
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("role")]
        public HttpResponseMessage PostContact([FromBody] QDescriptor param)
        {
            var repository = AdminModel.GetInstance();
            var result = repository.Find<RolleDto>(param);
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion
    }
}