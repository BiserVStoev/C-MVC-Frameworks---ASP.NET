namespace LearningSystem.Services
{
    using AutoMapper;
    using LearningSystem.Models.EntityModels;
    using LearningSystem.Models.ViewModels.Courses;

    public class CoursesService : Service
    {
        public DetailsCourseVm GetDetails(int id)
        {
            Course course = this.Context.Courses.Find(id);
            if (course == null)
            {
                return null;
            }

            DetailsCourseVm vm = Mapper.Map<Course, DetailsCourseVm>(course);

            return vm;
        }
    }
}
