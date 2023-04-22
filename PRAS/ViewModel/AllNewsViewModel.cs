using PRAS.DataTransferObjects;

namespace PRAS.ViewModel
{
    public class AllNewsViewModel
    {
        public IEnumerable<NewsDto> AllNews { get; set; }
        public bool HasNext { get; set; }
    }
}
