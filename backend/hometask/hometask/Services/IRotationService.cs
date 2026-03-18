using hometask.Entities;

namespace hometask.Services {
    public interface IRotationService {

        string GetResponsible(HouseTask task, DateOnly weekStart);

    }
}
