<template>
  <div class="mj-progress">
    <b-progress
      :max="max1"
      :value="firstProgressValue"
      class="w-25"
      height="2px" />
    <span class="mj-progress-icon">
      <fa-icon
        :class="{'mj-color-icon-active': isProgress1}"
        :icon="['far', 'smile']" />
    </span>
    <b-progress
      :max="max2"
      :value="secondProgressValue"
      class="w-50"
      height="2px" />
    <span class="mj-progress-icon">
      <fa-icon
        :class="{'mj-color-icon-active': isProgress2}"
        :icon="['far', 'grin']" />
    </span>
    <b-progress
      :max="max3"
      :value="thirdProgressValue"
      class="w-100"
      height="2px" />
    <span class="mj-progress-icon">
      <fa-icon
        :class="{'mj-color-icon-active': isProgress3}"
        :icon="['far', 'laugh']" />
    </span>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'ProgressBar',
  props: {
    pastDays: {
      required: true,
      type: Number,
    },
  },
  data() {
    return {
      max1: 7,
      max2: 14,
      max3: 45,
    };
  },
  computed: {
    isProgress1(): boolean {
      return this.pastDays >= this.max1;
    },
    isProgress2(): boolean {
      return this.pastDays >= (this.max1 + this.max2);
    },
    isProgress3(): boolean {
      return this.pastDays >= (this.max1 + this.max2 + this.max3);
    },
    firstProgressValue(): number {
      if (this.pastDays <= 7) {
        return this.pastDays;
      }
      return this.max1;
    },
    secondProgressValue(): number {
      if (this.pastDays <= 7) {
        return 0;
      }
      if (this.pastDays > 21) {
        return this.max2;
      }
      return (this.pastDays - this.max1);
    },
    thirdProgressValue(): number {
      if (this.pastDays <= 21) {
        return 0;
      }
      if (this.pastDays > 66) {
        return this.max3;
      }
      return (this.pastDays - (this.max2 + this.max1));
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-progress {
  display: flex;
  position: relative;
  align-items: center;
  justify-content: space-between;
  padding: 0.25rem 1rem;

  &-icon {
    padding: 0.5em;
  }
}

.mj-color-icon-active {
  color: mistakes-journal.color('success');
}
</style>
