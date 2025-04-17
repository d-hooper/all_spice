

namespace all_spice.Services;

public class IngredientsService
{
  public IngredientsService(IngredientsRepository repository, RecipesService recipesService)
  {
    _repository = repository;
    _recipesService = recipesService;
  }
  private readonly IngredientsRepository _repository;
  private readonly RecipesService _recipesService;

  internal Ingredient CreateIngredient(Ingredient ingredientData, Account userInfo)
  {
    Recipe recipe = _recipesService.GetRecipeById(ingredientData.RecipeId);

    if (recipe.CreatorId != userInfo.Id)
    {
      throw new Exception($"you cannot add ingredients to another user's recipes {userInfo.Name}".ToUpper());
    }

    Ingredient ingredient = _repository.CreateIngredient(ingredientData);
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repository.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }

  private Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repository.GetIngredientById(ingredientId);

    if (ingredient == null)
    {
      throw new Exception($"No ingredient found with id of {ingredientId}");
    }

    return ingredient;
  }

  internal string DeleteIngredient(int ingredientId, Account userInfo)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);

    _repository.DeleteIngredient(ingredientId);

    return $"{ingredient.Name} was deleted";
  }
}