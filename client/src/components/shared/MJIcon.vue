<template>
  <img
    v-bind="$attrs"
    :src="iconPath"
    alt=""
    class="mj-icon"
    @v-on="$listeners">
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'MJIcon',
  inheritAttrs: false,
  props: {
    name: {
      type: String,
      required: true,
    },
  },
  data(): { iconPath: string } {
    const images = require.context('../../assets/icons', false, /\.svg$/);

    return {
      iconPath: images(`./${this.name}.svg`),
    };
  },
});
</script>

<style
  lang="scss"
  scoped>
img {
  $size: 1em;
  width: $size;
  color: currentColor;
}
</style>
