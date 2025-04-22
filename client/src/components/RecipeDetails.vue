<script setup>
import { AppState } from '@/AppState.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { Modal } from 'bootstrap/dist/js/bootstrap.bundle.js';
import { computed } from 'vue';

const recipe = computed(() => AppState.activeRecipe)
const account = computed(() => AppState.account)

defineProps({
  editMode: { type: Boolean, required: true, default: false }
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

</script>


<template>
  <div class="modal fade" id="recipeDetails" tabindex="-1" aria-labelledby="recipeLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body">
          <div class="container-fluid">
            <div v-if="recipe" class="row">
              <div class="col-md-6">
                <img :src="recipe.img" alt="" class="modal-img object-fit-cover object-position-center">
              </div>
              <div class="col-md-6">
                <div class="d-flex justify-content-between">
                  <div class="mb-3">
                    <h2 id="recipeLabel" class="mb-0">{{ recipe.title }}</h2>
                    <p class="mb-0"> by {{ recipe.creator.name }}</p>
                  </div>
                  <div class="d-flex align-items-start gap-2">
                    <div v-if="account?.id == recipe.creatorId" class="dropdown ">
                      <span class="mdi mdi-dots-horizontal options" href="#" role="button" data-bs-toggle="dropdown"
                            aria-expanded="false"></span>

                      <ul class="dropdown-menu">
                        <li class="dropdown-item selectable text-success">Edit</li>
                        <li @click="deleteRecipe(recipe.id)" class="dropdown-item selectable text-danger">Delete</li>
                      </ul>
                    </div>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"
                            title="Close out of recipe details"></button>
                  </div>
                </div>
                <div class="d-flex text-light mb-3">
                  <p class="flex-shrink-1 px-2 mb-0 glassy-bg rounded-pill text-capitalize">{{ recipe.category }}</p>
                </div>
                <div>
                  <h3>Instructions</h3>
                  <p class="mb-0">{{ recipe.instructions }}</p>
                </div>
              </div>
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