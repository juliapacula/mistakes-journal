<template>
  <div
    v-if="canBootstrap"
    :style="{'font-size': fontSize, 'filter': filter, 'transition': 'font-size 100ms, filter 200ms'}">
    <router-view />
  </div>
</template>

<script lang="ts">
import { REFRESH_TIME } from '@/config/weather.config';
import { Locale } from '@/i18n/locales';
import { User } from '@/model/user';
import { State } from '@/store/state';
import { UiStateActions } from '@/store/ui-state-module/actions';
import { UiStateMutations } from '@/store/ui-state-module/mutations';
import { UserActions } from '@/store/user-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'App',
  computed: {
    fontSize(): string {
      return `${this.$store.state.uiState.fontSize}rem`;
    },
    filter(): string {
      return `saturate(${this.$store.state.uiState.saturation})`;
    },
    canBootstrap(): boolean {
      return this.$store.state.user.hasLoadedUser;
    },
  },
  async beforeCreate() {
    await this.$store.dispatch(UserActions.GetConfiguration);
    await this.$store.dispatch(UserActions.Get);
    await this.$store.dispatch(UserActions.WatchLocation);

    setInterval(() => this.$store.dispatch(UserActions.UpdateWeatherInfo), REFRESH_TIME);
  },
  created() {
    this.watchStateForErrors();
    this.watchStateForLanguageChange();
    this.watchStateForLocationChange();
    window.addEventListener('resize', this.hideSidebar);
  },
  destroyed() {
    window.removeEventListener('resize', this.hideSidebar);
  },
  methods: {
    watchStateForErrors(): void {
      this.$store.watch(
        (s: State) => s.uiState.errorMessageKeys,
        (messageKeys: string[]) => this.$toasted.error(this.$t(messageKeys[0]) as string),
      );
      this.$store.watch(
        (s: State) => s.user.canAccessLocation,
        (canAccessLocation: boolean | null) => {
          if (canAccessLocation === false) {
            this.$toasted.error(this.$t('LocationAccess.Denied') as string);
          }
        },
      );
    },
    watchStateForLanguageChange(): void {
      this.$store.watch(
        (s: State) => s.user.user,
        (user: User | null) => {
          if (user) {
            this.$store.commit(UiStateMutations.SetLanguage, user.language);
          }
        },
      );
      this.$store.watch(
        (s: State) => s.uiState.language,
        (language: Locale) => {
          this.$i18n.locale = language ?? Locale.EN;
        },
      );
    },
    watchStateForLocationChange(): void {
      this.$store.watch(
        (s: State) => s.user.location,
        (location: { latitude: number, longitude: number } | null) => {
          if (!location) {
            return;
          }

          this.$store.dispatch(UserActions.UpdateWeatherInfo);
        },
      );
    },
    hideSidebar(): void {
      if (window.innerWidth < 768 && this.$store.state.uiState.isSidebarVisible) {
        this.$store.dispatch(UiStateActions.ToggleSidebar);
      }
    },
  },
});
</script>

<style lang="scss">
@use 'styles';
</style>
