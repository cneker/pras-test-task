namespace PRAS.Models
{
    public class News
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }
    }
}
