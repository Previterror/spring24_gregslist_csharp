




namespace csharp_gregslist_api.Repositories;

public class HousesRepository
{
    private readonly IDbConnection _db;

    public HousesRepository(IDbConnection db)
    {
        _db = db;
    }



    internal List<House> GetAllHouses()
    {
        string sql = @"
        SELECT
        houses.*,
        accounts.*
        FROM houses
        JOIN accounts on accounts.id = houses.creatorId;";

        List<House> houses = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }).ToList();
        return houses;
    }

    internal House GetHouseById(int houseId)
    {
        string sql = @"
        SELECT
        houses.*,
        accounts.*
        FROM houses
        JOIN accounts ON accounts.id = houses.creatorId
        WHERE houses.id = @houseId;";

        House house = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }, new { houseId }).FirstOrDefault();
        return house;
    }

    internal House ListHouse(House houseData)
    {
        string sql = @"
        INSERT INTO
        houses(
            sqft,
            bedrooms,
            bathrooms,
            imgurl,
            description,
            price,
            creatorid
        )
        VALUES(
            @Sqft,
            @Bedrooms,
            @Bathrooms,
            @Imgurl,
            @Description,
            @Price,
            @Creatorid
        );

        SELECT * 
        FROM houses 
        JOIN accounts ON accounts.id = houses.creatorid
        WHERE houses.id = LAST_INSERT_ID();";

        House house = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }, houseData).FirstOrDefault();
        return house;
    }

    internal House UpdateHouse(House houseToUpdate)
    {
        string sql = @"
        UPDATE houses
        SET
        sqft = @sqft,
        bedrooms = @Bedrooms,
        bathrooms = @Bathrooms,
        imgurl = @ImgUrl,
        description = @Description,
        price = @Price
        where id = @Id;
        
        SELECT
        houses.*,
        accounts.*
        FROM houses
        JOIN accounts ON accounts.id = houses.creatorId
        WHERE houses.id = @Id;";

        House house = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }, houseToUpdate).FirstOrDefault();
        return house;
    }

    internal void UnlistHouse(int houseId)
    {
        string sql = "DELETE FROM houses WHERE id = @houseId;";
        _db.Execute(sql, new { houseId });
    }

}