<script setup>
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { Modal } from 'bootstrap/dist/js/bootstrap.bundle.js';
import { ref } from 'vue';

const editableRecipeData = ref({
  title: '',
  category: '',
  instructions: '',
  img: ''
})

async function createRecipe() {
  try {
    const foodData = editableRecipeData.value
    await recipesService.createRecipe(foodData)
    Modal.getOrCreateInstance('#createRecipe').hide()
  }
  catch (error) {
    Pop.error(error, 'could not create recipe');
    logger.error('could not create recipe'.toUpperCase(), error);
  }
}

</script>


<template>
  <form @submit.prevent="createRecipe()">
    <section class="container">
      <div class="row justify-content-center">
        <div class="col-md-6 order-last order-md-first">
          <div class="form-floating mb-2">
            <input v-model="editableRecipeData.img" id="img" type="text" class="form-control" aria-label="Find Recipes"
                   required>
            <label for="img">Image URL</label>
          </div>
          <div class="text-center mb-2">
            <img :src="editableRecipeData.img || 'https://upload.wikimedia.org/wikipedia/commons/thumb/b/b8/Placeholder-image.png/640px-Placeholder-image.png'"
                 alt="" class="rounded form-img">
          </div>
        </div>
        <div class="col-md-6">
          <div class="d-flex flex-column h-100">
            <div class="form-floating mb-2">
              <input v-model="editableRecipeData.title" id="title" type="text" class="form-control"
                     aria-label="Find Recipes" required max-length="25">
              <label for="title">Recipe Title</label>
            </div>
            <div class="form-floating mb-2">
              <select v-model="editableRecipeData.category" id="category" class="form-select"
                      aria-label="Default select example" required>
                <option disabled value="" selected>Select</option>
                <option value="breakfast">Breakfast</option>
                <option value="lunch">Lunch</option>
                <option value="dinner">Dinner</option>
                <option value="dessert">Dessert</option>
                <option value="snack">Snack</option>
              </select>
              <label for="category">Category</label>
            </div>
            <div class="form-floating mb-2 flex-grow-1 d-flex align-content-stretch">
              <textarea class="form-control" v-model="editableRecipeData.instructions" name="recipeInstructions"
                        id="instructions">
            </textarea>
              <label for="instructions">Recipe Instructions</label>
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <div class="text-end">
            <button class="btn btn-vue text-light" type="submit">Add Recipe</button>
          </div>
        </div>
      </div>
    </section>
  </form>
</template>


<style lang="scss" scoped>
.form-img {
  width: 100%;
  object-fit: cover;
}

textarea {
  min-height: 100% !important;
}
</style>