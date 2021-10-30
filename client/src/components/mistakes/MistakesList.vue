<template>
  <div class="row">
    <div class="col-12">
      <add-new-mistake-button />
    </div>
    <div
      v-if="mistakes.length > 0"
      class="col-12 mt-4 mj-mistake-items">
      <div
        v-for="mistake in mistakes"
        :key="mistake.id"
        class="mj-mistake-item">
        <div class="mj-mistake-first-line">
          <div class="mj-mistake-item-title">
            <router-link
              :to="'/journal/mistakes/' + mistake.id"
              class="mj-mistake-item-link">
              {{ mistake.name }}
            </router-link>
          </div>
          <mistake-options-menu :mistake-id="mistake.id" />
        </div>
        <progress-bar :past-days="countMistakeDays(mistake)" />
        <div class="mj-priority-repetition">
          <mistake-priority :priority="mistake.priority" />
          <repetition-button
            :mistake-id="mistake.id"
            :repetition-counter="mistake.repetitionDates.length" />
        </div>
      </div>
    </div>
    <div
      v-else
      class="col-12 text-center my-4">
      {{ $t('Mistakes.EmptyList') }}
    </div>
    <div class="col-12">
      <mistakes-pagination />
    </div>
  </div>
</template>

<script lang="ts">
import MistakeOptionsMenu from '@/components/mistakes/MistakeOptionsMenu.vue';
import MistakePriority from '@/components/mistakes/MistakePriority.vue';
import MistakesPagination from '@/components/mistakes/MistakesPagination.vue';
import ProgressBar from '@/components/mistakes/ProgressBar.vue';
import RepetitionButton from '@/components/mistakes/RepetitionButton.vue';
import AddNewMistakeButton from '@/components/shared/AddNewMistakeButton.vue';
import { Mistake } from '@/model/mistake';
import { MistakesActions } from '@/store/mistakes-module/actions';
import moment from 'moment';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakesList',
  components: {
    MistakesPagination,
    ProgressBar,
    RepetitionButton,
    AddNewMistakeButton,
    MistakePriority,
    MistakeOptionsMenu,
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
      return moment()
        .diff(moment.max(mistake.repetitionDates), 'days');
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use 'sass:color';
@use '../../styles/mistakes-journal';

.mj-mistake-items {
  width: 100%;
}

.mj-mistake-item {
  margin-bottom: 1rem;

  &-link {
    @include mistakes-journal.font-medium;
    display: block;
    width: 100%;
    color: mistakes-journal.color('text');
  }

  &-title {
    flex: 1;
    padding: 0.25rem 1rem;
    border-radius: 0.125rem;

    &:hover {
      background-color: mistakes-journal.color('secondary', '50');
    }
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

</style>
