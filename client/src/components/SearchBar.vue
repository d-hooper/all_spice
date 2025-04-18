<script setup>
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';


const editableSearchData = ref('')

async function getRecipesByQuery() {
  try {
    await recipesService.getRecipes(editableSearchData)
    editableSearchData.value = ''
  }
  catch (error) {
    Pop.error(error, 'Unable to retrieve recipes');
    logger.error('unable to retrieve recipes'.toUpperCase(), error);

  }
}
</script>


<template>
  <form>
    <div class="input-group">
      <label class="input-group-text" for="search">Find Recipes</label>
      <input v-model="editableSearchData" id="search" type="text" class="form-control" aria-label="Find Recipes">
      <button class="btn btn-vue" type="submit" title="Search!">
        <span class="mdi mdi-magnify text-light"></span>
      </button>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>