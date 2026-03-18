using System.Text.Json.Serialization;

namespace hometask.Dtos {
    public record UpdateTaskDoneDto {

        [JsonIgnore]
        public string PersonId { get; set; } = string.Empty;
        public Guid TaskId { get; set; }

        [JsonIgnore]
        public bool Status { get; set; }

        public DateOnly WeekStart { get; set; }
    }
}
