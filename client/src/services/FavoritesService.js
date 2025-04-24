import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"

class FavoritesService {
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