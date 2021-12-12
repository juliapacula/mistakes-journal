<template>
  <div>
    <landing-navigation-bar />
    <about-mistakes-journal />
    <landing-footer />
  </div>
</template>

<script lang="ts">
import AboutMistakesJournal from '@/components/landing/AboutMistakesJournal.vue';
import LandingFooter from '@/components/landing/LandingFooter.vue';
import LandingNavigationBar from '@/components/landing/LandingNavigationBar.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'LandingPage',
  components: { LandingFooter, AboutMistakesJournal, LandingNavigationBar },
});
</script>

<style
  lang="scss"
  scoped>

</style>
