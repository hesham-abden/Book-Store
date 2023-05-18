using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class PriceOffer
    {
        public int Id { get; set; }
        
        public string NewPrice { get; set; }
        
        public string PromotionText { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
