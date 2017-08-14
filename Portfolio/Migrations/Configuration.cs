namespace Portfolio.Migrations
{
    using Helpers;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Migrations;
    using System.Net;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var projects = new List<ProjectModel>();
            var tags = new List<TagModel>();
            var projectCategories = new List<ProjectCategoryModel>();
            DateTime dateTimeStart = new DateTime(2017, 1, 1);
            int dateTimeRange = (DateTime.Today - dateTimeStart).Days;
            Random random = new Random();

            string[] tagWords = {
                     "PHP", "UX", "YII", "Symfony", "ASP.NET", "CSS", "Javascript",
                     "E-commerce", "Frontend", "Backend", "Responsive", "CMS",
                     "Photoshop", "Typography", "API", "HTML", "JQuery"
                 };

            string[] categoryWords = {
                     "Coding", "Design", "Mobile", "Graphics", "UX", "Featured"
                 };

            string projectContent = @"
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.Nullam eget mi massa. Phasellus quis imperdiet purus. Pellentesque vitae dui eget mauris eleifend tempor.Sed laoreet cursus ligula, sit amet tincidunt ipsum rutrum et.Etiam euismod, ligula ut porta iaculis, leo tortor vehicula ligula, eu ultrices lorem quam auctor nibh.Morbi vestibulum ligula sem, sed auctor turpis aliquam ac. Phasellus pellentesque lacus vel sem sagittis feugiat.Donec maximus lobortis semper. Nunc nec erat sed tortor luctus iaculis.Nulla facilisi.
                </p><p>Vivamus quis dui cursus, convallis nunc nec, pharetra ante. Duis commodo sit amet neque in luctus.Nam condimentum tincidunt sollicitudin. Curabitur id pulvinar mi. Suspendisse hendrerit consectetur nunc, sit amet tempor leo mollis id.Ut faucibus sagittis lobortis. Proin interdum vel leo vel imperdiet. Nullam at malesuada sem, at vestibulum orci. Nam consequat libero non nisi cursus rhoncus.Suspendisse a velit id enim rhoncus bibendum vitae vitae mauris.
                </p><p>Cras odio nulla, placerat in leo sit amet, condimentum auctor lectus.Donec posuere libero in quam accumsan, eget fermentum nisl fermentum.Integer cursus convallis bibendum. Nullam ac tortor ultricies, semper nulla tristique, imperdiet dui.Sed mattis ligula ac nibh posuere volutpat.Proin tristique est mauris, ut sagittis nibh porta id. Vivamus at magna lacinia, rhoncus est ut, iaculis nisi.Suspendisse hendrerit, ante vitae tempus commodo, sapien metus aliquam tellus, quis posuere eros dolor non mi.Nunc sollicitudin lacus ut magna tincidunt, sit amet tincidunt velit mattis.
                </p><h3>Subtitle</h3><p>Nullam mattis tellus quis elementum mollis.Suspendisse et mi ligula. Proin risus lectus, mollis non dictum sit amet, tempus in diam.Mauris ultrices arcu purus, vitae consectetur leo condimentum sed. Curabitur tempus purus id ipsum hendrerit malesuada.Sed bibendum justo id leo blandit condimentum.Vivamus vel sollicitudin neque. Duis congue enim vitae condimentum rutrum. Nullam sed nulla posuere, eleifend odio vel, fringilla augue.
                </p><p>Maecenas id eleifend elit.Sed laoreet varius lorem eget lacinia. Sed mollis mollis fringilla. Pellentesque non purus et nunc fermentum elementum nec sit amet mauris.Fusce faucibus arcu sit amet dictum pharetra.Ut volutpat aliquam ante ac dignissim. Donec ac nulla arcu.
                </p>";

            //add tags
            for (int i = 0; i < tagWords.Length; i++)
            {
                TagModel tag = new TagModel
                {
                    Name = tagWords[i]
                };

                tags.Add(tag);
            }

            foreach (TagModel tag in tags)
            {
                context.Tags.AddOrUpdate(x => x.Id, tag);
               
            }
         
            //add categories
            for (int i = 0; i < categoryWords.Length; i++)
            {
                ProjectCategoryModel projectCategory = new ProjectCategoryModel
                {
                    Name = categoryWords[i],
                    Slug = SlugifyHelper.GenerateSlug(categoryWords[i]),
                    Summary = "summary of category \"" + categoryWords[i] +
                        "\" Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Aliquam eget scelerisque enim. Nam a lorem quis diam interdum " +
                        "egestas. Class aptent taciti sociosqu ad litora torquent per conubia nostra, " +
                        "per inceptos himenaeos."
                };

                projectCategories.Add(projectCategory);
            }

            foreach (ProjectCategoryModel projectCategory in projectCategories)
            {
                context.ProjectsCategories.AddOrUpdate(x => x.Id, projectCategory);
              
            }

            //add projects
            for (int i = 0; i < 100; i++)
            {
                //see ProjectModel status consts for range
                int projectStatus = random.Next(0, 2);
                int projectImage = random.Next(0, 11);
                DateTime dateTime = dateTimeStart.AddDays(random.Next(dateTimeRange));

                ProjectModel Project = new ProjectModel
                {
                    Title = "Project " + i,
                    Slug = SlugifyHelper.GenerateSlug("project " + i),
                    Status = (ProjectModel.ProjectStatus)projectStatus,
                    // ImageThumbUrl = "http://placehold.it/400x220", //use this for neutral imgs.
                    // ImageThumbUrl = "https://placeimg.com/400/220/tech", //use this for ramdom tech img
                    ImageThumbUrl = "/Content/Images/Project/tech" + projectImage + ".jpg", //from https://placeimg.com/
                    Summary = "Summary of project " + i + 
                        " Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                        "Aliquam eget scelerisque enim. Nam a lorem quis diam interdum " +
                        "egestas. Class aptent taciti sociosqu ad litora torquent per conubia nostra, " +
                        "per inceptos himenaeos.",
                    Content = WebUtility.HtmlEncode("<h3>WYSIWYG content of project " + i + "</h3> " + projectContent),
                    CreatedAt = dateTime,
                    UpdatedAt = dateTime
                };

                Project.Tags = new Collection<TagModel>();
                Project.Categories = new Collection<ProjectCategoryModel>();

                int tagAmount = random.Next(2, 8);
                int projectCategoryAmount = random.Next(1, 3);

                for (int j = 0; j < tagAmount; j++)
                {
                    Project.Tags.Add(tags[random.Next(0, tagWords.Length)]);
                }

                for (int j = 0; j < projectCategoryAmount; j++)
                {
                    Project.Categories.Add(projectCategories[random.Next(0, categoryWords.Length)]);
                }

                projects.Add(Project);
            }

            foreach(ProjectModel project in projects)
            {
                context.Projects.AddOrUpdate(x => x.Id, project);
            }

            context.SaveChanges();
        }
    }
}
