using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace BookStore.Models
{
    public class User
    {
        
        public int Id { get; set; }
        [Required,StringLength(12,MinimumLength =4)]
        public string username { get; set; }
        [Required,DataType(DataType.Password),StringLength(10,MinimumLength =4)]
        public string password { get; set; }
        [Required,EmailAddress]
        public string email { get; set; }
        [Required, StringLength(12, MinimumLength = 4)]
        public string firstName { get; set; }
        public string lastName { get; set; }
        public virtual ICollection<Role> Roles { get; set;}=new HashSet<Role>();

    }
}
