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

            for (int i = 0; i < weeks; i++) {
                var weekOffset = startOffset + i;

                var beginDate = startOfWeek.AddDays(weekOffset * 7);
                var endDate = beginDate.AddDays(6);

                var schedule = new ScheduleDto {
                    BeginDate = beginDate,
                    EndDate = endDate
                };

                foreach (var task in tasks) {
                    var participants = task.Participants
                        .OrderBy(p => p.RotationOrder)
                        .ToList();

                    if (!participants.Any())
                        continue;

                    var count = participants.Count;

                    var index = (task.RotationIndex + weekOffset % count + count) % count;

                    var responsible = participants[index];

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
            }

            return result;
        }
    }
}
