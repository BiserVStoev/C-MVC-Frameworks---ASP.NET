namespace LearningSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LearningSystem.Models.EntityModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<LearningSystemContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LearningSystemContext context)
        {
            if (!context.Roles.Any(role => role.Name == "Student"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Student");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Admin");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "Trainer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Trainer");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "BlogAuthor"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("BlogAuthor");
                manager.Create(role);
            }

            context.Courses.AddOrUpdate(course => course.Name, new Course[]
            {
                new Course()
                {
                    Name = "Курс Programming Fundamentals - септември 2017",
                    Description = "Курсът \"Programming Fundamentals\" дава начални умения по програмиране.",
                    StartDate = new DateTime(2017, 03, 23),
                    EndDate = new DateTime(2017, 05, 23)
                },
                new Course()
                {
                    Name = "Курс Software Technologies - септември 2017",
                    Description = "Курсът \"Software Technologies\" дава начални умения по най-използваните технологии.",
                    StartDate = new DateTime(2017, 03, 25),
                    EndDate = new DateTime(2017, 05, 30)
                },
                new Course()
                {
                    Name = "Курс C# MVC Frameworks - ASP.NET - юни 2017",
                    Description = "Курсът \"ASP.NET MVC\" ще ви научи как да изграждате съвременни уеб приложения",
                    StartDate = new DateTime(2017, 06, 04),
                    EndDate = new DateTime(2017, 09, 10)
                },
                new Course()
                {
                    Name = "Курс Java OOP Advanced - септември 2017",
                    Description = "Курсът \"Object Oriented Programming Advanced с Java\" надгражда курсът Object Oriented Programming с Java",
                    StartDate = new DateTime(2017, 09, 23),
                    EndDate = new DateTime(2017, 12, 23)
                }
            });
        }
    }
}
