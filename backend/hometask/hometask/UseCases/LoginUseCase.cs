using hometask.Data.Repositories;
using hometask.Entities;

namespace hometask.UseCases {
    public class LoginUseCase : IUseCase {

        private readonly IPersonRepository _personRepository;

        public LoginUseCase(IPersonRepository personRepository) {
            _personRepository = personRepository;
        }

        public async Task<Person?> ExecuteAsync(string username) {

            var normalizedName = username.ToUpper();

            return await _personRepository.GetById(normalizedName);
        }
    }
}
