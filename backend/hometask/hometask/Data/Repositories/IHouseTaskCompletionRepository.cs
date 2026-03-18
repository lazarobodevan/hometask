using hometask.Entities;

namespace hometask.Data.Repositories {
    public interface IHouseTaskCompletionRepository : IRepository<HouseTaskCompletion>{

        Task<HouseTaskCompletion?> GetByTaskIdAndWeekstart(Guid taskId, DateOnly weekStart);

    }
}
