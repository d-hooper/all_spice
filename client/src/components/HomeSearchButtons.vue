<script setup>
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';

async function getRecipes() {
  try {
    await recipesService.getRecipes()
  }
  catch (error) {
    Pop.error(error, 'Unable to retrieve recipes');
    logger.error('unable to retrieve recipes'.toUpperCase(), error);

  }
}

async function getMyRecipes() {
  try {
    await recipesService.getMyRecipes()
  }
  catch (error) {
    Pop.error(error, 'Unable to retrieve recipes');
    logger.error('unable to retrieve recipes'.toUpperCase(), error)
  }
}

async function getSavedRecipes() {
  try {
    await recipesService.getFavoriteRecipes()
  }
  catch (error) {
    Pop.error(error, 'Unable to retrieve recipes');
    logger.error('unable to retrieve recipes'.toUpperCase(), error)
  }
}
</script>


<template>
  <div class="bg-light border-success border-3 p-2 rounded box-shadow mb-3">
    <button @click="getRecipes()" class="btn text-success selectable me-2 fw-bold" type="button"
            title="All Recipes">Home</button>
    <button @click="getMyRecipes()" class="btn text-success selectable me-2 fw-bold" type="button"
            title="Browse recipes you have created">My
      Recipes</button>
    <button @click="getSavedRecipes()" class="btn text-success selectable fw-bold" type="button"
            title="Show your saved recipes">Favorites</button>
  </div>
</template>


<style lang="scss" scoped></style>