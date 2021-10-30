<template>
  <div class="mj-pagination">
    <button
      v-if="canShowMore"
      class="btn btn-primary"
      @click="showMoreMistakes()">
      {{ $t('Mistakes.ShowMore') }}
    </button>
  </div>
</template>

<script lang="ts">
import { PAGINATION_MAX_RESULTS_STEP } from '@/config/pagination.config';
import { MistakesActions } from '@/store/mistakes-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakesPagination',
  computed: {
    maxResults(): boolean {
      return this.$store.state.mistakes.mistakesFilters.pagination.maxResults;
    },
    canShowMore(): boolean {
      return this.maxResults < this.$store.state.mistakes.mistakesTotalCount;
    },
  },
  methods: {
    async showMoreMistakes(): void {
      await this.$store.dispatch(
        MistakesActions.UpdateMistakesFilters,
        { pagination: { startAt: 0, maxResults: this.maxResults + PAGINATION_MAX_RESULTS_STEP } },
      );
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
.mj-pagination {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100%;
  margin: 1rem 0;
}
</style>
