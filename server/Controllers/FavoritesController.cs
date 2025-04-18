namespace all_spice.Controllers;


[ApiController, Authorize]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{

  public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
  {
    _favoritesService = favoritesService;
    _auth0Provider = auth0Provider;
  }
  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth0Provider;

  [HttpPost]
  public async Task<ActionResult<FavoriteRecipe>> CreateFavoriteRecipe([FromBody] Favorite favoriteData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountId = userInfo.Id;
      FavoriteRecipe favoriteRecipe = _favoritesService.CreateFavoriteRecipe(favoriteData);
      return Ok(favoriteRecipe);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);

    }
  }
}