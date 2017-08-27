namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AutoMapper;
    using LearningSystem.Models.BindingModels;
    using LearningSystem.Models.EntityModels;
    using LearningSystem.Models.ViewModels.Users;

    public class UsersService : Service
    {
        public Student GetCurrentStudent(string username)
        {
            var user = this.Context.Users.FirstOrDefault(applicationUser => applicationUser.UserName == username);
            Student student = this.Context.Students.FirstOrDefault(student1 => student1.User.Id == user.Id);

            return student;
        }

        public void EnrollStudentInCourse(int courseId, int studentId)
        {
            Course wantedCourse = this.Context.Courses.Find(courseId);
            Student currentStudent = this.Context.Students.Find(studentId);
            currentStudent.Courses.Add(wantedCourse);
            this.Context.SaveChanges();
        }

        public ProfileVm GetProfileVm(string username)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(user => user.UserName == username);
            ProfileVm vm = Mapper.Map<ApplicationUser, ProfileVm>(currentUser);
            Student currentStudent = this.Context.Students.FirstOrDefault(student => student.User.Id == currentUser.Id);
            vm.EnrolledCourses = Mapper.Map<IEnumerable<Course>, IEnumerable<UserCourseVm>>(currentStudent.Courses);

            return vm;
        }

        public EditUserVm GetEditVm(string username)
        {
            ApplicationUser user =
                this.Context.Users.FirstOrDefault(applicationUser => applicationUser.UserName == username);
            EditUserVm vm = Mapper.Map<ApplicationUser, EditUserVm>(user);

            return vm;
        }

        public void EditUser(EditUserBm bind, string currentUsername)
        {
            ApplicationUser user =
                 this.Context.Users.FirstOrDefault(applicationUser => applicationUser.UserName == currentUsername);
            user.Name = bind.Name;
            user.Email = bind.Email;
            this.Context.SaveChanges();
        }
    }
}
