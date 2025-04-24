<script setup>
import { AppState } from '@/AppState.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed } from 'vue';

const recipes = computed(() => AppState.recipes)

async function getRecipeById(recipeId) {
  try {
    await recipesService.getRecipeById(recipeId)
  }
  catch (error) {
    Pop.error(error, 'Unable to retrieve selected recipe');
    logger.error('unable to retrieve selected recipe'.toUpperCase(), error)

  }
}

</script>


<template>
  <section class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-md-11 col-lg-10">
        <div class="row justify-content-center justify-content-sm-start">
          <div v-for="recipe in recipes" :key="recipe.id" class="col-sm-6 col-lg-4">
            <div @click="getRecipeById(recipe.id)"
                 class=" rounded food-card mb-3 shadow-sm d-flex flex-column justify-content-between text-light fw-bold poppins-light"
                 :style="{ backgroundImage: `url(${recipe.img})` }" role="button"
                 :title="`See details about ${recipe.title}`" data-bs-target="#recipeDetails" data-bs-toggle="modal">
              <div class="text-start p-1 d-flex p">
                <p class="flex-shrink-1 px-2 mb-0 glassy-bg rounded-pill text-capitalize">{{ recipe.category }}</p>
              </div>
              <div class="mb-1 mx-1 px-2 py-1 glassy-bg rounded d-flex justify-content-between align-items-center">
                <p class="mb-0">{{ recipe.title }}</p>
                <span v-if="recipe.isFavorite" class="mdi mdi-heart text-red fs-5"></span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>
.food-card {
  min-height: 36dvh;
  background-position: center;
  background-size: cover;
}
</style>