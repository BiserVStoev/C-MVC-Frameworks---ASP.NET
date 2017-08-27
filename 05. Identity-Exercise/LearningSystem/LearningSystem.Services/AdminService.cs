namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using AutoMapper;
    using LearningSystem.Models.EntityModels;
    using LearningSystem.Models.ViewModels.Admin;
    using LearningSystem.Models.ViewModels.Courses;

    public class AdminService : Service
    {
        public AdminPageVm GetAdminPage()
        {
            AdminPageVm page = new AdminPageVm();
            IEnumerable<Course> courses = this.Context.Courses;
            IEnumerable<Student> students = this.Context.Students;

            IEnumerable<CourseVm> courseVms = Mapper.Map<IEnumerable<Course>, IEnumerable<CourseVm>>(courses);
            IEnumerable<AdminPageUserVm> usersVm = Mapper.Map<IEnumerable<Student>, IEnumerable<AdminPageUserVm>>(students);

            page.Users = usersVm;
            page.Courses = courseVms;

            return page;
        }
    }
}
