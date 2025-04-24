<script setup>
import { Recipe } from '@/models/Recipe.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';


const props = defineProps({
  recipe: { type: Recipe, required: true }
})

const editableRecipeData = ref({
  title: props.recipe.title ?? '',
  category: props.recipe.category ?? '',
  instructions: props.recipe.instructions ?? '',
  img: props.recipe.img ?? ''
})

async function updateRecipe() {
  try {
    const recipeId = props.recipe.id
    await recipesService.updateRecipe(recipeId, editableRecipeData.value)

  }
  catch (error) {
    Pop.error(error, 'could not update recipe');
    logger.error('could not update recipe'.toUpperCase(), error);
  }
}
</script>

<template>

  <form @submit.prevent="updateRecipe()">
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
                 alt="" class="rounded form-img object-fit-cover object-position-center">
          </div>
        </div>
        <div class="col-md-6">
          <div class="d-flex flex-column h-100">
            <div class="form-floating mb-2">
              <input v-model="editableRecipeData.title" id="title" type="text" class="form-control"
                     aria-label="Find Recipes" required>
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
        <div class="col-12 order-last">
          <div class="text-end">
            <button class="btn btn-vue text-light" type="submit">Update Recipe</button>
          </div>
        </div>
      </div>
    </section>
  </form>
</template>

<style lang="scss">
.form-img {
  width: 100%;
  max-height: 40dvh;
}

textarea {
  min-height: 100% !important;
}
</style>