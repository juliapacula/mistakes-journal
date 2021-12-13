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
    <div class="row justify-content-center mt-4 mb-5">
      <div class="col-10 col-md-8 col-lg-6 mj-about-page-section-early-access">
        <h5>{{ $t('LandingPage.AboutMistakesJournal.EarlyAccessHeader') }}</h5>
        <p>
          {{ $t('LandingPage.AboutMistakesJournal.EarlyAccessText') }}
        </p>
      </div>
    </div>
    <div class="row justify-content-center mt-4 mb-3">
      <div class="col-12 col-md-10 col-xl-8 mj-about-page-section-2">
        <div class="ice-cream-icon">
          <img
            alt="Broken ice cream"
            src="@/assets/icons/ice_cream_broken.svg">
        </div>
        <div class="steps">
          <span>{{ $t('LandingPage.AboutMistakesJournal.Step1.StepName') }}</span>
          <fa-icon
            :icon="['fas', 'long-arrow-alt-right']"
            class="arrow-icon" />
          <span>{{ $t('LandingPage.AboutMistakesJournal.Step2.StepName') }}</span>
          <fa-icon
            :icon="['fas', 'long-arrow-alt-right']"
            class="arrow-icon" />
          <span>{{ $t('LandingPage.AboutMistakesJournal.Step3.StepName') }}</span>
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
        <register-button class="mb-5 mt-3">
          {{ $t('LandingPage.Nav.GetStarted') }}
        </register-button>
      </div>
    </div>
    <div class="row justify-content-center mt-5">
      <div class="col-12 col-md-6 col-xl-6 mj-about-page-section-with-image">
        <div class="advanced-steps">
          <span class="step-name">{{ $t('LandingPage.AboutMistakesJournal.Step1.StepName') }}</span>
          <span class="step-header">{{ $t('LandingPage.AboutMistakesJournal.Step1.Header') }}</span>
          <span class="step-text">{{ $t('LandingPage.AboutMistakesJournal.Step1.Text') }}</span>
        </div>
        <img
          alt="Mistakes journal tutorial presentation"
          class="mj-landing-steps-image"
          :src="require('@/assets/icons/' + $i18n.locale + '/onboard.svg')">
      </div>
    </div>
    <div class="row justify-content-center mt-4">
      <div class="col-12 col-md-6 col-xl-6 mj-about-page-section-with-image mj-about-page-section-4">
        <div class="advanced-steps">
          <span class="step-name">{{ $t('LandingPage.AboutMistakesJournal.Step2.StepName') }}</span>
          <span class="step-header">{{ $t('LandingPage.AboutMistakesJournal.Step2.Header') }}</span>
          <span class="step-text">{{ $t('LandingPage.AboutMistakesJournal.Step2.Text') }}</span>
        </div>
        <img
          alt="Mistakes journal tutorial presentation"
          class="mj-landing-steps-image"
          :src="require('@/assets/icons/' + $i18n.locale + '/solution.svg')">
      </div>
    </div>
    <div class="row justify-content-center mt-4">
      <div class="col-12 col-md-6 col-xl-6 mj-about-page-section-with-image mj-about-page-section-5">
        <div class="advanced-steps">
          <span class="step-name">{{ $t('LandingPage.AboutMistakesJournal.Step3.StepName') }}</span>
          <span class="step-header">{{ $t('LandingPage.AboutMistakesJournal.Step3.Header') }}</span>
          <span class="step-text">{{ $t('LandingPage.AboutMistakesJournal.Step3.Text') }}</span>
          <span class="step-text">{{ $t('LandingPage.AboutMistakesJournal.Step3.Text2') }}</span>
        </div>
        <img
          alt="Mistakes journal tutorial presentation"
          class="mj-landing-steps-image"
          :src="require('@/assets/icons/' + $i18n.locale + '/mistakes.svg')">
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-12 col-lg-8 col-xl-7 mj-about-page-section-6">
        <div class="header-end-1">
          {{ $t('LandingPage.AboutMistakesJournal.HeaderEndPage1') }}
        </div>
        <span class="header-end-2">{{ $t('LandingPage.AboutMistakesJournal.HeaderEndPage2') }}</span>
        <span class="header-end-bold">{{ $t('LandingPage.AboutMistakesJournal.HeaderEndPageBold') }}</span>
      </div>
    </div>
    <div class="row">
      <div class="col-12 mt-5 text-center mj-about-page-section-7">
        <register-button>{{ $t('LandingPage.Nav.EndingPageButton') }}</register-button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import RegisterButton from '@/components/landing/RegisterButton.vue';
import ApplicationTitle from '@/components/navigation-bar/ApplicationTitle.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'AboutMistakesJournal',
  components: { ApplicationTitle, RegisterButton },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-about-page-section {
  $margin-top-4: 4rem;

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

  &-early-access {
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    border-radius: 1rem;
    background-color: mistakes-journal.color('secondary', '50');
    text-align: center;

    h5 {
      @include mistakes-journal.font-regular(1.2rem);
      margin-top: 1rem;

      @include mistakes-journal.media-breakpoint-up(md) {
        @include mistakes-journal.font-regular(1.4rem);
      }
    }

    p {
      @include mistakes-journal.font-regular(1rem);

      @include mistakes-journal.media-breakpoint-up(md) {
        @include mistakes-journal.font-regular(1.2rem);
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

  &-with-image {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-around;

    @include mistakes-journal.media-breakpoint-up(lg) {
      flex: 1;
      flex-direction: row;
      justify-content: space-evenly;
    }

    .mj-landing-steps-image {
      display: block;
      text-align: center;

      @include mistakes-journal.media-breakpoint-up(md) {
        width: 39.3rem;
        height: 41.5rem;
      }
    }

    .advanced-steps {
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      width: 100%;
      margin: 0 1rem;
      text-align: center;

      @include mistakes-journal.media-breakpoint-up(lg) {
        text-align: left;
      }
    }

    .step-name {
      margin-top: 2rem;
    }

    .step-header {
      @include mistakes-journal.font-semi-bold(1.8rem);
      margin: 1rem 0;

      @include mistakes-journal.media-breakpoint-up(lg) {
        @include mistakes-journal.font-semi-bold(3rem);
      }
    }

    .step-text {
      @include mistakes-journal.font-regular(1.2rem);
      margin-bottom: 2rem;

      @include mistakes-journal.media-breakpoint-up(lg) {
        @include mistakes-journal.font-regular(1.5rem);
      }
    }
  }

  &-4 {
    @include mistakes-journal.media-breakpoint-up(lg) {
      flex-direction: row-reverse;
    }
  }

  &-5 {
    margin-top: $margin-top-4;

    @include mistakes-journal.media-breakpoint-up(lg) {
      border-radius: 1rem;
      background-color: mistakes-journal.color('primary', '50');
    }
  }

  &-6 {
    margin-top: $margin-top-4;
    text-align: center;

    .header-end-1 {
      @include mistakes-journal.font-semi-bold(2.9rem);
      display: flex;
      flex-direction: column;

      @include mistakes-journal.media-breakpoint-up(lg) {
        @include mistakes-journal.font-semi-bold(3.5rem);
        white-space: nowrap;
      }
    }

    .header-end-2 {
      @include mistakes-journal.font-regular(1.4rem);
    }

    .header-end-bold {
      @include mistakes-journal.font-bold(1.4rem);
    }
  }

  &-7 {
    margin: 2rem 0 6rem;
  }
}
</style>
