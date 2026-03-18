using hometask.Data.Repositories;
using hometask.Data.Repositories.Impl;
using hometask.Dtos;
using hometask.Entities;
using hometask.Services;
using hometask.Utils;

namespace hometask.UseCases {
    public class ConcludeTaskUseCase : IUseCase{

        private readonly IHouseTaskRepository _houseTaskRepository;
        private readonly IHouseTaskCompletionRepository _houseTaskCompletionRepository;
        private readonly IRotationService _rotationService;

        public ConcludeTaskUseCase(
            IHouseTaskRepository houseTaskRepository,
            IHouseTaskCompletionRepository houseTaskCompletionRepository,
            IRotationService rotationService) {

            _houseTaskRepository = houseTaskRepository;
            _houseTaskCompletionRepository = houseTaskCompletionRepository;
            _rotationService = rotationService;
        }

        public async Task ExecuteAsync(UpdateTaskDoneDto dto) {

            if (dto.WeekStart.DayOfWeek != DayOfWeek.Monday) {
                throw new Exception("WeekStart deve ser uma segunda-feira");
            }

            var task = await _houseTaskRepository.GetByIdWithParticipantsAsync(dto.TaskId);

            if (task == null)
                throw new Exception("Tarefa não encontrada");

            if (!task.Participants.Any(p => p.PersonId == dto.PersonId)) {
                throw new Exception("Você não participa dessa tarefa");
            }

            var weekStart = dto.WeekStart;

            var existing = await _houseTaskCompletionRepository.GetByTaskIdAndWeekstart(dto.TaskId, weekStart);

            var responsible = _rotationService.GetResponsible(task, weekStart);

            if (dto.Status) {
                if (existing != null)
                    return;

                var completion = new HouseTaskCompletion {
                    Id = Guid.NewGuid(),
                    HouseTaskId = dto.TaskId,
                    PersonId = dto.PersonId,
                    CompletedById = dto.PersonId,
                    WeekStart = weekStart,
                    CompletedAt = DateTime.UtcNow
                };

                await _houseTaskCompletionRepository.AddAsync(completion);
            }
            // DESMARCAR
            else {
                if (existing == null)
                    return;

                var completedBy = existing.CompletedById;

                if (completedBy == dto.PersonId) {
                    _houseTaskCompletionRepository.Remove(existing);
                } else {
                    if (completedBy == responsible)
                        throw new Exception("Não pode desfazer tarefa feita pelo responsável");

                    if (dto.PersonId == responsible)
                        throw new Exception("Responsável não pode desfazer tarefa");

                    throw new Exception("Você não pode desfazer essa tarefa");
                }
            }

            await _houseTaskCompletionRepository.SaveChangesAsync();
        }
    }
}
