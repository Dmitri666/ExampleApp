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
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Example.Data.Contract;
using Example.DB;
using Newtonsoft.Json;
using Qdata.Json.Contract;
using QData.ExpressionProvider;


namespace Example.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Example.Data.Contract.CrmModel;
    

    /// <summary>
    ///     The project controller.
    /// </summary>
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        public CustomerController()
        {
            this.Mapping = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<Contact, ContactDto>()
                            .ForMember(dto => dto.Customer, op => op.MapFrom(con => con.Customer)).MaxDepth(1);

                        cfg.CreateMap<Customer, CustomerDto>()
                            .ForMember(dto => dto.Firma11, op => op.MapFrom(cus => cus.Firma1))
                            .ForMember(dto => dto.Firma21, opts => opts.MapFrom(cus => cus.Firma2))
                            .ForMember(dto => dto.Contacts, op => op.MapFrom(cus => cus.Contacts));

                        cfg.CreateMap<ContactDto, Contact>()
                            .ForMember(con => con.Customer, op => op.MapFrom(dto => dto.Customer)).MaxDepth(1);

                        cfg.CreateMap<CustomerDto, Customer>()
                            .ForMember(cus => cus.Firma1, op => op.MapFrom(dto => dto.Firma11))
                            .ForMember(cus => cus.Firma2, opts => opts.MapFrom(dto => dto.Firma21))
                            .ForMember(cus => cus.Contacts, op => op.MapFrom(dto => dto.Contacts)).MaxDepth(1);


                    });
        }

        public IQueryable<CustomerDto> GetBaseQuery()
        {
            var customers = new CrmDataModel().Customers.AsQueryable();
            return customers.Select(x => new CustomerDto() { Id = x.Id, EdvNr = x.EdvNr,
                Contacts = x.Contacts.Select(c => new ContactDto() { Id = c.Id , EdvNr = c.EdvNr }) });
        }
        protected MapperConfiguration Mapping { get; set; }
        #region Public Methods and Operators



        [HttpPost]
        public HttpResponseMessage Get([FromBody] QDescriptor descriptor)
        {
            var query = this.GetBaseQuery();
            var expression = new ExpressionProvider(query).ConvertToExpression(descriptor);
            var searchResult = query.Provider.CreateQuery<CustomerDto>(expression);

            
            return this.Request.CreateResponse(HttpStatusCode.OK, searchResult);
        }

        [HttpPost]
        [Route("projection")]
        public HttpResponseMessage Projection([FromBody] ProjectionRequest request)
        {
            var query = this.GetBaseQuery();//.ToList().AsQueryable();
            //var t = query.Select(x => new { MyCntacts = x.Contacts.Select(p => new { MyId = p.Id }) }).AsQueryable().Expression;
            var searchExpression = new ExpressionProvider(query).ConvertToExpression(request.SearchDescriptor);
            //var searchResult = query.Provider.CreateQuery<CustomerDto>(searchExpression).ToList();
            var searchResult = query.Provider.CreateQuery<CustomerDto>(searchExpression).ToList().AsQueryable();

            

            var projectionExpression = new ExpressionProvider(searchResult.AsQueryable()).ConvertToExpression(request.ProjectionDescriptor);
            var pResult = searchResult.AsQueryable().Provider.CreateQuery(projectionExpression);

            var response = this.Request.CreateResponse(HttpStatusCode.OK, pResult);
            return response;
        }

        #endregion
    }
}