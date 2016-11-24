namespace Example.Repo
{
    using AutoMapper;

    using Example.Data.Contract.Model;
    using Example.DB;

    public class FlatModel : BaseModel
    {
        private static FlatModel instance;

        private FlatModel()
        {
        }

        public static FlatModel GetInstance()
        {
            if (instance == null)
            {
                instance = new FlatModel();
                instance.Mapping = new MapperConfiguration(
                    cfg =>
                        {
                            cfg.CreateMissingTypeMaps = true;

                            cfg.CreateMap<Contact, CustomerContactDto>()
                                .ForMember(dto => dto.FirstName, op => op.MapFrom(con => con.FirstName))
                                .ForMember(dto => dto.Firma, op => op.MapFrom(con => con.Customer.Firma1));
                        });
            }

            return instance;
        }
    }
}