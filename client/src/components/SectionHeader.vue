<template>
  <div class="mj-section-header">
    <mj-icon
      :name="name"
      class="mj-section-header-icon" />
    <h1>
      <slot />
    </h1>
    <div class="mj-section-header-actions">
      <slot name="actions" />
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'SectionHeader',
  props: {
    name: {
      type: String,
      required: true,
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../styles/mistakes-journal';

.mj-section-header {
  --header-size: 1.5rem;
  display: flex;
  position: relative;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 1rem;
  padding: 0.5rem 0;

  @include mistakes-journal.media-breakpoint-up(sm) {
    --header-size: 1.75rem;
  }

  &::before {
    content: '';
    display: block;
    position: absolute;
    bottom: 0;
    width: 100%;
    height: 3px;
    background-color: mistakes-journal.color('secondary', '900');
  }

  &-icon {
    width: var(--header-size);
    margin-right: 0.5rem;
  }

  h1 {
    @include mistakes-journal.font-semi-bold(var(--header-size));
    flex: 1;
    margin-bottom: 0;
  }
}
</style>
