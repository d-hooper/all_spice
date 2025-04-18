namespace all_spice.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly AccountService _accountService;
  private readonly FavoritesService _favoritesService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, FavoritesService favoritesService)
  {
    _auth0Provider = auth0Provider;
    _accountService = accountService;
    _favoritesService = favoritesService;
  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("favorites")]
  public async Task<ActionResult<List<FavoriteRecipe>>> GetMyFavoriteRecipes()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string accountId = userInfo.Id;
      List<FavoriteRecipe> favoriteRecipes = _favoritesService.GetFavoriteRecipesByAccountId(accountId);
      return Ok(favoriteRecipes);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);

    }
  }
}
