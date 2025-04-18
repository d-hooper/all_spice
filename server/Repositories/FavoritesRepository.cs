

namespace all_spice.Repositories;

public class FavoritesRepository
{
  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal FavoriteRecipe CreateFavoriteRecipe(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites(recipe_id, account_id)
    VALUES (@RecipeId, @AccountId);
    
    SELECT 
    favorites.*,
    recipes.*, 
    accounts.*
    FROM favorites
    INNER JOIN recipes ON recipes.id = favorites.recipe_id
    INNER JOIN accounts ON recipes.creator_id = accounts.id
    WHERE favorites.id = LAST_INSERT_ID();";

    FavoriteRecipe favoriteRecipe = _db.Query(sql, (Favorite favorite, FavoriteRecipe recipe, Profile profile) =>
    {
      recipe.AccountId = favorite.AccountId;
      recipe.FavoriteId = favorite.Id;
      recipe.Creator = profile;
      return recipe;
    }, favoriteData).SingleOrDefault();
    return favoriteRecipe;
  }

  internal List<FavoriteRecipe> GetFavoriteRecipesByAccountId(string accountId)
  {
    string sql = @"
    SELECT 
    favorites.*,
    recipes.*, 
    accounts.*
    FROM favorites
    INNER JOIN recipes ON recipes.id = favorites.recipe_id
    INNER JOIN accounts ON accounts.id = recipes.creator_id
    WHERE accounts.id = @accountId;";

    List<FavoriteRecipe> favoriteRecipes = _db.Query(sql, (Favorite favorite, FavoriteRecipe recipe, Profile profile) =>
    {
      recipe.AccountId = favorite.AccountId;
      recipe.FavoriteId = favorite.Id;
      recipe.Creator = profile;
      return recipe;
    }, new { accountId }).ToList();
    return favoriteRecipes;
  }
}