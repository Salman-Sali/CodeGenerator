using CG.CqrsCrud.Attributes.Commons;
using CG.CqrsCrud.Attributes.MediatorAttributes;
using CG.CqrsCrud.Attributes.MediatorAttributes.Commands;

namespace CG.Domain.Entities
{
    [AddMediator]
    [UpdateMediator]
    [DeleteMediator]
    [PluralAttribute("Users")]
    public class User : Entity<int>
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}