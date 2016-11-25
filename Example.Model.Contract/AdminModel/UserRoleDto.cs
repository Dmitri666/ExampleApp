namespace Example.Data.Contract.AdminModel
{
    using QData.Common;

    public class UserRoleDto : IModelEntity
    {
        public int Id { get; set; }

        public virtual UserDto User { get; set; }

        public virtual RolleDto Role { get; set; }
    }
}
