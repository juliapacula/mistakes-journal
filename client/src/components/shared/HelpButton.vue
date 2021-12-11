<template>
  <button
    class="btn btn-primary with-icon mj-help"
    type="button"
    @click="showTutorial()">
    <remix-icon
      class="btn-icon mj-help-icon"
      icon="question" />
    <span class="btn-text">{{ $t('NavigationBar.Help') }}</span>
  </button>
</template>

<script lang="ts">
import { UiStateActions } from '@/store/ui-state-module/actions';
import { UserActions } from '@/store/user-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'HelpButton',
  methods: {
    async showTutorial(): Promise<void> {
      await this.$store.dispatch(UserActions.UpdateUserTutorialState, false);

      if (this.$route.name !== 'MistakeSolutions' && this.$route.name !== 'MistakesPage') {
        await this.$router.push('/journal/mistakes');
      }

      await this.$store.dispatch(UiStateActions.ResetUserOnBoardingTour);
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-help {
  white-space: nowrap;

  &-icon {
    @include mistakes-journal.font-regular(1.1rem);
  }
}
</style>
