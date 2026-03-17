namespace hometask.Dtos {
    public class TaskWeekSchedule {
        public int WeekOffset { get; init; }
        public Guid TaskId { get; init; }
        public string TaskName { get; init; } = default!;
        public string ResponsiblePerson { get; init; } = default!;
    }
}
