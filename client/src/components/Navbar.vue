<script setup>
import { ref, watch } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import SearchBar from './SearchBar.vue';

const theme = ref(loadState('theme') || 'light')

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
}

watch(theme, () => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}, { immediate: true })

</script>

<template>
  <nav class="navbar navbar-expand-md">
    <div class="container-fluid gap-2">
      <!-- collapse button -->
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-links"
              aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
        <span class="mdi mdi-menu text-light"></span>
      </button>
      <!-- collapsing menu -->
      <div class="collapse navbar-collapse " id="navbar-links">
        <ul class="navbar-nav">
          <!-- <li>
            <RouterLink :to="{ name: 'About' }" class="btn text-green selectable">
              About
            </RouterLink>
          </li> -->
        </ul>
        <!-- LOGIN COMPONENT HERE -->
        <div class="d-flex justify-content-center justify-content-md-end align-items-center gap-2 w-100">
          <SearchBar />
          <div class="d-sm-flex">
            <Login />
            <!-- <button class="btn text-light" @click="toggleTheme"
                    :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme`">
              <i v-if="theme == 'dark'" class="mdi mdi-weather-sunny fs-4"></i>
              <i v-if="theme == 'light'" class="mdi mdi-weather-night fs-4"></i>
            </button> -->
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<style lang="scss" scoped>
a {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
