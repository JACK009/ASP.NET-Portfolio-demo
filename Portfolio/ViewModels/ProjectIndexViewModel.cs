using PagedList;
using Portfolio.Models;

namespace Portfolio.ViewModels
{
    public class ProjectIndexViewModel
    {
        public string CurrentSort { get; set; }
        public string TitleSort { get; set; }
        public string CreatedAtSort { get; set; }
        public string UpdatedAtSortParam { get; set; }
        public string CurrentSearch { get; set; }
        public IPagedList<ProjectModel> PagedList { get; set; }
    }
}