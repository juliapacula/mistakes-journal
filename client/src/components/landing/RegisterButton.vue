<template>
  <div>
    <button
      v-if="buttonNumber === 2"
      class="btn btn-primary"
      @click="register()">
      {{ $t('LandingPage.Nav.EndingPageButton') }}
    </button>
    <button
      v-else
      class="btn btn-primary"
      @click="register()">
      {{ $t('LandingPage.Nav.GetStarted') }}
    </button>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'RegisterButton',
  props: {
    buttonNumber: {
      required: false,
      type: Number,
    },
  },
  methods: {
    register(): void {
      // TODO: Add registration path retrival
    },
  },
});
</script>

<style
  lang="scss"
  scoped>

</style>
