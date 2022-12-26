using CG.CqrsCrud.Attributes.Commons;
using CG.CqrsCrud.Attributes.MediatorAttributes;
using CG.CqrsCrud.Attributes.MediatorAttributes.Commands;
using CG.CqrsCrud.Attributes.MediatorAttributes.Queries;

namespace CG.Domain.Entities
{
    [AddMediator]
    [UpdateMediator]
    [DeleteMediator]
    [GetMediator]
    [GetListMediator]
    [Plural("Users")]
    public class User : Entity<int>
    {
        [PrimaryKeyAttribute]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}