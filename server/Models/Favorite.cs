namespace all_spice.Models;

public class Favorite : RepoItem<int>
{
  public int RecipeId { get; set; }
  public string AccountId { get; set; }
}

public class FavoriteRecipe : Recipe
{
  public string AccountId { get; set; }
  public int FavoriteId { get; set; }
}