<template>
  <div class="mj-empty-list">
    <img
      alt="Ice cream Logo"
      class="mj-empty-list-icon"
      src="@/assets/icons/solved_empty_list.svg">
    <div class="mj-empty-list-text">
      <span>
        {{ $t('Solved.EmptyList') }}
      </span>
      <span>
        {{ $t('Solved.EmptyList2') }}
      </span>
      <span>
        {{ $t('Solved.EmptyList3') }}
      </span>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'SolvedEmptyList',
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
    display: inline-block;
    text-align: center;

    @include mistakes-journal.media-breakpoint-up(sm) {
      display: flex;
      flex-direction: column;
    }
  }
}
</style>
