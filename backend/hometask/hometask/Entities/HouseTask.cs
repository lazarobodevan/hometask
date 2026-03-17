using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hometask.Entities {
    public class HouseTask {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid HouseAreaId { get; set; }
        [Required, ForeignKey(nameof(HouseAreaId))]
        public HouseArea HouseArea { get; set; } = default!;
        
        [Required]
        public int RotationIndex { get; set; } = 0;

        public List<HouseTaskParticipant> Participants { get; set; } = new();

    }
}
