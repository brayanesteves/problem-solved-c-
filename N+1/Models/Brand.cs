using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N1.Models {
    public class Brand {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int BrandID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Beer> Beers { get; set; }
    } 
}