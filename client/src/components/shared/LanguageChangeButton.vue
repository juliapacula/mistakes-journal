<template>
  <div class="btn-group">
    <button
      v-for="locale in availableLocales"
      :key="locale.value"
      :class="{ 'btn-primary': !isTransparent }"
      :disabled="locale.value === $i18n.locale"
      class="btn"
      @click="changeLocale(locale.value)">
      <mj-icon :name="locale.value.toLowerCase()" />
    </button>
  </div>
</template>

<script lang="ts">
import {
  Locale,
  LOCALES,
} from '@/i18n/locales';
import { UiStateMutations } from '@/store/ui-state-module/mutations';
import { UserActions } from '@/store/user-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'LanguageChangeButton',
  props: {
    isTransparent: {
      type: Boolean,
      default: false,
    },
  },
  data(): { availableLocales: { caption: string, value: Locale }[] } {
    return {
      availableLocales: LOCALES,
    };
  },
  methods: {
    async changeLocale(value: Locale): Promise<void> {
      if (this.$store.state.user.user) {
        await this.$store.dispatch(UserActions.ChangeLanguage, value);
      } else {
        this.$store.commit(UiStateMutations.SetLanguage, value);
      }
    },
  },
});
</script>

<style
  lang="scss"
  scoped></style>
