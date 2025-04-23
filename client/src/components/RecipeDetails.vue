<script setup>
import { AppState } from '@/AppState.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import EditRecipeForm from '@/components/EditRecipeForm.vue';
import { Modal } from 'bootstrap/dist/js/bootstrap.bundle.js';
import { computed, onMounted, ref } from 'vue';

const recipe = computed(() => AppState.activeRecipe)
const account = computed(() => AppState.account)
const editMode = computed(() => AppState.editMode)

onMounted(() => {
})

async function deleteRecipe(recipeId) {
  try {
    const confirmed = await Pop.confirm('Are you sure you want to delete this recipe?', 'This cannot be undone.', 'YES', 'Cancel')
    if (!confirmed) return
    await recipesService.deleteRecipe(recipeId)
    Modal.getOrCreateInstance('#recipeDetails').hide()
    Pop.toast('Recipe deleted')
  }
  catch (error) {
    Pop.error(error, 'Could not delete recipe');
    logger.error('could not delete recipe'.toUpperCase(), error);
  }
}

function activateEditMode() {
  recipesService.activateEditMode()
}

</script>


<template>
  <div class="modal fade" id="recipeDetails" tabindex="-1" aria-labelledby="recipeLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-fullscreen-lg-down">
      <div class="modal-content">
        <div v-if="editMode" class="modal-header bg-vue text-light ">
          <div class="container">
            <div class="row">
              <div class="col-12">
                <div class="d-flex justify-content-between align-items-center">
                  <h1 class="libre-baskerville-bold">Edit Recipe</h1>
                  <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
                          title="Close out of recipe editor"></button>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-body">
          <div class="container-fluid">
            <div v-if="recipe && !editMode" class="row">
              <div class="col-md-6 px-0 order-last order-md-first">
                <img :src="recipe.img" alt="" class="modal-img object-fit-cover object-position-center rounded">
              </div>
              <div class="col-md-6 px-0">
                <div class="mx-2 d-flex justify-content-between">
                  <div class="mb-3">
                    <h2 id="recipeLabel" class="mb-0">{{ recipe.title }}</h2>
                  </div>
                  <div class="d-flex align-items-start gap-2">
                    <div v-if="account?.id == recipe.creatorId" class="dropdown ">
                      <span class="mdi mdi-dots-horizontal options" href="#" role="button" data-bs-toggle="dropdown"
                            aria-expanded="false" title="Edit or Delete Recipe"></span>

                      <ul class="dropdown-menu">
                        <li @click="activateEditMode()" class="dropdown-item selectable text-success">Edit</li>
                        <li @click="deleteRecipe(recipe.id)" class="dropdown-item selectable text-danger">Delete</li>
                      </ul>
                    </div>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
                            title="Close out of recipe details"></button>
                  </div>
                </div>
                <div class="mx-2 d-flex text-light mb-2 justify-content-between">
                  <p class="mb-0 text-dark"> by {{ recipe.creator.name }}</p>
                  <p class="px-2 mb-0 glassy-bg rounded-pill text-capitalize">{{ recipe.category }}</p>
                </div>
                <div class="mx-2 mb-2">
                  <h3>Instructions</h3>
                  <p class="mb-0">{{ recipe.instructions }}</p>
                </div>
              </div>
            </div>
            <div v-else-if="editMode">
              <EditRecipeForm :recipe="recipe" :editMode="editMode" />
            </div>
            <div v-else>
              <h1>Loading... <span class="mdi mdi-food-croissant mdi-spin"></span></h1>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

</template>

<style lang="scss" scoped>
.modal-img {
  width: 100%;
  height: 50dvh;
}

.options {

  border: 2px inset rgba(0, 0, 0, 0);
}

.options:hover {
  border: 2px outset var(--bs-success);
}
</style>