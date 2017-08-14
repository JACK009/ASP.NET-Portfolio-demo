using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models
{
    [Table("Tags")]
    public class TagModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [Index(IsUnique = true)]
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<ProjectModel> Projects { get; set; }
    }
}