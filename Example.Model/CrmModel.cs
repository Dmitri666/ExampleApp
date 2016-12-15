// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The project repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Repo
{
    using System.Linq;

    using AutoMapper;

    using Example.Data.Contract.CrmModel;
    using Example.DB;

    using Qdata.Json.Contract;

    using QData.Common;
    using QData.Model;

    /// <summary>
    ///     The project repository.
    /// </summary>
    public class CrmModel 

    {
        private static CrmModel instance;

        protected MapperConfiguration Mapping { get; set; }

        private CrmModel()
        {
        }

        public static CrmModel GetInstance()
        {
            if (instance == null)
            {
                instance = new CrmModel();
                instance.Mapping = new MapperConfiguration(
                    cfg =>
                        {
                            cfg.CreateMissingTypeMaps = true;

                            cfg.CreateMap<Contact, ContactDto>()
                                .ForMember(dto => dto.Customer, op => op.MapFrom(con => con.Customer));

                            cfg.CreateMap<Customer, CustomerDto>()
                                .ForMember(dto => dto.Firma11, op => op.MapFrom(cus => cus.Firma1))
                                .ForMember(dto => dto.Firma21, opts => opts.MapFrom(cus => cus.Firma2))
                                .ForMember(dto => dto.Contacts, op => op.MapFrom(cus => cus.Contacts));

                            cfg.CreateMap<ContactDto, Contact>()
                                .ForMember(con => con.Customer, op => op.MapFrom(dto => dto.Customer));

                            cfg.CreateMap<CustomerDto, Customer>()
                                .ForMember(cus => cus.Firma1, op => op.MapFrom(dto => dto.Firma11))
                                .ForMember(cus => cus.Firma2, opts => opts.MapFrom(dto => dto.Firma21))
                                .ForMember(cus => cus.Contacts, op => op.MapFrom(dto => dto.Contacts));


                        });
            }

            return instance;
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

        public void Update<TM>(TM model)
           where TM : IModelEntity
        {
            using (var ctx = new CrmDataModel())
            {

                var typeMap =
                this.Mapping.GetAllTypeMaps()
                    .FirstOrDefault(x => x.SourceType == typeof(TM));

                var repo = new Model<TM>(this.Mapping);
                //repo.Update(model);
            }
        }
    }
}