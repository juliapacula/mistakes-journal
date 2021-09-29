<template>
  <div class="btn-group">
    <button
      v-for="locale in availableLocales"
      :key="locale.value"
      :class="{ 'btn-primary': !isTransparent }"
      :disabled="locale.value === $i18n.locale"
      class="btn"
      @click="changeLocale(locale.value)">
      <mj-icon :name="locale.value" />
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
    changeLocale(value: Locale): void {
      this.$i18n.locale = value;
    },
  },
});
</script>

<style
  lang="scss"
  scoped></style>
