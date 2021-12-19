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
import { GOOGLE_EVENTS } from '@/config/google-analytics-events.config';
import Vue from 'vue';
import { event } from 'vue-gtag';

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
      event(GOOGLE_EVENTS.REGISTER);

      if (process.env.NODE_ENV === 'development') {
        window.location.href = `http://localhost:5001/${this.registerPath}?returnUrl=${window.location.pathname}journal&culture=${this.$i18n.locale.toLowerCase()}`;
      } else {
        window.location.href = `${this.registerPath}?returnUrl=${window.location.pathname}journal&culture=${this.$i18n.locale.toLowerCase()}`;
      }
    },
  },
});
</script>

<style
  lang="scss"
  scoped>

</style>
