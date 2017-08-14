namespace Portfolio.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                        Slug = c.String(nullable: false, maxLength: 100, unicode: false),
                        Summary = c.String(nullable: false, maxLength: 255, unicode: false),
                        ImageThumbUrl = c.String(nullable: false, maxLength: 255, unicode: false),
                        Content = c.String(nullable: false, unicode: false, storeType: "text"),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Title)
                .Index(t => t.Slug, unique: true)
                .Index(t => t.Status)
                .Index(t => t.CreatedAt)
                .Index(t => t.UpdatedAt);
            
            CreateTable(
                "dbo.ProjectCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Slug = c.String(nullable: false, maxLength: 100, unicode: false),
                        Summary = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name)
                .Index(t => t.Slug, unique: true);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectCategoryModelProjectModels",
                c => new
                    {
                        ProjectCategoryModel_Id = c.Int(nullable: false),
                        ProjectModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectCategoryModel_Id, t.ProjectModel_Id })
                .ForeignKey("dbo.ProjectCategories", t => t.ProjectCategoryModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectModel_Id, cascadeDelete: true)
                .Index(t => t.ProjectCategoryModel_Id)
                .Index(t => t.ProjectModel_Id);
            
            CreateTable(
                "dbo.TagModelProjectModels",
                c => new
                    {
                        TagModel_Id = c.Int(nullable: false),
                        ProjectModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagModel_Id, t.ProjectModel_Id })
                .ForeignKey("dbo.Tags", t => t.TagModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectModel_Id, cascadeDelete: true)
                .Index(t => t.TagModel_Id)
                .Index(t => t.ProjectModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TagModelProjectModels", "ProjectModel_Id", "dbo.Projects");
            DropForeignKey("dbo.TagModelProjectModels", "TagModel_Id", "dbo.Tags");
            DropForeignKey("dbo.ProjectCategoryModelProjectModels", "ProjectModel_Id", "dbo.Projects");
            DropForeignKey("dbo.ProjectCategoryModelProjectModels", "ProjectCategoryModel_Id", "dbo.ProjectCategories");
            DropIndex("dbo.TagModelProjectModels", new[] { "ProjectModel_Id" });
            DropIndex("dbo.TagModelProjectModels", new[] { "TagModel_Id" });
            DropIndex("dbo.ProjectCategoryModelProjectModels", new[] { "ProjectModel_Id" });
            DropIndex("dbo.ProjectCategoryModelProjectModels", new[] { "ProjectCategoryModel_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tags", new[] { "Name" });
            DropIndex("dbo.ProjectCategories", new[] { "Slug" });
            DropIndex("dbo.ProjectCategories", new[] { "Name" });
            DropIndex("dbo.Projects", new[] { "UpdatedAt" });
            DropIndex("dbo.Projects", new[] { "CreatedAt" });
            DropIndex("dbo.Projects", new[] { "Status" });
            DropIndex("dbo.Projects", new[] { "Slug" });
            DropIndex("dbo.Projects", new[] { "Title" });
            DropTable("dbo.TagModelProjectModels");
            DropTable("dbo.ProjectCategoryModelProjectModels");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tags");
            DropTable("dbo.ProjectCategories");
            DropTable("dbo.Projects");
        }
    }
}
