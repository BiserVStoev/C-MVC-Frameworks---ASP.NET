namespace TechJunk.Models.ViewModels.Interest
{
    using TechJunk.Models.Enums;

    public class InterestsIndexVm
    {
        public PagedList.IPagedList<InterestVm> Interests { get; set; }

        public Category Category { get; set; }
    }
}
