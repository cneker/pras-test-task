using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRAS.DataTransferObjects;
using PRAS.Services;
using System.Security.Claims;
using IAuthenticationService = PRAS.Contracts.Services.IAuthenticationService;
using ValidationException = PRAS.Exceptions.ValidationException;

namespace PRAS.Controllers
{
    [AllowAnonymous]
    [Route("/authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IValidator<UserForAuthenticationDto> _validator;

        public AuthenticationController(IAuthenticationService authenticationService, IValidator<UserForAuthenticationDto> validator)
        {
            _authenticationService = authenticationService;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            if (IsUserAuthenticated())
                return RedirectToAction("Index", "Admin");

            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] UserForAuthenticationDto userDto)
        {
            if (IsUserAuthenticated())
                return RedirectToAction("Index", "Admin");

            var result = await _validator.ValidateAsync(userDto);
            if (!result.IsValid)
            {
                throw new ValidationException(ConvertHelper.ConvertErrorsToMessageString(result));
            }

            var user = await _authenticationService.SignInAsync(userDto);

            var claimPrincipal = CreateClaimsPrincipal(user);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);

            return Ok();
        }

        [Authorize]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }

        private ClaimsPrincipal CreateClaimsPrincipal(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.RoleName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimPrincipal;
        }

        private bool IsUserAuthenticated() =>
            User.Claims.Any();
    }
}
