<template>
  <div class="container">
    <div class="row justify-content-center">
      <div class="col-12 col-lg-8 col-xl-7 mj-about-page-section-1">
        <h1>{{ $t('LandingPage.AboutMistakesJournal.HeaderFirstPart') }}</h1>
        <h2>
          {{ $t('LandingPage.AboutMistakesJournal.HeaderSecondPart') }}
          <application-title />
        </h2>
      </div>
    </div>
    <div class="row justify-content-center mt-4">
      <div class="col-12 col-md-6 col-xl-6 mj-about-page-section-2">
        <div class="ice-cream-icon">
          <img
            alt="Broken ice cream"
            src="@/assets/icons/ice_cream_broken.svg">
        </div>
        <div class="steps">
          <span>{{ $t('LandingPage.AboutMistakesJournal.Step1') }}</span>
          <fa-icon
            :icon="['fas', 'long-arrow-alt-right']"
            class="arrow-icon" />
          <span>{{ $t('LandingPage.AboutMistakesJournal.Step2') }}</span>
          <fa-icon
            :icon="['fas', 'long-arrow-alt-right']"
            class="arrow-icon" />
          <span>{{ $t('LandingPage.AboutMistakesJournal.Step3') }}</span>
        </div>
        <div class="ice-cream-icon">
          <img
            alt="Ice cream"
            src="@/assets/icons/ice_cream.svg">
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-12 text-center">
        <login-button />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import LoginButton from '@/components/landing/LoginButton.vue';
import ApplicationTitle from '@/components/navigation-bar/ApplicationTitle.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'AboutMistakesJournal',
  components: { LoginButton, ApplicationTitle },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-about-page-section {
  &-1 {
    text-align: center;

    h1 {
      @include mistakes-journal.font-bold(2rem);

      @include mistakes-journal.media-breakpoint-up(md) {
        @include mistakes-journal.font-bold(3rem);
        padding: 0 4rem;
      }

      @include mistakes-journal.media-breakpoint-up(lg) {
        @include mistakes-journal.font-bold(3.5rem);
        padding: 0 2rem;
      }
    }

    h2 {
      @include mistakes-journal.font-semi-bold(1rem);

      @include mistakes-journal.media-breakpoint-up(lg) {
        @include mistakes-journal.font-semi-bold(1.2rem);
      }
    }
  }

  &-2 {
    display: flex;
    align-items: center;
    justify-content: space-around;

    @include mistakes-journal.media-breakpoint-up(lg) {
      justify-content: space-evenly;
    }

    .ice-cream-icon {
      width: 10rem;
      text-align: center;
    }

    .steps {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: space-between;
      margin: 0 1rem;

      @include mistakes-journal.media-breakpoint-up(lg) {
        flex: 1;
        flex-direction: row;
      }

      .arrow-icon {
        margin: 1rem 0;
        transform: rotate(90deg);

        @include mistakes-journal.media-breakpoint-up(lg) {
          margin: 0 1rem;
          transform: rotate(0);
        }
      }
    }
  }
}
</style>
