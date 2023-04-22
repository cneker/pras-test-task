using AutoMapper;
using PRAS.Contracts.Repositories;
using PRAS.Contracts.Services;
using PRAS.DataTransferObjects;
using PRAS.Exceptions;

namespace PRAS.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AuthenticationService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<UserDto> SignInAsync(UserForAuthenticationDto userDto)
        {
            var user = await _repositoryManager.UserRepository.GetUserByEmailAsync(userDto.Email);
            if (user == null)
                throw new CredentialsException();

            if (!VerifyPasswordHash(userDto.Password, user.PasswordHash))
                throw new CredentialsException();

            var userForReturn = _mapper.Map<UserDto>(user);

            return userForReturn;
        }

        private bool VerifyPasswordHash(string password, string passwordHash) =>
            BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
