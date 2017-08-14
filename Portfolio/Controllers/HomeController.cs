using Portfolio.Helpers;
using Portfolio.Models;
using Portfolio.Repository;
using Portfolio.ViewModels;
using System.Web.Mvc;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectRepository _projectRepository;

        public HomeController()
        {
            var applicationDbContext = new ApplicationDbContext();
            _projectRepository = new ProjectRepository(applicationDbContext);
        }

        public ActionResult Index()
        {
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                LatestProjects = _projectRepository.GetLatestEnabledProjectsWithTags(),
                FeaturedProjects = _projectRepository.GetFeaturedEnabledProjectsWithTagsAndCategories()
            };

            return View(homeIndexViewModel);
        }
        
        [HttpGet]
        public ActionResult Contact()
        {
            EmailFormViewModel emailFormViewModel = new EmailFormViewModel();
            return View(emailFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(EmailFormViewModel emailFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(emailFormViewModel);
            }

            MailHelper.SendContactMessage(
                emailFormViewModel.EmailModel.Message,
                emailFormViewModel.EmailModel.FromEmail,
                emailFormViewModel.EmailModel.FromName,
                "A message from the portfolio website"
            );
            
            ModelState.Clear();
            emailFormViewModel = new EmailFormViewModel
            {
                MessageSent = true
            };

            return View(emailFormViewModel);
        }
    }
}