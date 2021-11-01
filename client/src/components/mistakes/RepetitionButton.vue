<template>
  <button
    class="btn mj-repetition with-icon"
    type="button"
    @click="addRepetition()">
    <span class="btn-icon">
      <fa-icon
        :icon="['fas', 'redo']" />
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
  margin-right: 1em;
  border-color: mistakes-journal.color('repetition');
  color: mistakes-journal.color('repetition');
  font-size: 0.75em;
  white-space: nowrap;

  &:hover {
    border-color: mistakes-journal.color('primary');
    background-color: mistakes-journal.color('gray', '0');
    color: mistakes-journal.color('primary');
  }
}
</style>
