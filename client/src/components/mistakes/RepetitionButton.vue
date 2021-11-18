<template>
  <button
    class="btn btn-outline-repetition mj-repetition with-icon"
    type="button"
    @click="addRepetition()">
    <span class="btn-icon">
      <remix-icon
        icon="restart" />
    </span>
    <span class="btn-text">{{ $t('Mistakes.RepetitionButton') }}: {{ repetitionCounter }} </span>
  </button>
</template>

<script lang="ts">
import { MistakesActions } from '@/store/mistakes-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'RepetitionButton',
  props: {
    mistakeId: {
      required: true,
      type: String,
    },
    repetitionCounter: {
      required: true,
      type: Number,
    },
  },
  methods: {
    async addRepetition(): Promise<void> {
      await this.$store.dispatch(
        MistakesActions.MistakeRepetition,
        { mistakeId: this.mistakeId },
      );
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-repetition {
  @include mistakes-journal.font-semi-bold(0.75em);
  margin-right: 1em;
  white-space: nowrap;
}
</style>
