using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models
{
    [Table("ProjectCategories")]
    public class ProjectCategoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [Index]
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Slug { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Summary { get; set; }

        public ICollection<ProjectModel> Projects { get; set; }
    }
}