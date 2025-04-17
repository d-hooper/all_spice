
namespace all_spice.Repositories;

public class IngredientsRepository
{
  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO ingredients(name, quantity, recipe_id)
    VALUES (@Name, @Quantity, @RecipeId);

    SELECT * FROM ingredients WHERE ingredients.id = LAST_INSERT_ID();";

    Ingredient createdIngredient = _db.Query<Ingredient>(sql, ingredientData).SingleOrDefault();
    return createdIngredient;
  }
}