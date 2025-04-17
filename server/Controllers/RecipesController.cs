namespace all_spice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{

  public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider)
  {
    _recipesService = recipesService;
    _auth0Provider = auth0Provider;
  }

  private readonly RecipesService _recipesService;
  private readonly Auth0Provider _auth0Provider;

  [HttpPost, Authorize]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      Recipe recipe = _recipesService.CreateRecipe(recipeData);
      return Ok(recipe);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);

    }
  }

  [HttpGet]
  //                                          vv[FromQuery] string category
  public ActionResult<List<Recipe>> GetRecipes()
  {
    try
    {
      // if (category == null)
      // {
      List<Recipe> recipes = _recipesService.GetRecipes();

      // }
      // else
      // {
      //   List<Recipe> recipes = _recipesService.GetRecipes(category);
      // }
      return Ok(recipes);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }

  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {

      Recipe recipe = _recipesService.GetRecipeById(recipeId);

      return Ok(recipe);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }

  }
}