namespace LearningSystem.Models.EntityModels
{
    using System.Collections.Generic;

    public class Student
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
