using hometask.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hometask.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HouseTasksController : ControllerBase {

        private readonly GetSchedulesUseCase _getSchedulesUseCase;

        public HouseTasksController(GetSchedulesUseCase getSchedulesUseCase) {
            _getSchedulesUseCase = getSchedulesUseCase;
        }

        [HttpGet("schedules")]
        public async Task<IActionResult> GetSchedules() {
            var result = await _getSchedulesUseCase.ExecuteAsync();

            return Ok(result);
        }
    }
}
