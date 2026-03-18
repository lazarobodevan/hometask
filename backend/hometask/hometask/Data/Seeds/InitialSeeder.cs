using hometask.Entities;
using Microsoft.EntityFrameworkCore;

namespace hometask.Data.Seeds {
    public class InitialSeeder {

        private readonly DatabaseContext _db;

        public InitialSeeder(DatabaseContext db) {
            _db = db;
        }

        public async Task SeedAsync() {

            if (await _db.People.AnyAsync()) {
                return;
            }

            List<Person> people = new() {
                new Person {
                    Id = "FERNANDA",
                    Name = "Fernanda"
                },
                new Person {
                    Id = "LAZARO",
                    Name = "Lázaro"
                },
                new Person {
                    Id = "ESTELA",
                    Name = "Estela"
                }
            };

            List<HouseArea> houseAreas = new() {
                new HouseArea {
                    Id = Guid.NewGuid(),
                    Name = "Sala"
                },
                new HouseArea {
                    Id = Guid.NewGuid(),
                    Name = "Cozinha"
                },
                new HouseArea {
                    Id = Guid.NewGuid(),
                    Name = "Banheiro"
                }
            };

            var baseDate = new DateOnly(2026, 3, 16);

            List<HouseTask> tasks = new() {
                new HouseTask() {
                    HouseAreaId = houseAreas.Where(x => x.Name == "Sala").Single().Id,
                    Name = "Limpar sala",
                    Id = Guid.NewGuid(),
                    RotationIndex = 2,
                    RotationStartDate = baseDate
                },
                new HouseTask() {
                    HouseAreaId = houseAreas.Where(x => x.Name == "Cozinha").Single().Id,
                    Name = "Limpar cozinha",
                    Id = Guid.NewGuid(),
                    RotationIndex = 0,
                    RotationStartDate = baseDate
                },
                new HouseTask() {
                    HouseAreaId = houseAreas.Where(x => x.Name == "Banheiro").Single().Id,
                    Name = "Limpar banheiro",
                    Id = Guid.NewGuid(),
                    RotationIndex = 1,
                    RotationStartDate = baseDate
                }
            };

            var sala = tasks.Single(x => x.Name == "Limpar sala");
            var cozinha = tasks.Single(x => x.Name == "Limpar cozinha");
            var banheiro = tasks.Single(x => x.Name == "Limpar banheiro");

            List<HouseTaskParticipant> participants = new() {

                // SALA
                new HouseTaskParticipant {
                    PersonId = "LAZARO",
                    HouseTaskId = sala.Id,
                    RotationOrder = 0
                },
                new HouseTaskParticipant {
                    PersonId = "FERNANDA",
                    HouseTaskId = sala.Id,
                    RotationOrder = 1
                },
                new HouseTaskParticipant {
                    PersonId = "ESTELA",
                    HouseTaskId = sala.Id,
                    RotationOrder = 2
                },

                // COZINHA
                new HouseTaskParticipant {
                    PersonId = "LAZARO",
                    HouseTaskId = cozinha.Id,
                    RotationOrder = 0
                },
                new HouseTaskParticipant {
                    PersonId = "FERNANDA",
                    HouseTaskId = cozinha.Id,
                    RotationOrder = 1
                },
                new HouseTaskParticipant {
                    PersonId = "ESTELA",
                    HouseTaskId = cozinha.Id,
                    RotationOrder = 2
                },

                // BANHEIRO
                new HouseTaskParticipant {
                    PersonId = "LAZARO",
                    HouseTaskId = banheiro.Id,
                    RotationOrder = 0
                },
                new HouseTaskParticipant {
                    PersonId = "FERNANDA",
                    HouseTaskId = banheiro.Id,
                    RotationOrder = 1
                }
            };

            _db.People.AddRange(people);
            _db.HouseAreas.AddRange(houseAreas);
            _db.HouseTasks.AddRange(tasks);
            _db.HouseTasksParticipants.AddRange(participants);

            await _db.SaveChangesAsync();
        }
    }
}
