<template>
  <div class="mj-container">
    <div class="mj-nav">
      <navigation-bar />
    </div>
    <transition name="expand">
      <div
        v-if="!isSidebarHidden"
        class="mj-sidebar">
        <sidebar />
      </div>
    </transition>
    <div class="mj-content">
      <router-view />
    </div>
  </div>
</template>

<script lang="ts">
import NavigationBar from '@/components/NavigationBar.vue';
import Sidebar from '@/components/Sidebar.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'MainPage',
  components: {
    NavigationBar,
    Sidebar,
  },
  computed: {
    isSidebarHidden(): boolean {
      return !this.$store.state.uiState.isSidebarVisible;
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../styles/mistakes-journal';

.mj-container {
  display: grid;
  position: relative;
  grid-template-areas: 'nav nav'
    'sidebar content';
  grid-template-columns: minmax(0, min-content) auto;
  grid-template-rows: max-content auto;
  width: 100vw;
  height: 100vh;
}

.mj-nav {
  grid-area: nav;
}

.mj-sidebar {
  position: absolute;
  z-index: 1000;
  top: 0;
  bottom: 0;
  grid-area: sidebar;
  width: 15rem;
  overflow-x: hidden;

  @include mistakes-journal.media-breakpoint-up(sm) {
    position: static;
  }

  &.expand-enter-active,
  &.expand-leave-active {
    transition: width 500ms;
  }

  &.expand-enter,
  &.expand-leave-to {
    width: 0;
  }
}

.mj-content {
  grid-area: content;
  overflow-y: auto;
}
</style>
