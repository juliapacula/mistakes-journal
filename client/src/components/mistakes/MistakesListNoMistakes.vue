<template>
  <div class="mj-empty-list">
    <img
      alt="Ice cream Logo"
      class="mj-empty-list-icon"
      src="@/assets/icons/logo_mistakes_list.svg">
    <div v-if="!areAnyFiltersApplied">
      <div class="mj-empty-list-text">
        {{ $t('Mistakes.EmptyList') }}
      </div>
      <div class="mj-empty-list-text">
        {{ $t('Mistakes.EmptyList2') }}
      </div>
    </div>
    <div v-else>
      <div class="mj-empty-list-text">
        {{ $t('Mistakes.EmptyListForFilters') }}
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakesListNoMistakes',
  computed: {
    areAnyFiltersApplied(): boolean {
      return this.$store.getters.areAnyFiltersApplied;
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-empty-list {
  display: flex;
  flex: 1;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 100%;
  padding-top: 1rem;
  padding-bottom: 1rem;
  border-radius: 1rem;
  background-color: mistakes-journal.color('gray', '100');

  &-icon {
    padding-bottom: 2rem;

    @include mistakes-journal.media-breakpoint-up(lg) {
      width: 15rem;
      height: 18rem;
    }
  }

  &-text {
    @include mistakes-journal.font-regular(1.2rem);
  }
}
</style>
