namespace hometask.Dtos {
    public class ScheduleItemDto {

        public DateOnly Date { get; init; }

        public Guid TaskId { get; init; }
        public string TaskName { get; init; } = default!;

        public Guid AreaId { get; init; }
        public string AreaName { get; init; } = default!;

        public string ResponsibleId { get; init; } = default!;
        public string ResponsibleName { get; init; } = default!;

        public bool Completed { get; init; }

        public string? CompletedBy { get; init; }

        public DateTime? CompletedAt { get; init; }

    }
}
