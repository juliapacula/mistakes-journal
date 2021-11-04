<template>
  <div>
    <button
      class="btn btn-primary"
      @click="register()">
      <slot></slot>
    </button>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'RegisterButton',
  methods: {
    register(): void {
      // TODO: Add registration path retrival
    },
  },
});
</script>

<style
  lang="scss"
  scoped>

</style>
