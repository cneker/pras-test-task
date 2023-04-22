using PRAS.RequestParameters;

namespace PRAS.DataTransferObjects
{
    public class NewsPagingDto
    {
        public IEnumerable<NewsDto> AllNews { get; set; }
        public MetaData MetaData { get; set; }
    }
}
