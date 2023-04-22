using PRAS.Contracts.Repositories;

namespace PRAS.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private INewsRepository _newsRepository;
        private IUserRepository _userRepository;
        private readonly AppDbContext _appDbContext;

        public RepositoryManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public INewsRepository NewsRepository
        {
            get
            {
                if (_newsRepository == null)
                {
                    _newsRepository = new NewsRepository(_appDbContext);
                }
                return _newsRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_appDbContext);
                }
                return _userRepository;
            }
        }

        public async Task SaveAsync() =>
            await _appDbContext.SaveChangesAsync();
    }
}
