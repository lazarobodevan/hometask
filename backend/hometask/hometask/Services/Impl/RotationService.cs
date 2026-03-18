using hometask.Entities;

namespace hometask.Services.Impl {
    public class RotationService : IRotationService{

        public string GetResponsible(HouseTask task, DateOnly weekStart) {
            var participants = task.Participants
                .OrderBy(p => p.RotationOrder)
                .ToList();

            if (!participants.Any())
                throw new Exception("Tarefa não tem participantes");

            var count = participants.Count;

            var weeksOffset = (weekStart.DayNumber - task.RotationStartDate.DayNumber) / 7;

            var index = (task.RotationIndex + weeksOffset % count + count) % count;

            return participants[index].PersonId;
        }

    }
}
