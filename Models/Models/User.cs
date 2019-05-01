using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Models.Models
{
    public class User : IdentityUser
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Fullname { get; set; }
        
        public List<Question> Questions { get; set; }
        
        public List<Answer> Answers { get; set; }
    }
}