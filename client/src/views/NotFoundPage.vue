<template>
  <div class="container">
    <div class="row">
      <div class="col-12 text-center mj-not-found-elements">
        <language-change-button is-transparent />
        <application-title
          class="mj-title"
          with-icon />
        <h1>{{ $t('NotFound.Header') }}</h1>
        <router-link
          class="btn btn-link btn-primary"
          to="/">
          {{ $t('NotFound.GoToMainPage') }}
        </router-link>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import ApplicationTitle from '@/components/navigation-bar/ApplicationTitle.vue';
import LanguageChangeButton from '@/components/shared/LanguageChangeButton.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'NotFoundPage',
  components: {
    LanguageChangeButton,
    ApplicationTitle,
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../styles/mistakes-journal';

.mj-not-found-elements {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin: 5rem 0;
  padding-top: 2rem;
  padding-bottom: 2rem;
  background-color: mistakes-journal.color('secondary', '50');

  @include mistakes-journal.media-breakpoint-up(sm) {
    border-radius: 2rem;
  }

  .mj-title {
    margin-bottom: 1rem;
    font-size: 2rem;
  }

  h1 {
    margin-bottom: 2rem;
    font-size: 1.2rem;
  }
}
</style>
