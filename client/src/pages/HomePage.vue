<script setup>
import { AppState } from '@/AppState.js';
import HomeSearchButtons from '@/components/HomeSearchButtons.vue';
import Navbar from '@/components/Navbar.vue';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

const recipes = computed(() => AppState.recipes)

onMounted(() => {
  getRecipes()
})

async function getRecipes() {
  try {
    await recipesService.getRecipes()
  }
  catch (error) {
    Pop.error(error, 'Unable to retrieve recipes');
    logger.error('unable to retrieve recipes'.toUpperCase(), error);

  }
}

</script>

<template>
  <section class="container-fluid mt-2">
    <div class="row">
      <div class="col-12">
        <div class="hero-img rounded-1">
          <div>
            <Navbar />
          </div>
          <div class="text-light d-flex flex-column justify-content-end align-items-center">
            <div class="text-center mb-3 fw-bold text-shadow">
              <h1 class="fw-bold">All-Spice</h1>
              <p class="mb-0 fs-2">Cherish Your Family</p>
              <p class="mb-0 fs-2">And Their Cooking</p>
            </div>
            <HomeSearchButtons />
          </div>
        </div>
      </div>
    </div>
  </section>
  <section class="container">
    <div class="row mt-3 justify-content-center justify-content-sm-start">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-sm-6 col-lg-4" role="button"
           :title="`See details about ${recipe.title}`">
        <div class=" rounded food-card mb-3 shadow-sm d-flex flex-column justify-content-between text-light fw-bold"
             :style="{ backgroundImage: `url(${recipe.img})` }">
          <div class="text-start p-1 d-flex p">
            <p class="flex-shrink-1 px-2 mb-0 glassy-bg rounded-pill text-capitalize">{{ recipe.category }}</p>
          </div>
          <div class="mb-1 mx-1 p-2 glassy-bg rounded">
            <p class="mb-0">{{ recipe.title }}</p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
.hero-img {
  min-height: 40dvh;
  background-image: url('https://images.unsplash.com/photo-1690983322070-22861e13ce47?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D');
  background-size: cover;
  background-position: center;
}

.food-card {
  min-height: 36dvh;
  background-position: center;
  background-size: cover;
}
</style>
