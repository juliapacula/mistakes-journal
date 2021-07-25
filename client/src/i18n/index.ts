import { DEFAULT_LOCALE, MESSAGES } from '@/i18n/locales';
import Vue from 'vue';
import VueI18n from 'vue-i18n';

Vue.use(VueI18n);

export default new VueI18n({
  messages: MESSAGES,
  locale: DEFAULT_LOCALE,
  fallbackLocale: DEFAULT_LOCALE,
});
