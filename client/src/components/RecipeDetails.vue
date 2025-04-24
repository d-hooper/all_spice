<script setup>
import { AppState } from '@/AppState.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import EditRecipeForm from '@/components/EditRecipeForm.vue';
import { Modal } from 'bootstrap/dist/js/bootstrap.bundle.js';
import { computed, onMounted } from 'vue';
import { favoritesService } from '@/services/FavoritesService.js';

const recipe = computed(() => AppState.activeRecipe)
const account = computed(() => AppState.account)
const editMode = computed(() => AppState.editMode)

onMounted(() => {
  const myModalEl = document.getElementById('recipeDetails')
  myModalEl.addEventListener('hidden.bs.modal', () => {
    if (myModalEl) {
      recipesService.deactivateEditMode()
    }
  })
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
    Modal.getOrCreateInstance('#recipeDetails').hide()
    Pop.error(error, 'Could not delete recipe');
    logger.error('could not delete recipe'.toUpperCase(), error);
  }
}

function activateEditMode() {
  recipesService.activateEditMode()
}

async function deleteFavoriteRecipe(recipeId) {
  try {
    const confirmed = await Pop.confirm('Are you sure you want to delete this recipe from your Saved Favorites?', 'You can mark this recipe as a favorite again later, should you choose to do so.', 'YES', 'Cancel')
    if (!confirmed) return
    await favoritesService.deleteFavoriteRecipe(recipeId)
    Modal.getOrCreateInstance('#recipeDetails').hide()
    Pop.toast('Favorite status removed')
  }
  catch (error) {
    Modal.getOrCreateInstance('#recipeDetails').hide()
    Pop.error(error, 'Could not remove the favorite recipe');
    logger.error('could not remove the favorite recipe'.toUpperCase(), error);
  }
}
async function createFavoriteRecipe(recipeId) {
  try {
    await favoritesService.createFavoriteRecipe(recipeId)
    Pop.toast('Favorite status added')
  }
  catch (error) {
    Modal.getOrCreateInstance('#recipeDetails').hide()
    Pop.error(error, 'Could not add the favorite recipe');
    logger.error('could not add the favorite recipe'.toUpperCase(), error);
  }
}


</script>

<!-- TODO chang editMode to false on modal exit besides updating recipe -->
<template>
  <div class="modal fade" id="recipeDetails" tabindex="-1" aria-labelledby="recipeLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
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
                            aria-expanded="false" title="Recipe actions"></span>

                      <ul class="dropdown-menu">
                        <li @click="activateEditMode()" class="dropdown-item selectable text-success"
                            title="Edit recipe">Edit</li>
                        <li @click="deleteRecipe(recipe.id)" class="dropdown-item selectable text-danger"
                            title="Delete recipe">Delete</li>
                        <li v-if="recipe.isFavorite" @click="deleteFavoriteRecipe(recipe.id)"
                            class="dropdown-item selectable text-pink" title="Remove from favorite recipes">
                          <span class="mdi mdi-heart-off text-red"></span>
                        </li>
                        <li v-else @click="createFavoriteRecipe(recipe.id)" class="dropdown-item selectable text-danger"
                            title="Add to favorite recipes">
                          <span class="mdi mdi-heart text-red"></span>
                        </li>
                      </ul>
                    </div>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
                            title="Close out of recipe details"></button>
                  </div>
                </div>
                <div class="mx-2 d-flex text-light mb-2 justify-content-between">
                  <p class="mb-0 text-dark"> by {{ recipe.creator.name }}</p>
                  <div class="d-flex gap-1">
                    <p class="px-2 mb-0 glassy-bg rounded-pill text-capitalize">{{ recipe.category }}</p>
                    <span v-if="recipe.isFavorite" class="mdi mdi-heart text-red"
                          title="This recipe is saved to your favorites"></span>
                  </div>
                </div>
                <div class="mx-2 mb-2">
                  <h3>Instructions</h3>
                  <p class="mb-0">{{ recipe.instructions }}</p>
                </div>
              </div>
            </div>
            <div v-else-if="editMode">
              <EditRecipeForm :recipe="recipe" />
            </div>
            <div v-else>
              <h1>Loading...
                <span class="mdi mdi-food-croissant mdi-spin"></span>
              </h1>
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
  height: 45dvh;
}

.options {

  border: 2px inset rgba(0, 0, 0, 0);
}

.options:hover {
  border: 2px outset var(--bs-success);
}
</style>