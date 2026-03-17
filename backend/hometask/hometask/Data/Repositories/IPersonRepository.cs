using hometask.Entities;

namespace hometask.Data.Repositories {
    public interface IPersonRepository : IRepository<Person>{

        Task<Person?> GetById(string id);
    }
}
