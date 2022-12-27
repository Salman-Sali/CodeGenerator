using CG.CqrsCrud.Attributes.Commons;
using CG.CqrsCrud.Attributes.MediatorAttributes;
using CG.CqrsCrud.Attributes.MediatorAttributes.Commands;
using CG.CqrsCrud.Attributes.MediatorAttributes.Queries;

namespace CG.Domain.Entities
{
    [GetMediator]
    [GetListMediator]
    public class User : Entity<int>
    {
        [PrimaryKeyAttribute]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
    }

    public enum UserRole
    {
        Admin
    }
}