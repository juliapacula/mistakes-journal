<template>
  <div v-if="!isMobile">
    <v-tour name="myTour3">
      <template slot-scope="tour">
        <transition name="fade">
          <v-step
            v-if="tour.steps[tour.currentStep]"
            :key="tour.currentStep"
            :step="tour.steps[tour.currentStep]"
            :previous-step="tour.previousStep"
            :next-step="tour.nextStep"
            :stop="tour.stop"
            :skip="tour.skip"
            :is-first="tour.isFirst"
            :is-last="tour.isLast"
            :labels="tour.labels"
            class="mj-step">
            <template>
              <div slot="header">
                <div class="v_step__header" />
              </div>
              <div slot="content">
                <div class="mj-onboard-content">
                  <div>
                    <img
                      class="mj-onboard-content-icon"
                      alt="Ice cream"
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
                  @click="stopTour()">
                  <span class="btn-icon">
                    <fa-icon
                      :icon="['far', 'laugh-beam']" />
                  </span>
                  <span class="btn-text">
                    {{ $t('Onboard.GotIt') }}</span>
                </button>
              </div>
            </template>
          </v-step>
        </transition>
      </template>
    </v-tour>
  </div>
</template>

<script lang="ts">
import { UiStateActions } from '@/store/ui-state-module/actions';
import { UserActions } from '@/store/user-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'OnboardRepetition',
  data() {
    return {
      stepsDesktop: [
        {
          target: '.step-6',
          header: {
            title: this.$t('Onboard.Step6.Header'),
          },
          content: this.$t('Onboard.Step6.Text'),
        },
      ],
      isMobile: window.innerWidth < 768,
    };
  },
  computed: {
    userWatchedTutorial(): boolean {
      return this.$store.state.user?.user.watchedTutorial ?? false;
    },
  },
  created() {
    window.addEventListener('resize', this.updateIsMobile);
  },
  destroyed() {
    window.removeEventListener('resize', this.updateIsMobile);
  },
  mounted() {
    if (this.$store.state.uiState.whichUserTour === 3 && !this.$tours.myTour3.isRunning && !this.userWatchedTutorial && !this.isMobile) {
      this.$tours.myTour3.start();
    }
  },
  methods: {
    async stopTour(): Promise<void> {
      await this.$tours.myTour3.finish();
      await this.$store.dispatch(UserActions.ChangeWhetherWatchedTutorial, true);
      await this.$store.dispatch(UiStateActions.ChangeWhichUserTour, 1);
    },
    updateIsMobile(): void {
      this.isMobile = window.innerWidth < 768;
    },
  },
});
</script>

<style
  scoped
  lang="scss">
@use 'sass:color';
@use '../../styles/mistakes-journal';

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
