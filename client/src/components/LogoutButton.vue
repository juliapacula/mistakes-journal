<template>
  <div class="btn-group">
    <button
      class="btn btn-primary"
      @click="logout()">
      <span class="btn-icon">
        <fa-icon
          :icon="['fas', 'sign-out-alt']" />
      </span>
    </button>
  </div>
</template>

<script lang="ts">
import { UserActions } from '@/store/user-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'LogoutButton',
  methods: {
    logout(): void {
      this.$store.dispatch(UserActions.Logout);
      this.$router.push('/');
    },
  },
});
</script>

<style
  lang="scss"
  scoped></style>
