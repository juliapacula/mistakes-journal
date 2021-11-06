<template>
  <div>
    <v-tour
      :class="{'overlay' : isTourActive}"
      :options="{ highlight: true }"
      :steps="isMobile === true ? stepsMobile : stepsDesktop"
      name="myTour">
      <template slot-scope="tour">
        <transition name="fade">
          <v-step
            v-if="tour.steps[tour.currentStep]"
            :key="tour.currentStep"
            :highlight="tour.highlight"
            :is-first="tour.isFirst"
            :is-last="tour.isLast"
            :labels="tour.labels"
            :next-step="tour.nextStep"
            :previous-step="tour.previousStep"
            :skip="tour.skip"
            :step="tour.steps[tour.currentStep]"
            :stop="tour.stop">
            <template>
              <div slot="header">
                <div class="v_step__header" />
              </div>
              <div
                v-if="isMobile"
                slot="actions">
                <button
                  class="btn mj-onboard-button-skip mj-skip-mobile"
                  @click="skipTutorial()">
                  <span class="btn-text">
                    {{ $t('Onboard.Skip') }}</span>
                </button>
              </div>
              <div slot="content">
                <div class="mj-onboard-content">
                  <div>
                    <img
                      v-if="tour.currentStep === 0"
                      alt="Ice cream"
                      class="mj-onboard-content-icon"
                      src="@/assets/icons/onboard_step_0.svg">
                    <img
                      v-else-if="tour.currentStep === 1"
                      alt="Ice cream"
                      class="mj-onboard-content-icon"
                      src="@/assets/icons/onboard_step_1.svg">
                    <img
                      v-else-if="tour.currentStep === 2"
                      alt="Ice cream"
                      class="mj-onboard-content-icon"
                      src="@/assets/icons/onboard_step_2.svg">
                    <img
                      v-else-if="tour.currentStep === 3"
                      alt="Ice cream"
                      class="mj-onboard-content-icon"
                      src="@/assets/icons/onboard_step_3.svg">
                    <img
                      v-else-if="tour.currentStep === 4"
                      alt="Ice cream"
                      class="mj-onboard-content-icon"
                      src="@/assets/icons/onboard_step_4.svg">
                  </div>
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
                      {{ tour.steps[tour.currentStep].header.title }}
                    </div>
                    <div class="mj-onboard-content-simple-text">
                      {{ tour.steps[tour.currentStep].content }}
                    </div>
                  </div>
                </div>
              </div>
              <div
                v-if="tour.currentStep === 0"
                slot="actions"
                :class="{'mj-onboard-two-buttons' : !isMobile}">
                <button
                  v-if="isMobile !== true"
                  class="btn mj-onboard-button-skip"
                  @click="skipTutorial()">
                  <span class="btn-text">
                    {{ $t('Onboard.Skip') }}</span>
                </button>
                <onboard-progress
                  :current-step="tour.currentStep"
                  :is-mobile="isMobile" />
                <button
                  :class="{ 'mj-button-proceed': isMobile }"
                  class="btn btn-primary with-icon"
                  @click="tour.nextStep">
                  <span class="btn-icon">
                    <fa-icon
                      :icon="['far', 'laugh-beam']" />
                  </span>
                  <span class="btn-text">
                    {{ $t('Onboard.ButtonStart') }}</span>
                </button>
              </div>
              <div
                v-else-if="tour.isLast"
                slot="actions">
                <onboard-progress
                  :current-step="tour.currentStep"
                  :is-mobile="isMobile" />
                <button
                  :class="{ 'mj-button-proceed': isMobile }"
                  class="btn btn-primary with-icon mj-onboard-end-button"
                  @click="turnOffTour()">
                  <span class="btn-icon">
                    <fa-icon
                      :icon="['far', 'laugh-beam']" />
                  </span>
                  <span class="btn-text">
                    {{ $t('Onboard.GotIt') }}</span>
                </button>
              </div>
              <div
                v-else
                slot="actions"
                :class="{'mj-onboard-two-buttons' : !isMobile}">
                <button
                  v-if="isMobile !== true"
                  class="btn mj-onboard-button-skip"
                  @click="skipTutorial()">
                  <span class="btn-text">
                    {{ $t('Onboard.Skip') }}</span>
                </button>
                <onboard-progress
                  :current-step="tour.currentStep"
                  :is-mobile="isMobile" />
                <button
                  :class="{ 'mj-button-proceed': isMobile }"
                  class="btn btn-primary with-icon"
                  @click="tour.nextStep">
                  <span class="btn-icon">
                    <fa-icon
                      :icon="['far', 'laugh-beam']" />
                  </span>
                  <span class="btn-text">
                    {{ $t('Onboard.Next') }}</span>
                </button>
              </div>
              <div class="v-step__arrow" />
            </template>
          </v-step>
        </transition>
      </template>
    </v-tour>
  </div>
