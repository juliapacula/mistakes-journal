<template>
  <div class="mj-mistake-list">
    <mistakes-list-applied-filters />
    <add-new-mistake-button />
    <div
      v-if="mistakes.length > 0"
      class="mt-4 mj-mistake-items">
      <div
        v-for="(mistake, index) in mistakes"
        :key="mistake.id"
        class="mj-mistake-item">
        <div class="mj-mistake-first-line">
          <router-link
            :to="'/journal/mistakes/' + mistake.id"
            class="mj-mistake-item-title">
            {{ mistake.name }}
          </router-link>
          <mistake-options-menu :mistake-id="mistake.id" />
        </div>
        <mistake-labels-list :labels="mistake.labels" />
        <div class="mj-mistake-item-date">
          {{ mistake.createdAt | formattedDate }}
        </div>
        <progress-bar :past-days="countMistakeDays(mistake)" />
        <div class="mj-mistake-item-days">
          <span class="success-past-days">{{ countMistakeDays(mistake) }}</span>
          <span>/{{ MISTAKES_COUNTING_DAYS }} {{ $t('Progress.Days') }}</span>
        </div>
        <div class="mj-priority-repetition">
          <mistake-priority :priority="mistake.priority" />
          <repetition-button
            :id="index === 0 ? 'step-6' : undefined"
            :mistake-id="mistake.id"
            :repetition-counter="mistake.repetitionDates.length" />
        </div>
      </div>
    </div>
    <mistakes-list-no-mistakes
      v-else
      class="my-4" />
    <mistakes-pagination v-if="mistakes.length > 0" />
    <adding-repetition-on-boarding v-if="mistakes.length > 0" />
    <mistakes-list-on-boarding />
  </div>
</template>

<script lang="ts">
import MistakeLabelsList from '@/components/mistakes/MistakeLabelsList.vue';
import MistakeOptionsMenu from '@/components/mistakes/MistakeOptionsMenu.vue';
import MistakePriority from '@/components/mistakes/MistakePriority.vue';
import MistakesListAppliedFilters from '@/components/mistakes/MistakesListAppliedFilters.vue';
import MistakesListNoMistakes from '@/components/mistakes/MistakesListNoMistakes.vue';
import MistakesPagination from '@/components/mistakes/MistakesPagination.vue';
import ProgressBar from '@/components/mistakes/ProgressBar.vue';
import RepetitionButton from '@/components/mistakes/RepetitionButton.vue';
import AddingRepetitionOnBoarding from '@/components/on-boarding/AddingRepetitionOnBoarding.vue';
import MistakesListOnBoarding from '@/components/on-boarding/MistakesListOnBoarding.vue';
import AddNewMistakeButton from '@/components/shared/AddNewMistakeButton.vue';
import { MISTAKES_COUNTING_DAYS } from '@/config/mistakes.config';
import { Mistake } from '@/model/mistake';
import { MistakesActions } from '@/store/mistakes-module/actions';
import moment from 'moment';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakesList',
  components: {
    AddingRepetitionOnBoarding,
    MistakesListOnBoarding,
    MistakesListAppliedFilters,
    MistakesListNoMistakes,
    MistakeLabelsList,
    MistakesPagination,
    ProgressBar,
    RepetitionButton,
    AddNewMistakeButton,
    MistakePriority,
    MistakeOptionsMenu,
  },
  data() {
    return {
      MISTAKES_COUNTING_DAYS,
    };
  },
  computed: {
    mistakes(): Mistake[] {
      return this.$store.state.mistakes.mistakes;
    },
  },
  async beforeCreate(): Promise<void> {
    await this.$store.dispatch(MistakesActions.GetAll);
  },
  methods: {
    countMistakeDays(mistake: Mistake): Number {
      const now = moment().startOf('date');

      if (mistake.repetitionDates.length === 0) {
        return now.diff(mistake.createdAt.startOf('date'), 'days');
      }

      return now.diff(moment.max(mistake.repetitionDates).startOf('date'), 'days');
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use 'sass:color';
@use '../../styles/mistakes-journal';

.mj-mistake-list {
  display: flex;
  flex-direction: column;
  align-items: start;
}

.mj-mistake-items {
  width: 100%;
}

.mj-mistake-item {
  margin-bottom: 1rem;

  &-title {
    @include mistakes-journal.font-medium;
    flex: 1;
    width: 100%;
    padding: 0.25rem 1rem;
    overflow: hidden;
    border-radius: 0.125rem;
    color: mistakes-journal.color('text');
    text-overflow: ellipsis;
    white-space: nowrap;

    &:hover {
      background-color: mistakes-journal.color('secondary', '300');
    }

    &:focus,
    &:active {
      background-color: mistakes-journal.color('secondary', '50');
    }
  }

  &-date {
    @include mistakes-journal.font-regular(0.69rem);
    padding-left: 1rem;
    color: mistakes-journal.color('gray', '400');
  }

  &-days {
    @include mistakes-journal.font-semi-bold(0.83rem);
    display: flex;
    justify-content: right;
    margin-right: 2rem;
    margin-bottom: 0.5rem;
    color: mistakes-journal.color('gray', '300');
  }
}

.mj-mistake-first-line {
  display: flex;
  align-items: center;
}

a {
  text-decoration: none;

  &:hover,
  &:focus,
  &:active {
    text-decoration: none;
  }
}

.mj-priority-repetition {
  display: flex;
  position: relative;
  align-items: center;
  justify-content: space-between;
}

.success-past-days {
  color: mistakes-journal.color('primary', '700');
}
</style>
