namespace csharp_gregslist_api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class HousesController : ControllerBase
{
    private readonly HousesService _housesService;
    private readonly Auth0Provider _auth0Provider;

    public HousesController(HousesService housesService, Auth0Provider auth0Provider)
    {
        _housesService = housesService;
        _auth0Provider = auth0Provider;
    }

    [HttpGet]
    public ActionResult<List<House>> GetAllHouses()
    {
        try
        {
            List<House> houses = _housesService.GetAllHouses();
            return Ok(houses);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{houseId}")]
    public ActionResult<House> GetHouseById(int houseId)
    {
        try
        {
            House house = _housesService.GetHouseById(houseId);
            return Ok(house);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<House>> ListHouse([FromBody] House houseData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

            houseData.CreatorId = userInfo.Id;

            House house = _housesService.ListHouse(houseData);
            return Ok(house);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut("{houseId}")]
    public async Task<ActionResult<House>> UpdateHouse(int houseId, [FromBody] House houseData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            House house = _housesService.UpdateHouse(houseId, userInfo.Id, houseData);
            return Ok(house);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{houseId}")]
    public async Task<ActionResult<string>> UnlistHouse(int houseId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _housesService.UnlistHouse(houseId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

}