namespace hometask.Dtos {
    public record GetScheduleDto {

        public DateOnly BeginDate { get; init; }
        public DateOnly EndDate { get; init; }

    }
}
