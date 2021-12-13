<template>
  <div>
    <div class="mj-progress">
      <b-progress
        :max="max1"
        :value="firstProgressValue > 0 ? firstProgressValue : 0.5"
        class="w-25 mj-progress-bar"
        height="3px"
        precision="1" />
      <span class="mj-progress-icon">
        <remix-icon
          :class="{'mj-color-active': isProgress1}"
          icon="emotion-happy" />
      </span>
      <b-progress
        :max="max2"
        :value="secondProgressValue"
        class="w-50 mj-progress-bar"
        height="3px" />
      <span class="mj-progress-icon">
        <remix-icon
          :class="{'mj-color-active': isProgress2}"
          icon="emotion" />
      </span>
      <b-progress
        :max="max3"
        :value="thirdProgressValue"
        class="w-100 mj-progress-bar"
        height="3px" />
      <span class="mj-progress-icon">
        <remix-icon
          :class="{'mj-color-active': isProgress3}"
          icon="emotion-laugh" />
      </span>
    </div>
    <div class="mj-progress mj-days">
      <div class="w-25" />
      <span :class="{'mj-color-active': isProgress1}">7</span>
      <div class="w-50" />
      <span :class="{'mj-color-active': isProgress2}">21</span>
      <div class="w-100" />
    </div>
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
  padding: 0.25rem 1rem 0;
  color: mistakes-journal.color('gray', '300');

  &-bar {
    background-color: mistakes-journal.color('gray', '300');
  }

  &-icon {
    padding: 0.5em;
    font-size: 1rem;
  }
}

.mj-color-active {
  color: mistakes-journal.color('success');
}

.mj-days {
  padding-top: 0;
}
</style>
