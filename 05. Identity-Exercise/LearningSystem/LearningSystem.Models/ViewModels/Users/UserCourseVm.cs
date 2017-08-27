namespace LearningSystem.Models.ViewModels.Users
{
    using LearningSystem.Models.Enums;

    public class UserCourseVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Grade Grade { get; set; }
    }
}
