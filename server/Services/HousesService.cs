

using Microsoft.AspNetCore.Http.HttpResults;

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
        List<House> houses = _repository.GetAllHouses();
        return houses;
    }

    internal House GetHouseById(int houseId)
    {
        House house = _repository.GetHouseById(houseId);
        if (house == null)
        {
            throw new Exception($"Invalid id: {houseId}");
        }
        return house;
    }

    internal House ListHouse(House houseData)
    {
        House house = _repository.ListHouse(houseData);
        return house;
    }
    internal House UpdateHouse(int houseId, string userId, House houseData)
    {
        House houseToUpdate = GetHouseById(houseId);
        if (houseToUpdate.CreatorId != userId)
        {
            throw new Exception("You are not authorized to alter this listing.");
        }

        houseToUpdate.Bathrooms = houseData.Bathrooms ?? houseToUpdate.Bathrooms;
        houseToUpdate.Bedrooms = houseData.Bedrooms ?? houseToUpdate.Bedrooms;
        houseToUpdate.SqFt = houseData.SqFt ?? houseToUpdate.SqFt;
        houseToUpdate.ImgUrl = houseData.ImgUrl ?? houseToUpdate.ImgUrl;
        houseToUpdate.Description = houseData.Description ?? houseToUpdate.Description;
        houseToUpdate.Price = houseData.Price ?? houseToUpdate.Price;

        House updatedHouse = _repository.UpdateHouse(houseToUpdate);
        return updatedHouse;

    }

    internal string UnlistHouse(int houseId, string userId)
    {
        House houseToUnlist = GetHouseById(houseId);
        if (houseToUnlist.CreatorId != userId)
        {
            throw new Exception("You are not authorized to unlist this property.");
        }

        _repository.UnlistHouse(houseId);
        return $"{houseId} listing has been removed.";
    }

}