</template>

<script lang="ts">
import OnboardProgress from '@/components/shared/OnboardProgress.vue';
import { UserActions } from '@/store/user-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'UserOnboard',
  components: { OnboardProgress },
  data() {
    return {
      stepsDesktop: [
        {
          target: false,
          header: {
            title: this.$t('Onboard.Step0.Header'),
          },
          content: this.$t('Onboard.Step0.Text'),
          params: {
            placement: 'none',
            highlight: false,
          },
        },
        {
          target: '#step-1',
          header: {
            title: this.$t('Onboard.Step1.Header'),
          },
          content: this.$t('Onboard.Step1.Text'),
          params: {
            placement: 'right',
          },
        },
        {
          target: '#step-2',
          header: {
            title: this.$t('Onboard.Step2.Header'),
          },
          content: this.$t('Onboard.Step2.Text'),
          params: {
            placement: 'right',
          },
        },
        {
          target: '#step-3',
          header: {
            title: this.$t('Onboard.Step3.Header'),
          },
          content: this.$t('Onboard.Step3.Text'),
          params: {
            placement: 'right',
          },
        },
        {
          target: '#step-4',
          header: {
            title: this.$t('Onboard.Step4.Header'),
          },
          content: this.$t('Onboard.Step4.Text'),
          params: {
            placement: 'right',
          },
        },
      ],
      stepsMobile: [
        {
          target: false,
          header: {
            title: this.$t('Onboard.Step0.Header'),
          },
          content: this.$t('Onboard.Step0.Text'),
          params: {
            placement: 'none',
            highlight: false,
          },
        },
        {
          target: false,
          header: {
            title: this.$t('Onboard.Step1.Header'),
          },
          content: this.$t('Onboard.Step1.Text'),
          params: {
            placement: 'none',
            highlight: false,
          },
        },
        {
          target: false,
          header: {
            title: this.$t('Onboard.Step2.Header'),
          },
          content: this.$t('Onboard.Step2.Text'),
          params: {
            placement: 'none',
            highlight: false,
          },
        },
        {
          target: false,
          header: {
            title: this.$t('Onboard.Step3.Header'),
          },
          content: this.$t('Onboard.Step3.Text'),
          params: {
            placement: 'none',
            highlight: false,
          },
        },
        {
          target: false,
          header: {
            title: this.$t('Onboard.Step4.Header'),
          },
          content: this.$t('Onboard.Step4.Text'),
          params: {
            placement: 'none',
            highlight: false,
          },
        },
      ],
      isMobile: window.innerWidth < 768,
      isTourActive: false,
    };
  },
  computed: {
    userWatchedTutorial(): boolean {
      const { user } = this.$store.state.user;
      if (user === null) {
        return false;
      }
      return user.watchedTutorial;
    },
  },
  created() {
    window.addEventListener('resize', this.updateIsMobile);
  },
  destroyed() {
    window.removeEventListener('resize', this.updateIsMobile);
  },
  mounted() {
    if (!this.userWatchedTutorial) {
      this.$tours.myTour.start();
      this.isTourActive = true;
    }
  },
  methods: {
    async skipTutorial(): Promise<void> {
      this.isTourActive = false;
      await this.$tours.myTour.finish();
      await this.$store.dispatch(UserActions.ChangeWhetherWatchedTutorial, true);
    },
    turnOffTour() : void {
      this.isTourActive = false;
      this.$tours.myTour.finish();
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
@use 'sass:color';
@use '../../styles/mistakes-journal';

.mj-onboard-button-skip {
  margin-right: 1em;
  border-color: mistakes-journal.color('gray', '400');
  color: mistakes-journal.color('gray', '400');
  font-size: 0.75em;
  white-space: nowrap;

  &:hover {
    border-color: mistakes-journal.color('gray', '600');
    color: mistakes-journal.color('gray', '600');
  }
}

.mj-onboard-two-buttons {
  display: flex;
  justify-content: space-between;
}

.mj-button-proceed {
  display: flex;
  position: fixed;
  bottom: 0;
  left: 50%;
  align-items: center;
  justify-content: center;
  margin-bottom: 3rem;
  transform: translateX(-50%);
  white-space: nowrap;
}

.mj-onboard-end-button {
  @include mistakes-journal.media-breakpoint-up(md) {
    display: flex;
    position: fixed;
    right: 0;
    bottom: 0;
    align-items: center;
    justify-content: center;
    margin-right: 1rem;
    margin-bottom: 0.5rem;
    white-space: nowrap;
  }
}

.mj-onboard-content {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-around;
  padding-top: 4rem;

  @include mistakes-journal.media-breakpoint-up(md) {
    flex-direction: row;
    padding-top: 0;
  }

  &-icon {
    flex-grow: 1;
    padding: 1rem 0;
  }

  &-simple-text {
    @include mistakes-journal.font-regular(1.2rem);
    flex-grow: 1;
    flex-shrink: 1;
    max-width: 17.5rem;
    padding-top: 1rem;
    padding-left: 1rem;
    text-align: left;

    @include mistakes-journal.media-breakpoint-up(md) {
      padding-top: 0;
      padding-left: 1rem;
    }
  }

  &-header {
    @include mistakes-journal.font-semi-bold(1.2rem);
    margin: 0;
    padding-top: 2rem;

    @include mistakes-journal.media-breakpoint-up(md) {
      margin: 0;
      padding-top: 0;
      padding-left: 1rem;
      text-align: left;
    }
  }
}

.mj-step {
  &.v-step {
    background-color: mistakes-journal.color('gray', '100');
    color: mistakes-journal.color('text');

    @include mistakes-journal.media-breakpoint-up(md) {
      max-width: 32rem;
      max-height: 17rem;
      border-radius: 1rem;
    }

    &--sticky {
      max-width: 100%;
      width: 100%;
      height: 100%;
      background-color: mistakes-journal.color('gray', '100');
      color: mistakes-journal.color('text');

      @include mistakes-journal.media-breakpoint-up(md) {
        max-width: 32rem;
        max-height: 17rem;
        border-radius: 1rem;
      }
    }
  }

  .v-step__header {
    display: none;
  }
}

.v-step--sticky[data-v-54f9a632] {
  position: absolute;
  flex-grow: 1;
  width: 100vw;
  max-width: 48rem;
  height: 100%;
  background-color: mistakes-journal.color('gray', '100');
  color: mistakes-journal.color('text');

  @include mistakes-journal.media-breakpoint-up(md) {
    max-width: 32rem;
    max-height: 17rem;
    border-radius: 1rem;
  }
}

.overlay {
  display: block;
  position: fixed;
  z-index: 2000;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.27);
}

.mj-skip-mobile {
  position: absolute;
  top: 0;
  right: 0;
  margin: 2rem;
}

::v-deep .v-step__arrow--dark {
  &:before {
    background: mistakes-journal.color('gray', '100') !important;
  }
}

.v-tour__target--highlighted {
  box-shadow: 0 0 0 5px mistakes-journal.color('gray', '700');
}
</style>
