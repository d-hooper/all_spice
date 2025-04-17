namespace all_spice.Controllers;

[ApiController, Authorize]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider, RecipesService recipesService)
  {
    _ingredientsService = ingredientsService;
    _auth0Provider = auth0Provider;
    _recipesService = recipesService;
  }
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth0Provider;
  private readonly RecipesService _recipesService;

  [HttpPost]
  public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData, userInfo);
      return Ok(ingredient);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);

    }
  }
}