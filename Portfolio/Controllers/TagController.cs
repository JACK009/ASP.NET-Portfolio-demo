using Portfolio.Models;
using Portfolio.Repository;
using Portfolio.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Portfolio.Controllers
{
    public class TagController : Controller
    {
        private readonly TagRepository _tagRepository;
        private readonly ProjectRepository _projectRepository;
        private readonly ApplicationDbContext _applicationDbContext;

        public TagController()
        {
            _applicationDbContext = new ApplicationDbContext();
            _tagRepository = new TagRepository(_applicationDbContext);
            _projectRepository = new ProjectRepository(_applicationDbContext);
        }
        public JsonResult GetTags()
        {
            return Json(_tagRepository.GetTagsOrderNameAsc(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostTag(string newTag)
        {
            _tagRepository.Add(newTag);
            _applicationDbContext.SaveChanges();

            return Json(string.Empty);
        }

        public JsonResult GetAssignedProjectModelRelatedTags(int projectId)
        {
            ProjectModel projectModel = _projectRepository.FindProjectByIdWithTagsAndCategories(projectId);
            IEnumerable<AssignedTagsViewModel> assignedTagsViewModel = _tagRepository.GetAssignedProjectModelRelatedTags(projectModel);

            return Json(assignedTagsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}
