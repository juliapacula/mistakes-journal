<template>
  <router-view />
</template>

<script lang="ts">
import { State } from '@/store/state';
import { UserActions } from '@/store/user-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'App',
  async beforeCreate() {
    await this.$store.dispatch(UserActions.GetConfiguration);
    await this.$store.dispatch(UserActions.Get);
  },
  created() {
    this.$store.watch(
      (s: State) => s.uiState.errorMessageKeys,
      (messageKeys: string[]) => this.$toasted.error(this.$t(messageKeys[0]) as string),
    );
  },
});
</script>

<style lang="scss">
@use 'styles';
</style>
