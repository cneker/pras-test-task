using PRAS.DataTransferObjects;

namespace PRAS.ViewModel
{
    public class AdminViewModel
    {
        public IEnumerable<NewsDto> AllNews { get; set; }
        public bool HasNext { get; set; }
    }
}
