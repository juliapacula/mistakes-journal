<template>
  <div class="mj-application-title">
    <img
      v-if="withIcon"
      alt="Application Logo"
      class="mj-application-title-logo"
      src="@/assets/icons/ice_cream_broken.svg">
    <span
      :class="{'d-none d-sm-inline': inNavBar}"
      class="mj-application-title-first">
      {{ $t('Application.Title.First') }}
    </span>
    <span
      :class="{'d-none d-sm-inline': inNavBar}"
      class="mj-application-title-second">
      {{ $t('Application.Title.Second') }}
    </span>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'ApplicationTitle',
  props: {
    withIcon: {
      type: Boolean,
      default: false,
    },
    inNavBar: {
      type: Boolean,
      default: false,
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-application-title {
  display: inline-flex;
  align-items: center;
  justify-content: center;

  span {
    @include mistakes-journal.font-semi-bold(1em);
  }

  &-logo {
    height: 3em;
    margin-right: 0.25em;
  }

  &-first {
    margin-right: 0.25em;
    color: mistakes-journal.color('primary', '900');
  }

  &-second {
    color: mistakes-journal.color('secondary', '900');
  }
}
</style>
