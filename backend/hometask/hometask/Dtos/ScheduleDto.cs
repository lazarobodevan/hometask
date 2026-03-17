namespace hometask.Dtos {
    public class ScheduleDto {
        public DateOnly BeginDate { get; init; }
        public DateOnly EndDate { get; init; }

        public List<ScheduleItemDto> Items { get; init; } = new();
    }
}
