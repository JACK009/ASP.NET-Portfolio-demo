using Portfolio.Models;
using System.Linq;

namespace Portfolio.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public ProjectModel ProjectModel { get; set; }

        public string GetCategories()
        {
            string categories = "";

            if (ProjectModel.Categories.Count() != 0)
            {
                categories = "In ";
                for (int i = 0; i < ProjectModel.Categories.Count(); i++)
                {
                    if (i > 0)
                    {
                        categories = categories + ", ";
                    }

                    categories = categories + ProjectModel.Categories.ToList()[i].Name;
                }
            }

            return categories;
        }
    }
}