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
                                .ForMember(dto => dto.Contacts, op => op.MapFrom(cus => cus.Contacts))
                                .ForMember(dto => dto.ContactsCount, op => op.MapFrom(cus => cus.Contacts.Count())).MaxDepth(1);

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

        public object Find<TM>(QDescriptor<TM> param)
           where TM : IModelEntity
        {
            using (var ctx = new CrmDataModel())
            {

                var typeMap =
                this.Mapping.GetAllTypeMaps()
                    .FirstOrDefault(x => x.DestinationType == typeof(TM));

                var query = ctx.Set(typeMap.SourceType).AsQueryable();
                var v = query.ProjectTo<TM>(this.Mapping.CreateMapper().ConfigurationProvider);

                
                var result = new SearchService().Search(param, v);
                return result;
            }
        }

        public void Update<TM>(TM model)
           where TM : IModelEntity
        {
            using (var ctx = new CrmDataModel())
            {

               
            }
        }
    }
}