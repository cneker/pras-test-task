namespace PRAS.Contracts.Repositories
{
    public interface IRepositoryManager
    {
        INewsRepository NewsRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}
