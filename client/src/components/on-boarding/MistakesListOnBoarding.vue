<template>
  <v-tour
    :callbacks="tourCallbacks"
    :class="{'overlay': canRunTour}"
    :options="{ highlight: true }"
    :steps="isMobile ? stepsMobile : stepsDesktop"
    name="mistakesListOnBoarding">
    <template slot-scope="tour">
      <transition name="fade">
        <v-step
          v-if="tour.steps[tour.currentStep]"
          :key="tour.currentStep"
          :class="{ 'mobile-layout': isMobile }"
          :highlight="tour.highlight"
          :is-first="tour.isFirst"
          :is-last="tour.isLast"
          :labels="tour.labels"
          :next-step="tour.nextStep"
          :previous-step="tour.previousStep"
          :skip="tour.skip"
          :step="tour.steps[tour.currentStep]"
          :stop="tour.stop"
          class="mj-step">
          <template>
            <div slot="header">
              <div class="v_step__header" />
              <div v-if="isMobile">
                <button
                  class="btn mj-onboard-button-skip mj-skip-mobile"
                  type="button"
                  @click="tour.skip()">
                  <span class="btn-text">{{ $t('Onboard.Skip') }}</span>
                </button>
              </div>
            </div>
            <div slot="content">
              <div class="mj-onboard-change-language">
                <language-change-button :is-transparent="true" />
              </div>
              <div class="mj-onboard-content">
                <img
                  :src="require('@/assets/icons/onboard_step_' + tour.currentStep +'.svg')"
                  alt=""
                  class="mj-onboard-content-icon">
                <div v-if="isMobile">
                  <img
                    v-if="tour.currentStep === 1"
                    alt="Menu button"
                    src="@/assets/icons/onboard_mobile_1.svg">
                  <img
                    v-else-if="tour.currentStep === 2"
                    alt="Menu button"
                    src="@/assets/icons/onboard_mobile_2.svg">
                  <img
                    v-else-if="tour.currentStep === 3"
                    alt="Menu button"
                    src="@/assets/icons/onboard_mobile_3.svg">
                  <img
                    v-else-if="tour.currentStep === 4"
                    alt="Menu button"
                    src="@/assets/icons/onboard_mobile_4.svg">
                </div>
                <div class="mj-onboard-content-texts">
                  <div
                    slot="header"
                    class="mj-onboard-content-header">
                    {{ $t(tour.steps[tour.currentStep].header.title) }}
                  </div>
                  <div class="mj-onboard-content-simple-text">
                    {{ $t(tour.steps[tour.currentStep].content) }}
                  </div>
                </div>
              </div>
            </div>
            <div
              v-if="tour.isFirst"
              slot="actions"
              :class="{'mj-onboard-two-buttons' : !isMobile}">
              <button
                v-if="isMobile !== true"
                class="btn mj-onboard-button-skip"
                type="button"
                @click="tour.skip()">
                <span class="btn-text">{{ $t('Onboard.Skip') }}</span>
              </button>
              <button
                :class="{ 'mj-button-proceed': isMobile }"
                class="btn btn-primary with-icon"
                type="button"
                @click="tour.nextStep">
                <span class="btn-icon">
                  <fa-icon :icon="['far', 'laugh-beam']" />
                </span>
                <span class="btn-text">{{ $t('Onboard.ButtonStart') }}</span>
              </button>
            </div>
            <div
              v-else-if="tour.isLast"
              slot="actions">
              <on-boarding-progress-bar
                :current-step="tour.currentStep"
                :is-mobile="isMobile" />
              <button
                :class="{ 'mj-button-proceed': isMobile }"
                class="btn btn-primary with-icon mj-onboard-end-button"
                type="button"
                @click="tour.finish">
                <span class="btn-icon">
                  <fa-icon :icon="['far', 'laugh-beam']" />
                </span>
                <span class="btn-text">{{ $t('Onboard.GotIt') }}</span>
              </button>
            </div>
            <div
              v-else
              slot="actions"
              :class="{'mj-onboard-two-buttons' : !isMobile}">
              <button
                v-if="!isMobile"
                class="btn mj-onboard-button-skip"
                type="button"
                @click="tour.skip()">
                <span class="btn-text">{{ $t('Onboard.Skip') }}</span>
              </button>
              <on-boarding-progress-bar
                :current-step="tour.currentStep"
                :is-mobile="isMobile" />
              <button
                :class="{ 'mj-button-proceed': isMobile }"
                class="btn btn-primary with-icon"
                type="button"
                @click="tour.nextStep">
                <span class="btn-icon">
                  <fa-icon :icon="['far', 'laugh-beam']" />
                </span>
                <span class="btn-text">{{ $t('Onboard.Next') }}</span>
              </button>
            </div>
            <div class="v-step__arrow" />
          </template>
        </v-step>
      </transition>
    </template>
  </v-tour>
</template>

