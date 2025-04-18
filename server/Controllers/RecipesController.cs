namespace all_spice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{

  public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider, IngredientsService ingredientsService)
  {
    _recipesService = recipesService;
    _auth0Provider = auth0Provider;
    _ingredientsService = ingredientsService;
  }

  private readonly RecipesService _recipesService;
  private readonly Auth0Provider _auth0Provider;
  private readonly IngredientsService _ingredientsService;

  [HttpGet]
  public ActionResult<List<Recipe>> GetRecipes([FromQuery] string category, [FromQuery] string title)
  {
    try
    {
      if (category == null && title == null)
      {
        List<Recipe> recipes = _recipesService.GetRecipes();
        return Ok(recipes);
      }
      else
      {
        List<Recipe> recipes = _recipesService.GetRecipes(category, title);
        return Ok(recipes);
      }
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

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try
    {

      List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipeId(recipeId);

      return Ok(ingredients);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }
  }

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

  [HttpPut("{recipeId}"), Authorize]
  public async Task<ActionResult<Recipe>> UpdateRecipe(int recipeId, [FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipesService.UpdateRecipe(recipeData, recipeId, userInfo);
      return Ok(recipe);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);

    }
  }

  [HttpDelete("{recipeId}"), Authorize]
  public async Task<ActionResult<string>> DeleteRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _recipesService.DeleteRecipe(recipeId, userInfo);
      return Ok(message);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);

    }
  }

}