<template>
  <span :class="'ri-' + icon + '-' + (fill ? 'fill' : 'line')" />
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'RemixIcon',
  props: {
    icon: String,
    fill: {
      type: Boolean,
      default: false,
    },
  },
});
</script>

<style
  lang="scss"
  scoped>

</style>
