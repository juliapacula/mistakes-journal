<template>
  <v-tour
    :callbacks="tourCallbacks"
    :class="{'overlay' : canRunTour}"
    :steps="isMobile ? stepsMobile : stepsDesktop"
    name="addingRepetitionOnBoarding">
    <template slot-scope="tour">
      <transition name="fade">
        <v-step
          v-if="tour.steps[tour.currentStep]"
          :key="tour.currentStep"
          :class="{ 'mobile-layout': isMobile }"
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
              <div class="mj-onboard-change-language">
                <language-change-button :is-transparent="true" />
              </div>
            </div>
            <div slot="content">
              <div class="mj-onboard-content">
                <img
                  alt="Ice cream"
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
                    class="mj-onboard-content-simple-text">
                    {{ $t('Onboard.Step6.Text2') }}
                  </div>
                </div>
              </div>
            </div>
            <div
              v-if="tour.isLast"
              slot="actions">
              <button
                class="btn btn-primary with-icon mj-onboard-end-button"
                @click="tour.finish">
                <span class="btn-icon">
                  <fa-icon :icon="['far', 'laugh-beam']" />
                </span>
                <span class="btn-text">{{ $t('Onboard.GotIt') }}</span>
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
import { UserActions } from '@/store/user-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'OnboardRepetition',
  components: { LanguageChangeButton },
  data() {
    return {
      stepsDesktop: [
        {
          target: '#step-6',
          header: {
            title: 'Onboard.Step6.Header',
          },
          content: 'Onboard.Step6.Text',
        },
      ],
      stepsMobile: [
        {
          target: false,
          header: {
            title: 'Onboard.Step6.Header',
          },
          content: 'Onboard.Step6.Text',
        },
      ],
      isMobile: window.innerWidth < 768,
    };
  },
  computed: {
    userWatchedTutorial(): boolean {
      return this.$store.state.user?.user.watchedTutorial ?? false;
    },
    canRunTour(): boolean {
      return !this.userWatchedTutorial && this.$store.state.uiState.currentOnBoardingTourStep === OnBoardingTourSteps.AddingRepetition;
    },
    tourCallbacks(): object {
      return {
        onPrevStep: this.updateIsMobile,
        onNextStep: this.updateIsMobile,
        onStop: this.stopTutorial,
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
      if (this.canRunTour && !this.$tours.addingRepetitionOnBoarding.isRunning) {
        this.$tours.addingRepetitionOnBoarding.start();
      }
    },
    async stopTutorial(): Promise<void> {
      await this.$store.dispatch(UserActions.UpdateUserTutorialState, true);
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
