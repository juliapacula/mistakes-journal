<template>
  <v-tour
    :steps="isMobile ? stepsMobile : stepsDesktop"
    name="myTour2">
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
              <div class="mj-onboard-content">
                <div>
                  <img
                    v-if="tour.currentStep === 0"
                    alt="Ice cream"
                    class="mj-onboard-content-icon"
                    src="@/assets/icons/onboard_step_5.svg">
                  <img
                    v-else-if="tour.currentStep === 1"
                    alt="Ice cream"
                    class="mj-onboard-content-icon"
                    src="@/assets/icons/onboard_step_6.svg">
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
                <span class="btn-text ">{{ $t('Onboard.GotIt') }}</span>
              </button>
            </div>
            <div
              v-if="tour.isLast"
              slot="actions">
              <button
                :class="{ 'mj-button-proceed': isMobile }"
                class="btn btn-primary with-icon mj-onboard-end-button"
                @click="tour.stop">
                <span class="btn-icon">
                  <fa-icon
                    :icon="['far', 'laugh-beam']" />
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
import Vue from 'vue';

export default Vue.extend({
  name: 'OnboardAddingMistake',
  data() {
    return {
      stepsDesktop: [
        {
          target: '#step-5',
          header: {
            title: 'Onboard.Step5.Header',
          },
          content: 'Onboard.Step5.Text',
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
        {
          target: false,
          header: {
            title: 'Onboard.Step6.Header',
          },
          content: 'Onboard.Step6.Text',
          params: {
            placement: 'none',
          },
        },
      ],
      isMobile: window.innerWidth < 768,
    };
  },
  created() {
    window.addEventListener('resize', this.updateIsMobile);
  },
  mounted() {
    if (!this.$tours.myTour2.isRunning) {
      this.$tours.myTour2.start();
    }
  },
  destroyed() {
    window.removeEventListener('resize', this.updateIsMobile);
  },
  methods: {
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
  padding-top: 2rem;

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
      padding-top: 0;
      padding-left: 1rem;
      text-align: left;
    }
  }
}

.mj-step {
  &.v-step {
    &.v-step {
      background-color: mistakes-journal.color('gray', '100');
      color: mistakes-journal.color('text');

      @include mistakes-journal.media-breakpoint-up(md) {
        max-width: 32rem;
        max-height: 17rem;
        border-radius: 1rem;
      }

      &--sticky {
        width: 100%;
        max-width: 100%;
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
}

::v-deep .v-step__arrow--dark {
  &:before {
    background: mistakes-journal.color('gray', '100');
  }
}
</style>
