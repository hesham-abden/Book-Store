using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class BookAuthors
    {
        
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public int? Order { get; set; }
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }    
    }
}
