namespace HledacFirmy.WebMvcApp.Models
{
    public class RssItemSearchModel
    {
        public int? FeedId { get; set; }
        public string? SearchText { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
