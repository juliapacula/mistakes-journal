<template>
  <div class="">
    <router-link :to="{ name: 'MistakesPage' }">
      {{ $t('LandingPage.Nav.GetStarted') }}
    </router-link>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'LandingPage',
});
</script>

<style
  lang="scss"
  scoped>

</style>
