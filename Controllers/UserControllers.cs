using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentControl.model;
using PaymentControl.Repositories.Interface;
using PaymentControl.Services;
namespace PaymentControl.Controllers
{
    [Route("[controller]")]
    public class UserControllers : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserControllers(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("/api/users")]
        [AllowAnonymous]
        public async Task<ActionResult> AdicionarNovoUsuario([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await _userRepository.AddUserAsync(user);
            return Created();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] User loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return BadRequest(new { message = "Username e/ou senha não podem estar vazios." });
            }

            var userAutenticate = await _userRepository.GetUserAsync(loginRequest.Username, loginRequest.Password);

            if (userAutenticate == null)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos." });
            }

            var token = TokenService.GenereteToken(userAutenticate);

            return Ok(new
            {
                username = userAutenticate.Username, // Presumindo que 'userAutenticate' tenha a propriedade 'Username'
                token = token
            });
        }
    }
}