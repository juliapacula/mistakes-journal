<template>
  <router-view />
</template>

<script lang="ts">
import { UserActions } from '@/store/user-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'App',
  async beforeCreate() {
    await this.$store.dispatch(UserActions.GetConfiguration);
    await this.$store.dispatch(UserActions.Get);
  },
});
</script>

<style lang="scss">
@use 'styles';
</style>
