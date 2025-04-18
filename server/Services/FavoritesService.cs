

namespace all_spice.Services;

public class FavoritesService
{

  public FavoritesService(FavoritesRepository repository)
  {
    _repository = repository;
  }
  private readonly FavoritesRepository _repository;

  internal FavoriteRecipe CreateFavoriteRecipe(Favorite favoriteData)
  {
    FavoriteRecipe favoriteRecipe = _repository.CreateFavoriteRecipe(favoriteData);
    return favoriteRecipe;
  }

  internal List<FavoriteRecipe> GetFavoriteRecipesByAccountId(string accountId)
  {
    List<FavoriteRecipe> favoriteRecipes = _repository.GetFavoriteRecipesByAccountId(accountId);
    return favoriteRecipes;
  }
}