import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Recipe } from "@/models/Recipe.js"

class RecipesService {
  async updateRecipe(recipeId, recipeData) {
    const response = await api.put(`api/recipes/${recipeId}`, recipeData)
    logger.log(response.data)
    AppState.editMode = false
    this.getRecipeById(recipeId)
  }

  async getRecipes(query) {
    if (!query) {
      const response = await api.get(`api/recipes`)
      this.formatRecipeData(response)
    }
    else {
      const response = await api.get(`api/recipes?title=${query}`)
      this.formatRecipeData(response)
    }
  }
  async getMyRecipes() {
    const response = await api.get('account/recipes')
    // logger.log(response.data)
    this.formatRecipeData(response)
  }

  async getFavoriteRecipes() {
    const response = await api.get('account/favorites')
    // logger.log(response.data)
    const recipes = response.data.map(pojo => new Recipe(pojo))
    AppState.favoriteRecipes = recipes
  }

  setFavoriteRecipes() {
    AppState.recipes = AppState.favoriteRecipes
  }

  async getRecipeById(recipeId) {
    AppState.activeRecipe = null
    const response = await api.get(`api/recipes/${recipeId}`)
    const recipe = new Recipe(response.data)
    AppState.activeRecipe = recipe
  }
  async createRecipe(foodData) {
    const response = await api.post('api/recipes', foodData)
    logger.log(response.data)
    const recipe = new Recipe(response.data)
    AppState.recipes.push(recipe)
  }
  async deleteRecipe(recipeId) {
    const response = await api.delete(`api/recipes/${recipeId}`)
    const recipes = AppState.recipes
    const indexToDelete = recipes.findIndex(recipe => recipe.id == recipeId)
    recipes.splice(indexToDelete, 1)
  }
  formatRecipeData(response) {
    const recipes = response.data.map(pojo => new Recipe(pojo))
    AppState.recipes = recipes
  }
  activateEditMode() {
    AppState.editMode = true
  }
  deactivateEditMode() {
    AppState.editMode = false
  }
}
export const recipesService = new RecipesService()