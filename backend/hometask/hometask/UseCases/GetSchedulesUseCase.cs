using hometask.Data.Repositories;
using hometask.Dtos;

namespace hometask.UseCases {
    public class GetSchedulesUseCase : IUseCase {

        private readonly IHouseTaskRepository _houseTaskRepository;

        public GetSchedulesUseCase(IHouseTaskRepository houseTaskRepository) {
            _houseTaskRepository = houseTaskRepository;
        }

        public async Task<List<ScheduleDto>> ExecuteAsync() {
            return await _houseTaskRepository.GetSchedules();
        }

    }
}
