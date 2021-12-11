<template>
  <div
    class="mj-onboard-progress"
    :class="{'mj-onboard-mobile' : isMobile}">
    <div
      v-if="currentStep === 1">
      <fa-icon
        :icon="['fas', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
    </div>
    <div
      v-else-if="currentStep === 2">
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['fas', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
    </div>
    <div
      v-else-if="currentStep === 3">
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['fas', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
    </div>
    <div
      v-else-if="currentStep === 4">
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['far', 'circle']"
        transform="shrink-6" />
      <fa-icon
        :icon="['fas', 'circle']"
        transform="shrink-6" />
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'OnboardProgress',
  props: {
    currentStep: {
      required: true,
      type: Number,
    },
    isMobile: {
      required: true,
      type: Boolean,
    },
  },
});
</script>

<style
  scoped
  lang="scss">
@use 'sass:color';
@use '../../styles/mistakes-journal';

.mj-onboard-progress {
  margin: auto;
  color: mistakes-journal.color('gray', '400');
  text-align: center;
}

.mj-onboard-mobile {
  display: flex;
  position: fixed;
  bottom: 0;
  left: 50%;
  align-items: center;
  justify-content: center;
  margin-bottom: 1rem;
  transform: translateX(-50%);
}
</style>
