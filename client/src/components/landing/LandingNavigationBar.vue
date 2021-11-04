<template>
  <div class="mj-landing-navbar">
    <application-title :with-icon="true" />
    <div class="mj-landing-navbar-buttons">
      <language-change-button :is-transparent="true" />
      <login-button />
      <span class="mj-register-visible">
        <register-button />
      </span>
    </div>
  </div>
</template>

<script lang="ts">
import LoginButton from '@/components/landing/LoginButton.vue';
import RegisterButton from '@/components/landing/RegisterButton.vue';
import ApplicationTitle from '@/components/navigation-bar/ApplicationTitle.vue';
import LanguageChangeButton from '@/components/shared/LanguageChangeButton.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'LandingNavigationBar',
  components: {
    LanguageChangeButton,
    LoginButton,
    ApplicationTitle,
    RegisterButton,
  },
});

</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-landing-navbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 2rem;
  box-shadow: mistakes-journal.$box-shadow-sm;

  @include mistakes-journal.media-breakpoint-up(lg) {
    padding: 0.5rem 3rem;
  }

  &-buttons {
    display: flex;

    > :not(:last-child) {
      margin-right: 1rem;
    }
  }
}

.mj-register-visible {
  display: none;

  @include mistakes-journal.media-breakpoint-up(sm) {
    display: flex;
  }
}
</style>
