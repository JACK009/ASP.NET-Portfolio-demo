using Portfolio.Models;
using Portfolio.Repository;
using Portfolio.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Portfolio.Controllers
{
    public class ProjectCategoryController : Controller
    {
        private readonly ProjectCategoryRepository _projectCategoryRepository;
        private readonly ProjectRepository _projectRepository;
        private readonly ApplicationDbContext _applicationDbContext;

        public ProjectCategoryController()
        {
            _applicationDbContext = new ApplicationDbContext();
            _projectCategoryRepository = new ProjectCategoryRepository(_applicationDbContext);
            _projectRepository = new ProjectRepository(_applicationDbContext);
        }

        public JsonResult GetProjectCategories()
        {
            return Json(_projectCategoryRepository.GetProjectCategoriesOrderNameAsc(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostProjectCategory(string newProjectCategory)
        {
            _projectCategoryRepository.Add(newProjectCategory);
            _applicationDbContext.SaveChanges();

            return Json(string.Empty);
        }

        public JsonResult GetAssignedProjectModelRelatedProjectCategories(int projectId)
        {
            ProjectModel projectModel = _projectRepository.FindProjectByIdWithTagsAndCategories(projectId);
            IEnumerable<AssignedProjectCategoriesViewModel> assignedProjectCategoriesViewModel = _projectCategoryRepository.GetAssignedProjectModelRelatedProjectCategories(projectModel);

            return Json(assignedProjectCategoriesViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}
