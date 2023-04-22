namespace PRAS.DataTransferObjects
{
    public class NewsDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string AuthorEmail { get; set; }
    }
}
