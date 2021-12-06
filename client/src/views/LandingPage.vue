<template>
  <div>
    <landing-navigation-bar />
    <about-mistakes-journal />
  </div>
</template>

<script lang="ts">
import AboutMistakesJournal from '@/components/landing/AboutMistakesJournal.vue';
import LandingNavigationBar from '@/components/landing/LandingNavigationBar.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'LandingPage',
  components: { AboutMistakesJournal, LandingNavigationBar },
});
</script>

<style
  lang="scss"
  scoped>

</style>
