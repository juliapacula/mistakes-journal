<template>
  <v-tour
    :class="{'overlay' : canRunTour}"
    :steps="isMobile ? stepsMobile : stepsDesktop"
    :callbacks="tourCallbacks"
    name="addingMistakeOnboarding">
    <template slot-scope="tour">
      <transition name="fade">
        <v-step
          v-if="tour.steps[tour.currentStep]"
          :key="tour.currentStep"
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
            </div>
            <div slot="content">
              <div class="mj-onboard-change-language">
                <language-change-button :is-transparent="true" />
              </div>
              <div class="mj-onboard-content">
                <img
                  v-if="tour.currentStep === 0"
                  alt=""
                  class="mj-onboard-content-icon"
                  src="@/assets/icons/onboard_step_5.svg">
                <img
                  v-else-if="tour.currentStep === 1"
                  alt=""
                  class="mj-onboard-content-icon"
                  src="@/assets/icons/onboard_step_6.svg">
                <div class="mj-onboard-content-texts">
                  <div
                    slot="header"
                    class="mj-onboard-content-header">
                    {{ $t(tour.steps[tour.currentStep].header.title) }}
                  </div>
                  <div class="mj-onboard-content-simple-text">
                    {{ $t(tour.steps[tour.currentStep].content) }}
                  </div>
                  <div
                    v-if="tour.currentStep === 1"
                    class="mj-onboard-content-simple-text">
                    {{ $t('Onboard.Step6.Text2') }}
                  </div>
                </div>
              </div>
            </div>
            <div
              v-if="!tour.isLast"
              slot="actions">
              <button
                :class="{ 'mj-button-proceed': isMobile }"
                class="btn btn-primary with-icon mj-onboard-end-button"
                @click="tour.nextStep">
                <span class="btn-icon">
                  <fa-icon
                    :icon="['far', 'laugh-beam']" />
                </span>
                <span class="btn-text ">
                  {{ $t('Onboard.GotIt') }}
                </span>
              </button>
            </div>
            <div
              v-if="tour.isLast"
              slot="actions">
              <button
                :class="{ 'mj-button-proceed': isMobile }"
                class="btn btn-primary with-icon mj-onboard-end-button"
                @click="tour.finish">
                <span class="btn-icon">
                  <fa-icon
                    :icon="['far', 'laugh-beam']" />
                </span>
                <span class="btn-text">
                  {{ $t('Onboard.GotIt') }}
                </span>
              </button>
            </div>
          </template>
        </v-step>
      </transition>
    </template>
  </v-tour>
</template>

<script lang="ts">
import LanguageChangeButton from '@/components/shared/LanguageChangeButton.vue';
import { OnBoardingTourSteps } from '@/model/on-boarding-tour-steps.enum';
import { UiStateActions } from '@/store/ui-state-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'OnboardAddingMistake',
  components: { LanguageChangeButton },
  data() {
    return {
      stepsDesktop: [
        {
          target: '#step-5',
          header: {
            title: 'Onboard.Step5.Header',
          },
          content: 'Onboard.Step5.Text',
          params: {
            placement: 'right',
          },
        },
      ],
      stepsMobile: [
        {
          target: false,
          header: {
            title: 'Onboard.Step5.Header',
          },
          content: 'Onboard.Step5.Text',
          params: {
            placement: 'none',
          },
        },
      ],
      isMobile: window.innerWidth < 768,
    };
  },
  computed: {
    userWatchedTutorial(): boolean {
      return this.$store.state.user?.user.watchedTutorial ?? false;
    },
    isTourRunning(): boolean {
      return this.$tours.addingMistakeOnboarding?.isRunning ?? false;
    },
    canRunTour(): boolean {
      return !this.userWatchedTutorial && this.$store.state.uiState.currentOnBoardingTourStep === OnBoardingTourSteps.AddingMistake;
    },
    tourCallbacks(): object {
      return {
        onStop: this.stopTutorial,
      };
    },
  },
  watch: {
    canRunTour(): void {
      this.startTour();
    },
  },
  created() {
    window.addEventListener('resize', this.updateIsMobile);
  },
  mounted() {
    this.startTour();
  },
  destroyed() {
    window.removeEventListener('resize', this.updateIsMobile);
  },
  methods: {
    startTour(): void {
      if (this.canRunTour && !this.$tours.addingMistakeOnboarding.isRunning) {
        this.$tours.addingMistakeOnboarding.start();
      }
    },
    async stopTutorial(): Promise<void> {
      await this.$store.dispatch(UiStateActions.NextOnBoardingTourStep);
    },
    updateIsMobile(): void {
      this.isMobile = window.innerWidth < 768;
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/components/mj-tour';
</style>
