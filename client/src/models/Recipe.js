import { AppState } from "@/AppState.js"
import { Profile } from "./Account.js"

export class Recipe {
  constructor(data) {
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.title = data.title
    this.instructions = data.instructions
    this.img = data.img
    this.category = data.category
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)
  }
  get isFavorite() {
    const recipes = AppState.favoriteRecipes
    if (recipes.find(recipe => recipe.id == this.id)) {
      return true
    }
    return false
  }
}

export class FavoriteRecipe extends Recipe {

  constructor(data) {
    super(data);
    this.favoriteId = data.favoriteId
  }
}