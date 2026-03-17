using System.ComponentModel.DataAnnotations;

namespace hometask.Entities {
    public class HouseTaskParticipant {

        public Guid HouseTaskId { get; set; }
        public HouseTask HouseTask { get; set; } = default!;

        public string PersonId { get; set; } = string.Empty;
        public Person Person { get; set; } = default!;

        [Required]
        public int RotationOrder { get; set; } = 0;

    }
}
