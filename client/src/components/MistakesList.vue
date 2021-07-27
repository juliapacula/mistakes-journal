<template>
  <div class="row">
    <div class="col-12">
      <add-new-mistake-button />
    </div>
  </div>
</template>

<script lang="ts">
import AddNewMistakeButton from '@/components/AddNewMistakeButton.vue';
import { MistakesActions } from '@/store/mistakes-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakesList',
  components: { AddNewMistakeButton },
  async beforeCreate(): Promise<void> {
    await this.$store.dispatch(MistakesActions.GetAll);
  },
});
</script>

<style
  lang="scss"
  scoped>

</style>
