using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dot.Models.Models
{
    public class Category
    {

        [Key]
        public int id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayName("Dispaly Order")]
        [Range(1,100, ErrorMessage ="يجب ان يكون الارقام بين 1 - 100")]
        public int DisplayOrder { get; set; }


    }

}
