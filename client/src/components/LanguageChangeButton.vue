<template>
  <div class="btn-group">
    <button
      v-for="locale in availableLocales"
      :key="locale.value"
      :disabled="locale.value === $i18n.locale"
      class="btn btn-primary"
      @click="changeLocale(locale.value)">
      {{ locale.icon }}
    </button>
  </div>
</template>

<script lang="ts">
import {
  Locale,
  LOCALES,
} from '@/i18n/locales';
import Vue from 'vue';

export default Vue.extend({
  name: 'LanguageChangeButton',
  data(): { availableLocales: { caption: string, value: Locale, icon: string }[] } {
    return {
      availableLocales: LOCALES,
    };
  },
  methods: {
    changeLocale(value: Locale): void {
      this.$i18n.locale = value;
    },
  },
});
</script>

<style
  lang="scss"
  scoped></style>
