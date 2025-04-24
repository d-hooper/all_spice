import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,
  /** @type {import('./models/Recipe.js').Recipe[]} recipes from the database*/
  recipes: [],
  /** @type {import('./models/Recipe.js').FavoriteRecipe[]} recipes from the database*/
  favoriteRecipes: [],
  /** @type {import('./models/Recipe.js').FavoriteRecipe} actively shown recipe*/
  activeRecipe: null,
  editMode: false
})

