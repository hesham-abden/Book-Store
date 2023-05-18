using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Role
    {
        
        public int Id { get; set; }

        [Required, StringLength(12, MinimumLength = 4)]
        public string Name { get;set; }
        public virtual ICollection<User> Users { get; set; }=new HashSet<User>();


    }
}
