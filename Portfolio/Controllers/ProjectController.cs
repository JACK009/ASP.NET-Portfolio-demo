using PagedList;
using Portfolio.Helpers;
using Portfolio.Models;
using Portfolio.Repository;
using Portfolio.ViewModels;
using System;
using System.Net;
using System.Web.Mvc;

namespace Portfolio.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ProjectRepository _projectRepository;

        public ProjectController()
        {
            _applicationDbContext = new ApplicationDbContext();
            _projectRepository = new ProjectRepository(_applicationDbContext);
        }

        public ActionResult Index(string sort, string currentSearch, string search, int? page)
        {
            ProjectIndexViewModel projectIndexViewModel = new ProjectIndexViewModel()
            {
                CurrentSort = sort,
                TitleSort = String.IsNullOrEmpty(sort) ? "title_desc" : "",
                CreatedAtSort = sort == "created_at" ? "created_at_desc" : "created_at",
                UpdatedAtSortParam = sort == "updated_at" ? "updated_at_desc" : "updated_at",
                CurrentSearch = currentSearch
            };

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = projectIndexViewModel.CurrentSearch;
            }

            projectIndexViewModel.CurrentSearch = search;
            projectIndexViewModel.PagedList = _projectRepository
                .GetSearchedProjectModels(sort, search)
                .ToPagedList((page ?? 1), 10);
            
            return View(projectIndexViewModel);
        }
        
        public ActionResult Details(int id = -1, string slug = null)
        {
            ProjectDetailsViewModel detailsViewModel = new ProjectDetailsViewModel();
            
            if(!String.IsNullOrEmpty(id.ToString()) && id != -1)
            {
                detailsViewModel.ProjectModel = _projectRepository.FindProjectByIdWithTagsAndCategories(id);
                return View(detailsViewModel);
            }

            if (!String.IsNullOrEmpty(slug))
            {
                detailsViewModel.ProjectModel = _projectRepository.FindProjectBySlugWithTagsAndCategories(slug);
                return View(detailsViewModel);
            }

            return HttpNotFound();
        }

        public ActionResult Create()
        {
            ProjectModel projectModel = new ProjectModel();
            ViewBag.ThumbList = ProjectHelper.GetImagesList();

            return View(projectModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            ProjectModel project = new ProjectModel
            {
                Title = collection.Get("title"),
                Content = WebUtility.HtmlEncode(collection.Get("content")),
                Slug = collection.Get("slug"),
                Status = (ProjectModel.ProjectStatus)Enum.Parse((typeof(ProjectModel.ProjectStatus)), collection.Get("status")),
                ImageThumbUrl = collection.Get("imageThumbUrl"),
                Summary = collection.Get("summary"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            if (collection.GetValues("Categories[]") != null)
            {
                _projectRepository.AddCategories(project, Array.ConvertAll(collection.GetValues("Categories[]"), int.Parse));
            }
 
            if (collection.GetValues("Tags[]") != null)
            {
               _projectRepository.AddTags(project, Array.ConvertAll(collection.GetValues("Tags[]"), int.Parse));
            }

            _projectRepository.Add(project);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(int id)
        {
            ProjectModel projectModel = _projectRepository.FindProjectByIdWithTagsAndCategories(id);
            ViewBag.ThumbList = ProjectHelper.GetImagesList();

            if (projectModel == null)
            {
                return HttpNotFound();
            }

            return View(projectModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string[] tags, string[] categories)
        {
            ProjectModel projectModel = _projectRepository.FindProjectByIdWithTagsAndCategories(id);

            if (ModelState.IsValid)
            {
                projectModel.UpdatedAt = DateTime.Now;

                if (TryUpdateModel(projectModel, "", new[] { "Title", "Slug", "Summary", "ImageThumbUrl", "Content", "Status" }))
                {
                    projectModel.Content = WebUtility.HtmlEncode(projectModel.Content);
                    _projectRepository.UpdateTags(projectModel, tags);
                    _projectRepository.UpdateCategories(projectModel, categories);
                    _applicationDbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            
            return View(projectModel);
        }
        
        public ActionResult Delete(int id)
        {
            _projectRepository.Delete(id);
            _applicationDbContext.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult ChangeStatus(int id)
        {
            var project = _projectRepository.Find(id);

            if (project != null)
            {
                _projectRepository.InverseStatus(project);
                _applicationDbContext.SaveChanges();
            }
            else
            {
                throw new NullReferenceException("Project not found.");
            }
         
            return RedirectToAction("Details", new {id});
        }
    }
}
