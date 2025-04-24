import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { FavoriteRecipe } from "@/models/Recipe.js"

class FavoritesService {
  async createFavoriteRecipe(recipeId) {
    const response = await api.post('api/favorites', { recipeId: recipeId })
    const createdRecipe = new FavoriteRecipe(response.data)
    const recipes = AppState.favoriteRecipes
    recipes.push(createdRecipe)
  }

  async deleteFavoriteRecipe(recipeId) {
    const recipes = AppState.favoriteRecipes
    const favoriteRecipe = recipes.find(recipe => recipe.id == recipeId)
    debugger
    const response = await api.delete(`api/favorites/${favoriteRecipe.favoriteId}`)
    logger.log('Deleted favorite recipe', response.data)
    const index = recipes.findIndex(recipe => recipe.favoriteId)
    recipes.splice(index, 1)
  }

}

export const favoritesService = new FavoritesService()