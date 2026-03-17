using hometask.Dtos;
using hometask.Entities;
using hometask.Utils;
using Microsoft.EntityFrameworkCore;

namespace hometask.Data.Repositories.Impl {
    public class HouseTaskRepository : Repository<HouseTask>, IHouseTaskRepository {


        public HouseTaskRepository(DatabaseContext context) : base(context) { }

        public async Task<List<ScheduleDto>> GetSchedules() {
            var tasks = await _dbSet
                .Include(t => t.HouseArea)
                .Include(t => t.Participants)
                    .ThenInclude(a => a.Person)
                .ToListAsync();

            var util = new GenerateScheduleUtil();

            return util.GenerateSchedules(tasks, -2, 5);
        }
    }
}
