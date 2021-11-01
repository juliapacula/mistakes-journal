<template>
  <span class="mj-label-icon">
    <remix-icon
      :style="{ color: color }"
      :fill="true"
      class="mj-label-icon-content"
      icon="price-tag-3" />
    <remix-icon
      class="mj-label-icon-border"
      icon="price-tag-3" />
  </span>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'LabelIcon',
  props: {
    color: {
      type: String,
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-label-icon {
  position: relative;

  &-content {
    z-index: 1;
  }

  &-border {
    position: absolute;
    z-index: 2;
    top: 0;
    left: 0;
    color: mistakes-journal.color('gray', '900');
  }
}
</style>
