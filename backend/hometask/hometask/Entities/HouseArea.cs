using System.ComponentModel.DataAnnotations;

namespace hometask.Entities {
    public class HouseArea {

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
