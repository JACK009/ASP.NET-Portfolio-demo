using Portfolio.Models;
using System.Web;

namespace Portfolio.Helpers
{
    public static class StatusHtmlHelper
    {
        public static IHtmlString CreateStatus(ProjectModel projectModel)
        {
            return projectModel.Status == ProjectModel.ProjectStatus.Enabled ? 
                new HtmlString("<span class=\"text-success\">Enabled</span>") : 
                new HtmlString("<span class=\"text-warning\">Disabled</span>");
        }
    }
}