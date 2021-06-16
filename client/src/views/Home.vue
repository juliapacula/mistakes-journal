<template>
  <div class="home">
    <img
        alt="Vue logo"
        src="../assets/logo.png">
    <HelloWorld msg="Hello in Mistakes Journal!!!" />
  </div>
</template>

<script lang="ts">
import HelloWorld from '@/components/HelloWorld.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'Home',
  components: {
    HelloWorld,
  },
  async beforeCreate() {
    const response = await fetch('/api/weather');
    const body = await response.json();
  },
});
</script>
