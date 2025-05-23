namespace all_spice.Controllers;

[ApiController, Authorize]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider, RecipesService recipesService)
  {
    _ingredientsService = ingredientsService;
    _auth0Provider = auth0Provider;
  }
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth0Provider;

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

  [HttpDelete("{ingredientId}")]
  public async Task<ActionResult<string>> DeleteIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      string message = _ingredientsService.DeleteIngredient(ingredientId, userInfo);
      return Ok(message);
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);

    }
  }
}