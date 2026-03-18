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
                .Include(t => t.Completions)
                    .ThenInclude(c => c.CompletedBy)
                .ToListAsync();

            var util = new GenerateScheduleUtil();

            return util.GenerateSchedules(tasks, -2, 5);
        }

        public async Task<HouseTask?> GetHouseTaskDetailed(Guid id, DateOnly weekStart) {

            return await _dbSet
                .Include(t => t.HouseArea)
                .Include(t => t.Participants)
                    .ThenInclude(p => p.Person)
                .Include(t => t.Completions
                    .Where(c => c.WeekStart == weekStart))
                    .ThenInclude(c => c.CompletedBy)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<HouseTask?> GetByIdWithParticipantsAsync(Guid id) {
            return await _dbSet
                .Include(t => t.Participants)
                .SingleOrDefaultAsync(t => t.Id == id);
        }

    }
}
