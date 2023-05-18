using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Review
    {
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [Required]
        public string VoterName { get; set; }
        [Required,Range(1,5)]
        
        public int NumStarts { get; set;}
        public string Comment { get; set; }

        public virtual Book Book  { get; set; }
    }
}
