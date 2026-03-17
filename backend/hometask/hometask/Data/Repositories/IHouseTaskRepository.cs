using hometask.Dtos;
using hometask.Entities;

namespace hometask.Data.Repositories {
    public interface IHouseTaskRepository : IRepository<HouseTask>{

        Task<List<ScheduleDto>> GetSchedules();
    }
}
