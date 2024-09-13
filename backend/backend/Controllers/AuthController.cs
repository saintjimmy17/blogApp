using backend.Data;
using backend.Dto;
using backend.Entity;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<User> _repository;

        public AuthController(IRepository<User> repository) 
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IResult> Login([FromBody] LoginDto model)
        {
            var user = (await _repository.GetAll(x => x.Email== model.Email)).FirstOrDefault();
            if (user is not null && user.Password == model.Password)
            {
                var claimsPrincipal = new ClaimsPrincipal(
                    new ClaimsIdentity(
                            new[] { new Claim(ClaimTypes.Name, model.Email) },
                            BearerTokenDefaults.AuthenticationScheme
                        )
                    );

                return Results.SignIn( claimsPrincipal );
            }
            else
            {
                return Results.BadRequest();
            }
        }
    }
}
