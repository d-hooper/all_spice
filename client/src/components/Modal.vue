<script setup>
import RecipeForm from './RecipeForm.vue';

defineProps({
  showHeader: { type: Boolean, default: false },
  modalId: { type: String, required: true },
  modalTitle: { type: String },
})

</script>


<template>
  <div class="modal fade" :id="`${modalId}`" tabindex="-1" :aria-labelledby="`${modalId}Label`" aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div v-if="showHeader" class="modal-header bg-vue text-light">
          <h1 class="modal-title fs-3 libre-baskerville-bold" :id="`${modalId}Label`">{{ modalTitle ?? '' }}</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <RecipeForm />
        </div>
        <!-- <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div> -->
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>