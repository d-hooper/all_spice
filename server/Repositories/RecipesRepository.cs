



namespace all_spice.Repositories;

public class RecipesRepository
{
  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO recipes(title, instructions, img, category, creator_id)
    VALUES (@Title, @Instructions, @Img, @Category, @CreatorId);

    SELECT 
    recipes.*,
    accounts.*
    FROM recipes
    INNER JOIN accounts ON accounts.id = recipes.creator_id
    WHERE recipes.id = LAST_INSERT_ID();";
    Recipe createdRecipe = _db.Query(sql, (Recipe recipe, Profile account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, recipeData).SingleOrDefault();
    return createdRecipe;
  }

  internal List<Recipe> GetRecipes()
  {
    string sql = @"
    SELECT 
    recipes.*,
    accounts.*
    FROM recipes
    INNER JOIN accounts ON accounts.id = recipes.creator_id;";

    List<Recipe> recipes = _db.Query(sql, (Recipe recipe, Profile account) =>
    {
      recipe.Creator = account;
      return recipe;
    }).ToList();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
    SELECT 
    recipes.*,
    accounts.*
    FROM recipes
    INNER JOIN accounts ON accounts.id = recipes.creator_id
    WHERE recipes.id = @recipeId;";

    Recipe foundRecipe = _db.Query(sql, (Recipe recipe, Profile account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, new { recipeId }).SingleOrDefault();
    return foundRecipe;
  }

  internal void UpdateRecipe(Recipe recipe)
  {
    string sql = @"
    UPDATE recipes SET
    title = @Title,
    instructions = @Instructions
    WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, recipe);

    if (rowsAffected != 1)
    {
      throw new Exception($"The update did not process as expected. {rowsAffected} rows were updated instead of 1");
    }
  }

  internal void DeleteRecipe(int recipeId)
  {
    string sql = "DELETE FROM recipes WHERE id = @recipeId;";

    int rowsAffected = _db.Execute(sql, new { recipeId });
    if (rowsAffected != 1)
    {
      throw new Exception($"The delete request did not process as expected. {rowsAffected} rows were deleted instead of 1");
    }

  }
}