namespace Example.Data.Contract.AdminModel
{
    using System.Collections.Generic;

    using QData.Common;

    public class RolleDto : IModelEntity
    {
        public RolleDto()
        {
            this.UserRoles = new List<UserRoleDto>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<UserRoleDto> UserRoles { get; set; }
    }
}
