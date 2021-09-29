<template>
  <div class="mj-landing-navbar">
    <application-title :with-icon="true" />
    <div class="mj-landing-navbar-buttons">
      <language-change-button :is-transparent="true" />
      <login-button />
    </div>
  </div>
</template>

<script lang="ts">
import LoginButton from '@/components/landing/LoginButton.vue';
import ApplicationTitle from '@/components/navigation-bar/ApplicationTitle.vue';
import LanguageChangeButton from '@/components/shared/LanguageChangeButton.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'LandingNavigationBar',
  components: { LanguageChangeButton, LoginButton, ApplicationTitle },
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
  padding: 0.5rem 3rem;
  box-shadow: mistakes-journal.$box-shadow-sm;

  &-buttons {
    display: flex;

    > :not(:last-child) {
      margin-right: 1rem;
    }
  }
}
</style>
