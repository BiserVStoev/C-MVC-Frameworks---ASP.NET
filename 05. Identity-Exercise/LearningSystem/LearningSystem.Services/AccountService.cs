namespace LearningSystem.Services
{
    using LearningSystem.Models.EntityModels;

    public class AccountService : Service
    {
        public void CreateStudent(string userId)
        {
            Student student = new Student();
            ApplicationUser user = this.Context.Users.Find(userId);
            student.User = user;
            student.Name = user.Name;
            this.Context.Students.Add(student);
            this.Context.SaveChanges();
        }
    }
}
