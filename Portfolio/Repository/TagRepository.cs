using Portfolio.Models;
using Portfolio.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Portfolio.Repository
{
    public class TagRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TagRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }        

        public IEnumerable<TagModel> GetTagsOrderNameAsc()
        {
            return _applicationDbContext.Tags.OrderBy(m => m.Name).ToList();
        }

        public IEnumerable<TagModel> GetTagsByProjectId(int projectId)
        {
            return _applicationDbContext.Tags
               .Where(m => m.Projects.Any(c => c.Id == projectId))
               .Include(m => m.Projects)
               .OrderByDescending(m => m.Name).ToList();
        }

        public void Add(string tag)
        {
            TagModel tagModel = new TagModel()
            {
                Name = tag
            };
            _applicationDbContext.Tags.Add(tagModel);
        }

        public IEnumerable<AssignedTagsViewModel> GetAssignedProjectModelRelatedTags(ProjectModel projectModel)
        {
            var tags = GetTagsOrderNameAsc();
            var projectTags = new HashSet<int>(projectModel.Tags.Select(c => c.Id));
            var assignedTagsviewModelList = new List<AssignedTagsViewModel>();
            foreach (TagModel tag in tags)
            {
                AssignedTagsViewModel assignedTagViewModel = new AssignedTagsViewModel
                {
                    Name = tag.Name,
                    Id = tag.Id,
                    Assigned = projectTags.Contains(tag.Id)
                };
                assignedTagsviewModelList.Add(assignedTagViewModel);
            }

            return assignedTagsviewModelList;
        }
    }
}