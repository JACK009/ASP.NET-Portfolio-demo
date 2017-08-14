using Portfolio.Helpers;
using Portfolio.Models;
using Portfolio.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Portfolio.Repository
{
    public class ProjectCategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProjectCategoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<ProjectCategoryModel> GetProjectCategoriesOrderNameAsc()
        {
            return _applicationDbContext.ProjectsCategories
                .OrderBy(m => m.Name)
                .ToList();
        }
        
        public void Add(string projectCategory)
        {
            ProjectCategoryModel projectCategoryModel = new ProjectCategoryModel()
            {
                Name = projectCategory,
                Slug = SlugifyHelper.GenerateSlug(projectCategory)
            };

            _applicationDbContext.ProjectsCategories.Add(projectCategoryModel);
        }

        public IEnumerable<AssignedProjectCategoriesViewModel> GetAssignedProjectModelRelatedProjectCategories(ProjectModel projectModel)
        {
            var categories = GetProjectCategoriesOrderNameAsc();
            var projectCategories = new HashSet<int>(projectModel.Categories.Select(c => c.Id));
            var assignedCategoriesviewModelList = new List<AssignedProjectCategoriesViewModel>();

            foreach (ProjectCategoryModel projectCategory in categories)
            {
                AssignedProjectCategoriesViewModel assignedProjectCategoriesViewModel = new AssignedProjectCategoriesViewModel
                {
                    Name = projectCategory.Name,
                    Id = projectCategory.Id,
                    Assigned = projectCategories.Contains(projectCategory.Id)
                };
                assignedCategoriesviewModelList.Add(assignedProjectCategoriesViewModel);
            }

            return assignedCategoriesviewModelList;
        }
    }
}