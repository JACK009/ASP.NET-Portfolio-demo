using Portfolio.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Portfolio.Models
{
    [Table("Projects")]
    public class ProjectModel
    {
        private string _slug;

        public enum ProjectStatus
        {
            Enabled = 0,
            Disabled = 1
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [Index]
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Slug
        {
            get => _slug;
            set => _slug = SlugifyHelper.GenerateSlug(value);
        }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Summary { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ImageThumbUrl { get; set; }

        [Required]
        [AllowHtml]
        [Column(TypeName = "text")]
        public string Content { get; set; }

        [Required]
        [Index]
        [DefaultValue(ProjectStatus.Disabled)]
        public ProjectStatus Status { get; set; }

        [Required]
        [Index]
        [Display(Name = "Creation date")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Index]
        [Display(Name = "Updated date")]
        public DateTime UpdatedAt { get; set; }
        
        public ICollection<TagModel> Tags { get; set; }
        public ICollection<ProjectCategoryModel> Categories { get; set; }
    }
}