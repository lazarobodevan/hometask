using hometask.Dtos;
using hometask.Extensions;
using hometask.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace hometask.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HouseTasksController : ControllerBase {

        private readonly GetSchedulesUseCase _getSchedulesUseCase;
        private readonly ConcludeTaskUseCase _concludeTaskUseCase;

        public HouseTasksController(
            GetSchedulesUseCase getSchedulesUseCase,
            ConcludeTaskUseCase concludeTaskUseCase) {

            _getSchedulesUseCase = getSchedulesUseCase;
            _concludeTaskUseCase = concludeTaskUseCase;
        }

        [HttpGet("schedules")]
        public async Task<IActionResult> GetSchedules() {
            var result = await _getSchedulesUseCase.ExecuteAsync();

            return Ok(result);
        }

        [HttpPatch("done")]
        public async Task<IActionResult> MarkAsDone([FromBody] UpdateTaskDoneDto dto) {

            try {
                dto.Status = true;
                dto.PersonId = HttpContext.GetUserId();
                await _concludeTaskUseCase.ExecuteAsync(dto);

                return Ok("Tarefa marcada como concluída");
            } catch (Exception ex) {

                return UnprocessableEntity(new {error = ex.Message});
            }
           
        }

        [HttpPatch("undone")]
        public async Task<IActionResult> MarkAsUnDone([FromBody] UpdateTaskDoneDto dto) {

            try {
                dto.Status = false;
                dto.PersonId = HttpContext.GetUserId();
                await _concludeTaskUseCase.ExecuteAsync(dto);

                return Ok("Tarefa marcada como concluída");
            } catch (Exception ex) {
                return UnprocessableEntity(new { error = ex.Message });
            }
           
        }
    }
}
