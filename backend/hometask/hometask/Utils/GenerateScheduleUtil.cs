using hometask.Dtos;
using hometask.Entities;

namespace hometask.Utils {
    public class GenerateScheduleUtil {

        public List<ScheduleDto> GenerateSchedules(
            List<HouseTask> tasks,
            int startOffset,
            int weeks) {

            var result = new List<ScheduleDto>();

            var today = DateOnly.FromDateTime(DateTime.Today);
            var startOfWeek = DateUtils.GetStartOfWeek(today);

            Dictionary<Guid, string?> lastWeekAssignments = new();

            for (int i = 0; i < weeks; i++) {
                var weekOffset = startOffset + i;

                var beginDate = startOfWeek.AddDays(weekOffset * 7);
                var endDate = beginDate.AddDays(6);

                var schedule = new ScheduleDto {
                    BeginDate = beginDate,
                    EndDate = endDate
                };

                // 🔥 NOVO: distribuição da semana inteira
                var assignments = AssignTasksForWeek(tasks, lastWeekAssignments, beginDate);

                foreach (var task in tasks) {
                    var responsibleId = assignments[task.Id];

                    var responsible = task.Participants
                        .First(p => p.PersonId == responsibleId);

                    var completion = task.Completions
                        .FirstOrDefault(c => c.WeekStart == beginDate);

                    schedule.Items.Add(new ScheduleItemDto {
                        Date = beginDate,
                        TaskId = task.Id,
                        TaskName = task.Name,
                        AreaId = task.HouseAreaId,
                        AreaName = task.HouseArea?.Name ?? "",
                        ResponsibleId = responsible.PersonId,
                        ResponsibleName = responsible.Person.Name,
                        Completed = completion is not null,
                        CompletedBy = completion?.CompletedBy?.Name,
                        CompletedAt = completion?.CompletedAt
                    });
                }

                result.Add(schedule);

                // 🔁 salva histórico da semana
                lastWeekAssignments = assignments
                    .ToDictionary(x => x.Key, x => (string?)x.Value);
            }

            return result;
        }

        private Dictionary<Guid, string> AssignTasksForWeek(
            List<HouseTask> tasks,
            Dictionary<Guid, string?> lastWeekAssignments,
            DateOnly weekStart) {

            var result = new Dictionary<Guid, string>();
            var usedPeople = new HashSet<string>();

            // 🔥 tarefas com menos participantes primeiro (ex: banheiro)
            var orderedTasks = tasks
                .OrderBy(t => t.Participants.Count)
                .ToList();

            foreach (var task in orderedTasks) {
                var participants = task.Participants
                    .OrderBy(p => p.RotationOrder)
                    .ToList();

                if (!participants.Any())
                    continue;

                var count = participants.Count;

                // 🔥 calcula offset baseado no seed
                var weeksOffset = (weekStart.DayNumber - task.RotationStartDate.DayNumber) / 7;

                // 🔥 responsável ideal da rotação
                var idealIndex = (task.RotationIndex + (weeksOffset % count) + count) % count;

                var idealPerson = participants[idealIndex].PersonId;

                string? chosen = null;

                // 1️⃣ tenta seguir rotação ideal
                if (!usedPeople.Contains(idealPerson) &&
                    (!lastWeekAssignments.ContainsKey(task.Id) ||
                     lastWeekAssignments[task.Id] != idealPerson)) {
                    chosen = idealPerson;
                }

                // 2️⃣ tenta alguém válido (sem repetir semana passada e sem duplicar)
                if (chosen == null) {
                    chosen = participants
                        .Select(p => p.PersonId)
                        .FirstOrDefault(p =>
                            !usedPeople.Contains(p) &&
                            (!lastWeekAssignments.ContainsKey(task.Id) ||
                             lastWeekAssignments[task.Id] != p));
                }

                // 3️⃣ fallback: só evitar duplicidade na semana
                if (chosen == null) {
                    chosen = participants
                        .Select(p => p.PersonId)
                        .FirstOrDefault(p => !usedPeople.Contains(p));
                }

                // 4️⃣ fallback final: aceita o ideal mesmo duplicando (caso extremo)
                if (chosen == null) {
                    chosen = idealPerson;
                }

                result[task.Id] = chosen;
                usedPeople.Add(chosen);
            }

            return result;
        }
    }
}
