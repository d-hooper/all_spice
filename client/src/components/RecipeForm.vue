<script setup>
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
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
  }
  catch (error) {
    Pop.error(error, 'could not create recipe');
    logger.error('could not create recipe'.toUpperCase(), error);
  }
}

</script>


<template>
  <form @submit.prevent="createRecipe()">
    <div class="form-floating mb-2">
      <input v-model="editableRecipeData.img" id="img" type="text" class="form-control" aria-label="Find Recipes"
             required>
      <label for="img">Image URL</label>
    </div>
    <div class="d-flex gap-2">
      <div class="">
        <div class="h-100">
          <img :src="editableRecipeData.img || ''" alt=""
               class="rounded form-img object-fit-cover object-position-center">
        </div>
      </div>
      <div class="flex-grow-1">
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
        <div class="form-floating mb-2">
          <textarea class="form-control" v-model="editableRecipeData.instructions" name="" id="instructions">
          </textarea>
          <label for="instructions">Recipe Instructions</label>
        </div>
      </div>
    </div>
    <div class="text-end">
      <button class="btn btn-vue text-light" type="submit">Add Recipe</button>
    </div>
  </form>
</template>


<style lang="scss" scoped>
.form-img {
  width: 30dvw;
  aspect-ratio: 1/1;
}

textarea {
  min-height: 25.15dvh !important;
}
</style>