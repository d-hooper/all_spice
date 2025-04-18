import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Recipe } from "@/models/Recipe.js"

class RecipesService {
  async getRecipes() {
    const response = await api.get("api/recipes")
    logger.log(response.data)
    this.formatRecipeData(response)
  }
  async getMyRecipes() {
    const response = await api.get('account/recipes')
    // logger.log(response.data)
    this.formatRecipeData(response)
  }

  async getFavoriteRecipes() {
    const response = await api.get('account/favorites')
    // logger.log(response.data)
    this.formatRecipeData(response)

  }
  async getRecipeById(recipeId) {
    const response = await api.get(`api/recipes/${recipeId}`)
    const recipe = new Recipe(response.data)
    AppState.activeRecipe = recipe
  }

  formatRecipeData(response) {
    const recipes = response.data.map(pojo => new Recipe(pojo))
    AppState.recipes = recipes
  }
}
export const recipesService = new RecipesService()