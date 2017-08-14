using System.Collections.Generic;
using System.Web.Mvc;

namespace Portfolio.Helpers
{
    public class ProjectHelper
    {
        public static IEnumerable<SelectListItem> GetImagesList()
        {
            List<SelectListItem> thumbList = new List<SelectListItem>();

            for (int i = 0; i <= 11; i++)
            {
                thumbList.Add(new SelectListItem
                {
                    Text = $"tech{i}.jpg",
                    Value = $"/Content/Images/Project/tech{i}.jpg"
                });
            }

            return thumbList;
        }
    }
}