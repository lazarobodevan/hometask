using hometask.Entities;
using Microsoft.EntityFrameworkCore;

namespace hometask.Data.Repositories.Impl {
    public class HouseTaskCompletionRepository : Repository<HouseTaskCompletion>, IHouseTaskCompletionRepository{

        public HouseTaskCompletionRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        public async Task<HouseTaskCompletion?> GetByTaskIdAndWeekstart(Guid taskId, DateOnly weekStart) {

            return await _dbSet.FirstOrDefaultAsync(c =>
                c.HouseTaskId == taskId &&
                c.WeekStart == weekStart);
        }

    }
}
