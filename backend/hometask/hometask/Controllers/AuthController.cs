using hometask.Dtos;
using hometask.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hometask.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly LoginUseCase _loginUseCase;


        public AuthController(LoginUseCase loginUseCase) {
            _loginUseCase = loginUseCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto) {
            var found = await _loginUseCase.ExecuteAsync(dto.Username);

            if(found is null) {
                return Unauthorized("Usuário não cadastrado");
            }
            return Ok(found);
        }
    }
}
