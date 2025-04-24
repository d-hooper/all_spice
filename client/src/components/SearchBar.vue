<script setup>
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';


const editableSearchData = ref('')

async function getRecipesByQuery() {
  try {
    await recipesService.getRecipes(editableSearchData.value)
    editableSearchData.value = ''
  }
  catch (error) {
    Pop.error(error, 'Unable to retrieve recipes');
    logger.error('unable to retrieve recipes'.toUpperCase(), error);

  }
}
</script>


<template>
  <form @submit.prevent="getRecipesByQuery()">
    <div class="input-group">
      <input v-model="editableSearchData" id="recipeSearch" type="text" class="form-control" aria-label="Find Recipes"
             placeholder="Find Recipes">
      <button class="btn btn-vue" type="submit" title="Search!">
        <span class="mdi mdi-magnify text-light"></span>
      </button>
    </div>
  </form>
</template>


<style lang="scss" scoped>
input {
  min-width: 30dvw !important;
}

.input-group {
  max-height: 3.75rem !important;
}
</style>