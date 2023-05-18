using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        [Required]
        public string Catergory { get; set; }
        public virtual ICollection<Book> Books { get; set; }=new HashSet<Book>();
    }
}
