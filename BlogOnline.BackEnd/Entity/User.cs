using Microsoft.AspNetCore.Identity;

namespace BlogOnline.BackEnd.Entity
{
    public class User : IdentityUser
    {
        public User() : base()
        {
        }

        public User(string userName) : base(userName)
        {
        }

        [PersonalData]
        public string FullName { get; set; }
        public Guid? ImageUserId {  get; set; } 
    }
}
