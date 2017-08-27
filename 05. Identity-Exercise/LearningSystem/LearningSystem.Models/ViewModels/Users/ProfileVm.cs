namespace LearningSystem.Models.ViewModels.Users
{
    using System;
    using System.Collections.Generic;

    public class ProfileVm
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public IEnumerable<UserCourseVm> EnrolledCourses { get; set; }
    }
}
