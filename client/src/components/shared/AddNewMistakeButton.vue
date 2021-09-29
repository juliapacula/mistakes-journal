<template>
  <router-link
    to="/journal/mistakes/new"
    tag="a"
    role="button"
    class="btn btn-primary mj-add-mistake with-icon">
    <mj-icon
      class="btn-icon"
      name="add_circle" />
    <span class="btn-text">{{ $t('Mistakes.AddButton') }}</span>
  </router-link>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'AddNewMistakeButton',
});
</script>

<style
  lang="scss"
  scoped>
.mj-add-mistake {
  white-space: nowrap;
}
</style>
