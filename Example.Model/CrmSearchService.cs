// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The project repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System;

namespace Example.Service
{
    using System.Linq;


    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Example.Data.Contract.CrmModel;
    using Example.DB;

    using Qdata.Json.Contract;

    using QData.Common;
    
    using System.Collections.Generic;

    using QData.ExpressionProvider;
    using QData.Querable.Extentions;

    /// <summary>
    ///     The project repository.
    /// </summary>
    public class CrmSearchService 

    {
        private static CrmSearchService instance;

        protected MapperConfiguration Mapping { get; set; }

        private CrmSearchService()
        {
        }

        public static CrmSearchService GetInstance()
        {
            if (instance == null)
            {
                instance = new CrmSearchService();
                instance.Mapping = new MapperConfiguration(
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

            return instance;
        }

        public object Find<TM>(QDescriptor param)
        
        {
            
            using (var ctx = new CrmDataModel())
            {
                //var t = ctx.Customers.Where(c => c.Id.ToString().Contains("1")).ToList();
                var typeMap =
                this.Mapping.GetAllTypeMaps()
                    .FirstOrDefault(x => x.DestinationType == typeof(TM));

                var query = ctx.Set(typeMap.SourceType).AsQueryable();
                var source = query.ProjectTo<TM>(this.Mapping.CreateMapper().ConfigurationProvider);

                var provider = new ExpressionProvider(source);
                var expression = provider.ConvertToExpression(param);

                var result = source.Execute(expression);
               
                return result;
            }
        }

        public void test<TM>()
        {
            using (var ctx = new CrmDataModel())
            {
                var typeMap =
                this.Mapping.GetAllTypeMaps()
                    .FirstOrDefault(x => x.DestinationType == typeof(TM));

                var query = ctx.Set(typeMap.SourceType).AsQueryable();
                var source = query.ProjectTo<TM>(this.Mapping.CreateMapper().ConfigurationProvider);

                var provider = new ExpressionProvider(source);
                var expression = provider.GetFullTextSearchExpression("So");
                var t = query.Execute(expression);



            }
        }

        public void test1(IQueryable query)
        {
            

                var provider = new ExpressionProvider(query);
                var expression = provider.GetFullTextSearchExpression("a");
            var t = query.Execute(expression);






        }
        public Page Page<TM>(QDescriptor param,int skip, int take)
        
        {

            using (var ctx = new CrmDataModel())
            {
                var typeMap =
                this.Mapping.GetAllTypeMaps()
                    .FirstOrDefault(x => x.DestinationType == typeof(TM));

                var query = ctx.Set(typeMap.SourceType).AsQueryable();
                var source = query.ProjectTo<TM>(this.Mapping.CreateMapper().ConfigurationProvider);

                var provider = new ExpressionProvider(source);
                var expression = provider.ConvertToExpression(param);

                var page = source.GetPage(expression, skip, take);
                return new Page()
                {
                    Total = page.Total,
                    Data = page.Data
                };
                
            
            }
        }
    }
}