
using System.Net;

namespace all_spice.Services;

public class RecipesService
{
  public RecipesService(RecipesRepository repository)
  {
    _repository = repository;
  }
  private readonly RecipesRepository _repository;

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repository.CreateRecipe(recipeData);
    return recipe;
  }

  // internal List<Recipe> GetRecipes()
  // {
  //   List<Recipe> recipes = _repository.GetRecipes();
  //   return recipes;
  // }
}