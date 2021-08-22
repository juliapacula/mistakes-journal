import {
  DEFAULT_LOCALE,
  Locale,
  MESSAGES,
} from '@/i18n/locales';
import Vue from 'vue';
import VueI18n from 'vue-i18n';

Vue.use(VueI18n);

export default new VueI18n({
  messages: MESSAGES,
  locale: DEFAULT_LOCALE,
  fallbackLocale: DEFAULT_LOCALE,
  pluralizationRules: {
    /**
     To use plural forms in polish translations, you can use translation as:
     "zero | 1 | regular number | special numbers (like 2, 3, 4)" e.g.:
     "0 kobiet | {number} kobieta | {number} kobiet | {number} kobiety"
     For English, there are some default rules:
     https://kazupon.github.io/vue-i18n/guide/pluralization.html#accessing-the-number-via-the-pre-defined-argument
     */
    [Locale.PL](choice: number): number {
      if (choice === 0) {
        return 0;
      }

      if (choice === 1) {
        return 1;
      }

      if (choice >= 12 && choice <= 14) {
        return 2;
      }

      const endNumber = choice % 10;

      if (endNumber >= 2 && endNumber <= 4) {
        return 3;
      }

      return 2;
    },
  },
});
