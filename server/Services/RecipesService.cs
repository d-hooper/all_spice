
using System.Net;

namespace all_spice.Services;

public class RecipesService
{
  public RecipesService(RecipesRepository repository)
  {
    _repository = repository;
  }
  private readonly RecipesRepository _repository;

  internal List<Recipe> GetRecipes()
  {
    List<Recipe> recipes = _repository.GetRecipes();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repository.GetRecipeById(recipeId);

    if (recipe == null)
    {
      throw new Exception($"No recipe found with id of {recipeId}");
    }

    return recipe;
  }
  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repository.CreateRecipe(recipeData);
    return recipe;
  }

  internal Recipe UpdateRecipe(Recipe recipeData, int recipeId, Account userInfo)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"you cannot update recipes input by other users {userInfo.Name}".ToUpper());
    }
    recipe.Title = recipeData.Title ?? recipe.Title;
    recipe.Instructions = recipeData.Instructions ?? recipe.Instructions;

    _repository.UpdateRecipe(recipe);

    return recipe;
  }
}