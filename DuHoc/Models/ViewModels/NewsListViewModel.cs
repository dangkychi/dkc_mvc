namespace DuHoc.Models.ViewModels
{
    public class NewsListViewModel
    {
        public IEnumerable<NewsPost> NewsPosts { get; set; } = Enumerable.Empty<NewsPost>();
        public IEnumerable<ParentComment> ParentComments { get; set; } = Enumerable.Empty<ParentComment>();
        public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
        public bool ShowAllComments { get; set; } = true;

    }
}
