


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
  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _repository.GetFavoriteById(favoriteId);

    if (favorite == null)
    {
      throw new Exception("No favorite relationship was found with the provided id of " + favoriteId + ".");
    }

    return favorite;
  }

  internal void DeleteFavorite(int favoriteId, Account userInfo)
  {
    Favorite favoriteToDelete = GetFavoriteById(favoriteId);
    if (favoriteToDelete.AccountId != userInfo.Id)
    {
      throw new Exception($"you cannot delete favorite recipe relationships for other users {userInfo.Name}".ToUpper());
    }
    _repository.DeleteFavorite(favoriteId);

  }
}