<script lang="ts">
import LanguageChangeButton from '@/components/shared/LanguageChangeButton.vue';
import { GOOGLE_EVENTS } from '@/config/google-analytics-events.config';
import { OnBoardingTourSteps } from '@/model/on-boarding-tour-steps.enum';
import { UiStateActions } from '@/store/ui-state-module/actions';
import { UiStateMutations } from '@/store/ui-state-module/mutations';
import { UserActions } from '@/store/user-module/actions';
import Vue from 'vue';
import { event } from 'vue-gtag';
import OnBoardingProgressBar from './OnBoardingProgressBar.vue';

// Declaration for current component.
declare module 'vue/types/vue' {
  interface Vue {
    showSidebarIfHidden: () => void;
  }
}

export default Vue.extend({
  name: 'MistakesListOnBoarding',
  components: { LanguageChangeButton, OnBoardingProgressBar },
  data() {
    return {
      stepsDesktop: [
        {
          target: false,
          header: {
            title: 'Onboard.Step0.Header',
          },
          content: 'Onboard.Step0.Text',
          params: {
            placement: 'none',
            highlight: false,
          },
        },
        {
          target: '#step-1',
          header: {
            title: 'Onboard.Step1.Header',
          },
          content: 'Onboard.Step1.Text',
          params: {
            placement: 'right',
          },
          before: this.showSidebarIfHidden,
        },
        {
          target: '#step-2',
          header: {
            title: 'Onboard.Step2.Header',
          },
          content: 'Onboard.Step2.Text',
          params: {
            placement: 'right',
          },
          before: this.showSidebarIfHidden,
        },
        {
          target: '#step-3',
          header: {
            title: 'Onboard.Step3.Header',
          },
          content: 'Onboard.Step3.Text',
          params: {
            placement: 'right',
          },
          before: this.showSidebarIfHidden,
        },
        {
          target: '#step-4',
          header: {
            title: 'Onboard.Step4.Header',
          },
          content: 'Onboard.Step4.Text',
          params: {
            placement: 'right',
          },
          before: this.showSidebarIfHidden,
        },
      ],
      stepsMobile: [
        {
          target: false,
          header: {
            title: 'Onboard.Step0.Header',
          },
          content: 'Onboard.Step0.Text',
          params: {
            placement: 'none',
            highlight: false,
          },
        },
        {
          target: false,
          header: {
            title: 'Onboard.Step1.Header',
          },
          content: 'Onboard.Step1.Text',
          params: {
            placement: 'none',
            highlight: false,
          },
        },
        {
          target: false,
          header: {
            title: 'Onboard.Step2.Header',
          },
          content: 'Onboard.Step2.Text',
          params: {
            placement: 'none',
            highlight: false,
          },
        },
        {
          target: false,
          header: {
            title: 'Onboard.Step3.Header',
          },
          content: 'Onboard.Step3.Text',
          params: {
            placement: 'none',
            highlight: false,
          },
        },
        {
          target: false,
          header: {
            title: 'Onboard.Step4.Header',
          },
          content: 'Onboard.Step4.Text',
          params: {
            placement: 'none',
            highlight: false,
          },
        },
      ],
      isMobile: window.innerWidth < 768,
    };
  },
  computed: {
    userWatchedTutorial(): boolean {
      return this.$store.state.user.user.watchedTutorial ?? false;
    },
    isTourRunning(): boolean {
      return this.$tours.mistakesListOnBoarding?.isRunning ?? false;
    },
    canRunTour(): boolean {
      return !this.userWatchedTutorial && this.$store.state.uiState.currentOnBoardingTourStep === OnBoardingTourSteps.MistakesList;
    },
    tourCallbacks(): object {
      return {
        onPrevStep: this.updateIsMobile,
        onNextStep: this.updateIsMobile,
        onStop: this.stopTutorial,
        onSkip: this.skipTutorial,
      };
    },
  },
  watch: {
    canRunTour(): void {
      this.startTour();
    },
  },
  mounted() {
    this.startTour();
  },
  methods: {
    startTour(): void {
      if (this.canRunTour && !this.$tours.mistakesListOnBoarding.isRunning) {
        this.$tours.mistakesListOnBoarding.start();
      }
    },
    async skipTutorial(): Promise<void> {
      event(GOOGLE_EVENTS.SKIP_TUTORIAL);
      await this.$store.dispatch(UserActions.UpdateUserTutorialState, true);
      await this.$store.dispatch(UiStateActions.ResetUserOnBoardingTour);
    },
    async stopTutorial(): Promise<void> {
      await this.$store.dispatch(UiStateActions.NextOnBoardingTourStep);
    },
    async showSidebarIfHidden(): Promise<void> {
      const shouldWait = !this.$store.state.uiState.isSidebarVisible ?? false;

      if (shouldWait) {
        this.$store.commit(UiStateMutations.ExpandSidebar);

        // Wait for sidebar to be displayed.
        await new Promise((res) => setTimeout(res, 600));
      }
    },
    async updateIsMobile(): Promise<void> {
      this.isMobile = window.innerWidth < 768;
      await this.showSidebarIfHidden();
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/components/mj-tour';
</style>
