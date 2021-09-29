<template>
  <transition name="collapse">
    <div
      v-if="isVisible"
      class="invalid-feedback">
      <slot />
    </div>
  </transition>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'MJError',
  props: {
    isVisible: {
      type: Boolean,
    },
  },
});
</script>

<style
  lang="scss"
  scoped>

.collapse {
  &-enter-active,
  &-leave-active {
    display: block;
    overflow-y: hidden;
    transition: height 250ms, opacity 200ms;
  }

  &-enter,
  &-leave-to {
    height: 0;
    opacity: 0;
  }

  &-enter-to,
  &-leave {
    height: calc(1em + 0.25rem);
  }
}
</style>
