using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Portfolio.Repository
{
    public class ProjectRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProjectRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<ProjectModel> GetLatestEnabledProjectsWithTags()
        {
            return _applicationDbContext.Projects
                .Where(m => m.Status == ProjectModel.ProjectStatus.Enabled)
                .Include(m => m.Tags)
                .OrderByDescending(m => m.CreatedAt)
                .ThenBy(m => m.Title)
                .Take(3)
                .ToList();
        }

        public IEnumerable<ProjectModel> GetSearchedProjectModels(string sort, string search)
        {
            var projects = _applicationDbContext.Projects.Select(m => m);

            switch (sort)
            {
                case "title_desc":
                    projects = projects.OrderByDescending(m => m.Title);
                    break;
                case "created_at":
                    projects = projects.OrderBy(m => m.CreatedAt);
                    break;
                case "created_at_desc":
                    projects = projects.OrderByDescending(m => m.CreatedAt);
                    break;
                case "updated_at":
                    projects = projects.OrderBy(m => m.UpdatedAt);
                    break;
                case "updated_at_desc":
                    projects = projects.OrderByDescending(m => m.UpdatedAt);
                    break;
                default:
                    projects = projects.OrderBy(m => m.Title);
                    break;
            }

            if (!String.IsNullOrEmpty(search))
            {
                projects = projects.Where(m => m.Title.Contains(search));
            }

            return projects;
        }

        public IEnumerable<ProjectModel> GetFeaturedEnabledProjectsWithTagsAndCategories()
        {
            return _applicationDbContext.Projects
                .Where(
                    m => m.Status == ProjectModel.ProjectStatus.Enabled &&
                    m.Categories.Any(c => c.Name == "Featured"))
                .Include(m => m.Tags)
                .Include(m => m.Categories)
                .OrderByDescending(m => m.CreatedAt)
                .ThenBy(m => m.Title)
                .Take(3)
                .ToList();
        }

        public ProjectModel Find(int id)
        {
            return _applicationDbContext.Projects.Find(id);
        }

        public ProjectModel FindProjectByIdWithTagsAndCategories(int id)
        {
            return _applicationDbContext.Projects
                .Include(m => m.Tags)
                .Include(m => m.Categories)
                .Single(m => m.Id == id);
        }

        public ProjectModel FindProjectBySlugWithTagsAndCategories(string slug)
        {
            return _applicationDbContext.Projects
                .Include(m => m.Tags)
                .Include(m => m.Categories)
                .Single(m => m.Slug == slug);
        }
        
        //todo
        public void Add(ProjectModel projectModel)
        {
           _applicationDbContext.Projects.Add(projectModel);
        }

        public void InverseStatus(ProjectModel projectModel)
        {
            projectModel.Status = 
                projectModel.Status == ProjectModel.ProjectStatus.Disabled ? 
                ProjectModel.ProjectStatus.Enabled : 
                ProjectModel.ProjectStatus.Disabled;

            projectModel.UpdatedAt = DateTime.Now;
        }

        public void Delete(int id)
        {
            ProjectModel projectModel = new ProjectModel { Id = id };
            _applicationDbContext.Entry(projectModel).State = EntityState.Deleted;
        }

        public void AddCategories(ProjectModel projectModel, int[] categories)
        {
             projectModel.Categories = _applicationDbContext.ProjectsCategories.Where(m => categories.Contains(m.Id)).ToList();
        }

        public void AddTags(ProjectModel projectModel, int[] tags)
        {
            projectModel.Tags = _applicationDbContext.Tags.Where(m => tags.Contains(m.Id)).ToList();
        }

        public void UpdateCategories(ProjectModel projectModel, string[] categories)
        {
            IEnumerable<ProjectCategoryModel> projectCategoryModels = _applicationDbContext.ProjectsCategories.ToList();

            if (categories == null)
            {
                projectModel.Categories.Clear();
                return;
            }

            var projectCategories = new HashSet<string>(categories);
            var currentProjectCategories = projectModel.Categories.Select(c => c.Id);

            foreach (ProjectCategoryModel projectCategory in projectCategoryModels)
            {
                //if posted tag is not yet added to project tags collection
                if (projectCategories.Contains(projectCategory.Id.ToString()))
                {
                    if (!currentProjectCategories.Contains(projectCategory.Id))
                    {
                        projectModel.Categories.Add(projectCategory);
                    }
                }
                //else we remove the tag
                else if (currentProjectCategories.Contains(projectCategory.Id))
                {
                    projectModel.Categories.Remove(projectCategory);
                }
            }
        }

        public void UpdateTags(ProjectModel projectModel, string[] tags)
        {
            IEnumerable<TagModel> tagModels = _applicationDbContext.Tags.ToList();

            if (tags == null)
            {
                projectModel.Tags.Clear();
                return;
            }

            var projectTags = new HashSet<string>(tags);
            var currentProjectTags = new HashSet<int>(projectModel.Tags.Select(m => m.Id));

            foreach (TagModel tag in tagModels)
            {
                //if contain matches the looped tag
                if (projectTags.Contains(tag.Id.ToString()))
                {
                    // and is not yet added to project tags collection
                    if (!currentProjectTags.Contains(tag.Id))
                    {
                        projectModel.Tags.Add(tag);
                    }
                }
                //else we remove the tag
                else if (currentProjectTags.Contains(tag.Id))
                {
                    projectModel.Tags.Remove(tag);
                }
            }
        }
    }
}