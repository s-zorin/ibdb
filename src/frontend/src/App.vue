<script lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import { defineComponent, ref } from 'vue'

export default defineComponent({
  components: {
    RouterLink,
    RouterView
  },
  setup() {
    const hamburgerButton = ref<HTMLLinkElement>()
    const menu = ref<HTMLDivElement>()

    const hamburgerButtonClick = () => {
      hamburgerButton.value?.classList.toggle('is-active');
      menu.value?.classList.toggle('is-active');
    }

    return {
      hamburgerButton,
      hamburgerButtonClick,
      menu
    }
  }
})
</script>

<template>
  <nav class="navbar has-shadow" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <RouterLink :to="{ name: 'home' }" class="navbar-item">
        <p class="has-text-weight-bold has-text-primary is-size-4">IBDB</p>
      </RouterLink>
      <a ref="hamburgerButton" @click="hamburgerButtonClick" role="button" class="navbar-burger" aria-label="menu"
        aria-expanded="false" data-target="navbarBasicExample">
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
      </a>
    </div>
    <div ref="menu" class="navbar-menu">
      <div class="navbar-start">
        <div class="navbar-item has-dropdown is-hoverable">
          <RouterLink :to="{ name: 'books-view' }" class="navbar-link">
            Books
          </RouterLink>
          <div class="navbar-dropdown">
            <RouterLink :to="{ name: 'books-add' }" class="navbar-item">
              Add New Book
            </RouterLink>
          </div>
        </div>

          <RouterLink :to="{ name: 'reviews-view' }" class="navbar-item">
            Reviews
          </RouterLink>
      </div>
      <div class="navbar-end">
      </div>
    </div>
  </nav>

  <RouterView />
</template>

<style>
</style>
