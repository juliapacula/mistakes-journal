<template>
  <div class="mj-applied-filters">
    <button
      v-if="selectedLabel"
      v-tooltip="$t('Mistakes.RemoveLabelFilter')"
      class="mj-label-filter btn"
      type="button"
      @click="removeLabelFilter()">
      <label-icon :color="selectedLabel.color" />
      <span class="mj-label-filter-name">{{ selectedLabel.name }}</span>
    </button>
  </div>
</template>

<script lang="ts">
import { Label } from '@/model/label';
import { MistakesActions } from '@/store/mistakes-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakesListAppliedFilters',
  computed: {
    selectedLabel(): Label | null {
      const filterLabelId = this.$store.state.mistakes.mistakesFilters.labelId;

      if (!filterLabelId) {
        return null;
      }

      return this.$store.state.labels.labels.find((l: Label) => l.id === filterLabelId) ?? null;
    },
  },
  methods: {
    async removeLabelFilter(): Promise<void> {
      await this.$store.dispatch(MistakesActions.UpdateMistakesFilters, { labelId: null });
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-applied-filters {
  margin-bottom: 0.5rem;
}

.mj-label-filter {
  display: flex;
  align-items: center;
  padding: 0.1rem 0.3rem;
  border: 2px solid mistakes-journal.color('secondary', '500');
  background-color: mistakes-journal.color('secondary', '300');
  border-radius: 0.2em;
  font-size: 0.8rem;

  &-name {
    margin-left: 0.2rem;
  }

  &:hover {
    border: 2px solid mistakes-journal.color('secondary', '300');
    background-color: mistakes-journal.color('secondary', '50');
  }
}
</style>
