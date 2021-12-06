<template>
  <div>
    <router-link
      v-if="isLoggedIn"
      :to="{ name: 'MistakesPage' }"
      class="btn btn-outline-primary mj-login-button"
      role="button"
      tag="a">
      {{ $t('LandingPage.Nav.Hello') }}
    </router-link>
    <button
      v-else
      class="btn btn-outline-primary mj-login-button"
      @click="login()">
      {{ $t('LandingPage.Nav.Login') }}
    </button>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'LoginButton',
  computed: {
    isLoggedIn(): boolean {
      return !!this.$store.state.user.user;
    },
    loginPath(): string {
      return this.$store.state.user.configuration.loginPath.substr(1);
    },
  },
  methods: {
    login(): void {
      if (process.env.NODE_ENV === 'development') {
        window.location.href = `http://localhost:5001/${this.loginPath}?returnUrl=${window.location.pathname}journal`;
      } else {
        window.location.href = `${this.loginPath}?returnUrl=${window.location.pathname}journal`;
      }
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
</style>
