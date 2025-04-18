<script setup>
import HomeSearchButtons from '@/components/HomeSearchButtons.vue';
import Navbar from '@/components/Navbar.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { onMounted } from 'vue';



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
            <div class="text-center mb-3 fw-bold text-shadow libre-baskerville-bold">
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
  <RecipeCard />
</template>

<style scoped lang="scss">
.hero-img {
  min-height: 40dvh;
  background-image: url('https://images.unsplash.com/photo-1690983322070-22861e13ce47?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D');
  background-size: cover;
  background-position: center;
}
</style>