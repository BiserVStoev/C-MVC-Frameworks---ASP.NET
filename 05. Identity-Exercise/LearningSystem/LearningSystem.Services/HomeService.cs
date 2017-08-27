namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using AutoMapper;
    using LearningSystem.Models.EntityModels;
    using LearningSystem.Models.ViewModels.Courses;

    public class HomeService : Service
    {
        public IEnumerable<CourseVm> GetAllCourses()
        {
            IEnumerable<Course> courses = this.Context.Courses;
            IEnumerable<CourseVm> coursesVm = Mapper.Map<IEnumerable<Course>, IEnumerable<CourseVm>>(courses);

            return coursesVm;
        }
    }
}
