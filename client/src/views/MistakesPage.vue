<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <section-header name="mistake">
          {{ $t('Mistakes.Title') }}
        </section-header>
      </div>
    </div>
    <router-view />
  </div>
</template>

<script lang="ts">
import SectionHeader from '@/components/SectionHeader.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakesPage',
  components: {
    SectionHeader,
  },
});
</script>

<style
  lang="scss"
  scoped>
.mj-add-mistake {
  .mj-icon {
    width: 1.5rem;
  }
}
</style>
