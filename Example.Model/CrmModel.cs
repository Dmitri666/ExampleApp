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
    using AutoMapper;

    using Example.Data.Contract.Model;
    using Example.DB;

    /// <summary>
    ///     The project repository.
    /// </summary>
    public class CrmModel : BaseModel

    {
        private static CrmModel instance;

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

                            
                        });
            }

            return instance;
        }
    }
}