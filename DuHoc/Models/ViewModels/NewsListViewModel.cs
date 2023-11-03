namespace DuHoc.Models.ViewModels
{
    public class NewsListViewModel
    {
        public IEnumerable<NewsPost> NewsPosts { get; set; } = Enumerable.Empty<NewsPost>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
