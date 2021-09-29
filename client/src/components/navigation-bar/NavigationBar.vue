<template>
  <div class="mj-nav-container">
    <div class="mj-nav-toggle">
      <button
        v-tooltip="isSidebarVisible ? $t('NavigationBar.Collapse') : $t('NavigationBar.Expand')"
        class="btn btn-primary"
        type="button"
        @click="toggleSidebar()">
        <span class="btn-icon">
          <fa-layers>
            <fa-icon
              :icon="['fas', isSidebarVisible ? 'caret-left' : 'caret-right']"
              transform="shrink-3 left-8 up-2" />
            <fa-icon
              :icon="['fas', isSidebarVisible ? 'caret-square-left' : 'caret-square-right']"
              :mask="['fas', 'bars']"
              transform="shrink-3 left-8 up-2" />
          </fa-layers>
        </span>
        <span class="btn-text">{{ $t('NavigationBar.Toggle') }}</span>
      </button>
    </div>
    <div class="mj-nav-actions">
      <add-new-mistake-button class="mj-new" />
      <language-change-button />
      <logout-button />
    </div>
  </div>
</template>

<script lang="ts">
import AddNewMistakeButton from '@/components/shared/AddNewMistakeButton.vue';
import LanguageChangeButton from '@/components/shared/LanguageChangeButton.vue';
import LogoutButton from '@/components/navigation-bar/LogoutButton.vue';
import { UiStateActions } from '@/store/ui-state-module/actions';
import Vue from 'vue';
import { mapActions } from 'vuex';

export default Vue.extend({
  name: 'NavigationBar',
  components: { LogoutButton, AddNewMistakeButton, LanguageChangeButton },
  computed: {
    isSidebarVisible(): boolean {
      return this.$store.state.uiState.isSidebarVisible;
    },
  },
  methods: {
    ...mapActions({
      toggleSidebar: UiStateActions.ToggleSidebar,
    }),
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-nav-container {
  display: flex;
  justify-content: space-between;
  padding: 0.5rem 1rem;
  background-color: mistakes-journal.color('primary');
  color: mistakes-journal.color('gray', '50');
}

.mj-nav-actions,
.mj-nav-toggle {
  .btn-text {
    display: none;

    @include mistakes-journal.media-breakpoint-up(sm) {
      display: inline;
    }
  }
}

.mj-nav-actions {
  display: flex;
  align-items: center;
}
</style>
