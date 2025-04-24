<script setup>
import FloatingButton from '@/components/FloatingButton.vue';
import HomeSearchButtons from '@/components/HomeSearchButtons.vue';
import Navbar from '@/components/Navbar.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeForm from '@/components/RecipeForm.vue';
import Modal from '@/components/Modal.vue';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import RecipeDetails from '@/components/RecipeDetails.vue';
import { AppState } from '@/AppState.js';

const account = computed(() => AppState.account)

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
          <Navbar />
          <div class="text-light d-flex flex-column justify-content-end align-items-center position-relative">
            <div class="text-center mb-3 fw-bold text-shadow libre-baskerville-bold">
              <h1 class="fw-bold display-6">All-Spice</h1>
              <p class="mb-0 fs-3">Cherish Your Family</p>
              <p class="mb-0 fs-3">And Their Cooking</p>
            </div>
            <HomeSearchButtons class="position-absolute buttons" />
          </div>
        </div>
      </div>
    </div>
  </section>
  <RecipeCard />

  <FloatingButton v-if="account" class="sticky-bottom" />
  <Modal :modalId="'createRecipe'" :modalTitle="'Add a New Recipe'" :showHeader="true">
    <RecipeForm />
  </Modal>
  <RecipeDetails :editMode="false" />
</template>

<style scoped lang="scss">
.hero-img {
  min-height: 40dvh;
  background-image: url('https://images.unsplash.com/photo-1690983322070-22861e13ce47?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D');
  background-size: cover;
  background-position: center;
  margin-bottom: 2.75rem;
}

.buttons {
  bottom: -5.75rem;
}

@media screen and (max-width: 992px) {
  .buttons {
    bottom: -6.7rem;
  }

}

@media screen and (max-width: 768px) {
  .buttons {
    bottom: -5.25rem;
  }

}

@media screen and (max-width: 576px) {
  .hero-img {
    margin-bottom: 0.75rem;
  }

  .buttons {
    bottom: -4rem;
  }

}
</style>