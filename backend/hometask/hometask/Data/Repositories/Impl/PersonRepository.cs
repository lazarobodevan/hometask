using hometask.Entities;
using Microsoft.EntityFrameworkCore;

namespace hometask.Data.Repositories.Impl {
    public class PersonRepository : Repository<Person>, IPersonRepository{

        public PersonRepository(DatabaseContext dbContext) : base(dbContext) { }

        public async Task<Person?> GetById(string id) {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}
