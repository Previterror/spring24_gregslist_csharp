
namespace csharp_gregslist_api.Services;

public class HousesService
{
    private readonly HousesRepository _repository;

    public HousesService(HousesRepository repository)
    {
        _repository = repository;
    }

    internal List<House> GetAllHouses()
    {
        List<House> houses = _repository.GetHouses();
        return houses;
    }
}