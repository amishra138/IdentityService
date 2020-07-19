using IdentityService.Application.Commands;
using IdentityService.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityService.Controller
{
    /// <summary>
    /// User controller
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserQuery _userQuery;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="userQuery"></param>
        public UserController(IMediator mediator, IUserQuery userQuery)
        {
            _mediator = mediator;
            _userQuery = userQuery;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Domain.Models.UserRequest model)
        {
            var response = await _mediator.Send(new RegisterUserCommand(model));

            if (response == null)
                return BadRequest(new { message = "please enter valid data" });

            return Ok(response);
        }

        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] Domain.Models.AuthenticateRequest model)
        {
            var response = await _mediator.Send(new AuthenticateUserCommand(model));

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }



        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userQuery.Get(string.Empty);
            return Ok(users);
        }
    }
}
