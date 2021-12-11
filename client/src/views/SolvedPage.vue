<template>
  <div class="container-fluid h-100">
    <div class="row position-relative h-100">
      <div
        class="solved-panel col-12 mb-2">
        <section-header name="solved">
          {{ $t('Solved.Title') }}
        </section-header>
        <router-view class="flex-grow-1" />
        <solved-empty-list />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import SectionHeader from '@/components/shared/SectionHeader.vue';
import SolvedEmptyList from '@/components/solved/SolvedEmptyList.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'SolvedPage',
  components: {
    SolvedEmptyList,
    SectionHeader,
  },
});
</script>

<style
  scoped
  lang="scss">
@use '../styles/mistakes-journal';

.solved-panel {
  display: flex;
  flex-direction: column;
}

</style>
