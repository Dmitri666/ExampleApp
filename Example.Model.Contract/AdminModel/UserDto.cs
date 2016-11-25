namespace Example.Data.Contract.AdminModel
{
    using System.Collections.Generic;

    using QData.Common;

    public class UserDto : IModelEntity
    {
        public UserDto()
        {
            this.UserRoles = new HashSet<UserRoleDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        string Password { get; set; }

        

        public virtual IEnumerable<UserRoleDto> UserRoles { get; set; }
    }
}
