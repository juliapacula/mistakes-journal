<template>
  <div class="mj-labels-list">
    <div
      v-for="label in labels"
      :key="label.id"
      class="mj-label">
      <label-icon
        :color="label.color"
        class="mj-label-icon" />
      <span>{{ label.name }}</span>
    </div>
  </div>
</template>

<script lang="ts">
import { Label } from '@/model/label';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakeLabelsList',
  props: {
    labels: {
      type: Array as () => Label[],
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
.mj-labels-list {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  padding: 0.5rem 1rem;
}

.mj-label {
  display: flex;
  align-items: center;
  margin: 0.1em 0.2em;
  font-size: 0.9em;
}

.mj-label-icon {
  margin-right: 0.2em;
}
</style>
