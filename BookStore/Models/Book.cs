using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using BookStore.Services;

namespace BookStore.Models
{
    public class Book 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string ImgName { get; set; }
        public virtual ICollection<BookAuthors> BookAuthors { get; set; } = new HashSet<BookAuthors>();
        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();



    }
}
