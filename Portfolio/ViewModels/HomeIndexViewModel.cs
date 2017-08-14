using Portfolio.Models;
using System.Collections.Generic;

namespace Portfolio.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<ProjectModel> LatestProjects { get; set; }
        public IEnumerable<ProjectModel> FeaturedProjects { get; set; }
    }
}