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
    <div
      :class="{ 'sidebar-visible': !isSidebarHidden }"
      class="mj-content">
      <div
        v-if="!isSidebarHidden"
        class="mj-sidebar-overlay"
        @click="closeSidebar()" />
      <router-view />
    </div>
    <label-modal />
  </div>
</template>

<script lang="ts">
import LabelModal from '@/components/labels/LabelModal.vue';
import NavigationBar from '@/components/navigation-bar/NavigationBar.vue';
import Sidebar from '@/components/navigation-bar/Sidebar.vue';
import { UiStateActions } from '@/store/ui-state-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'MainPage',
  components: {
    LabelModal,
    NavigationBar,
    Sidebar,
  },
  computed: {
    isSidebarHidden(): boolean {
      return !this.$store.state.uiState.isSidebarVisible;
    },
  },
  methods: {
    async closeSidebar(): Promise<void> {
      await this.$store.dispatch(UiStateActions.ToggleSidebar);
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

.mj-sidebar-overlay {
  position: absolute;
  z-index: 500;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  opacity: 0.2;
  background-color: mistakes-journal.color('gray', '900');

  @include mistakes-journal.media-breakpoint-up(md) {
    display: none;
  }
}

.mj-sidebar {
  position: absolute;
  z-index: 1000;
  top: 0;
  bottom: 0;
  grid-area: sidebar;
  width: 18.5rem;
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
  position: relative;
  grid-area: content;
  overflow-y: auto;

  &.sidebar-visible {
    @include mistakes-journal.media-breakpoint-down(md) {
      overflow-y: hidden;
    }
  }
}
</style>
