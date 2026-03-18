using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hometask.Entities {
    public class HouseTaskCompletion {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid HouseTaskId { get; set; }

        [ForeignKey(nameof(HouseTaskId))]
        public HouseTask HouseTask { get; set; } = default!;

        [Required]
        public string PersonId { get; set; } = string.Empty;

        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; } = default!;

        public string? CompletedById { get; set; } = string.Empty;
        public Person? CompletedBy { get; set; }

        [Required]
        public DateOnly WeekStart { get; set; }

        [Required]
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    }
}
