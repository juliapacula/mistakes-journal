<template>
  <button
    v-if="!isLoggedIn"
    class="btn btn-primary"
    type="button"
    @click="register()">
    <slot />
  </button>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'RegisterButton',
  computed: {
    isLoggedIn(): boolean {
      return !!this.$store.state.user.user;
    },
    registerPath(): string {
      return this.$store.state.user.configuration.registerPath.substr(1);
    },
  },
  methods: {
    register(): void {
      if (process.env.NODE_ENV === 'development') {
        window.location.href = `http://localhost:5001/${this.registerPath}?returnUrl=${window.location.pathname}journal`;
      } else {
        window.location.href = `${this.registerPath}?returnUrl=${window.location.pathname}journal`;
      }
    },
  },
});
</script>

<style
  lang="scss"
  scoped>

</style>
