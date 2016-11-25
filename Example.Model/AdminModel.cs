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

    using Example.Data.Contract.AdminModel;
    using Example.DB;

    /// <summary>
    ///     The project repository.
    /// </summary>
    public class AdminModel : BaseModel

    {
        private static AdminModel instance;

        private AdminModel()
        {
        }

        public static AdminModel GetInstance()
        {
            if (instance == null)
            {
                instance = new AdminModel();
                instance.Mapping = new MapperConfiguration(
                    cfg =>
                        {
                            cfg.CreateMissingTypeMaps = true;

                            cfg.CreateMap<User, UserDto>()
                                .ForMember(dto => dto.UserRoles, op => op.MapFrom(con => con.UserRoles));

                            cfg.CreateMap<Rolle, RolleDto>()
                                .ForMember(dto => dto.UserRoles, op => op.MapFrom(cus => cus.UserRoles));

                            cfg.CreateMap<UserRole, UserRoleDto>()
                                .ForMember(dto => dto.User, op => op.MapFrom(cus => cus.User))
                                .ForMember(dto => dto.Role, op => op.MapFrom(cus => cus.Role));


                        });
            }

            return instance;
        }
    }
}