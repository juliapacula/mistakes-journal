<template>
  <div class="row">
    <div class="col-12">
      <add-new-mistake-button />
    </div>
    <div class="col-12 mt-4 mj-mistake-items">
      <div
        v-for="mistake in $store.state.mistakes.mistakes"
        :key="mistake.id">
        <div class="mj-mistake-item-title">
          <router-link
            :to="'/journal/mistakes/' + mistake.id"
            class="mj-mistake-item-link">
            {{ mistake.name }}
          </router-link>
        </div>
        <mistake-priority :priority="mistake.priority" />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import AddNewMistakeButton from '@/components/AddNewMistakeButton.vue';
import MistakePriority from '@/components/MistakePriority.vue';
import { MistakesActions } from '@/store/mistakes-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakesList',
  components: {
    AddNewMistakeButton,
    MistakePriority,
  },
  async beforeCreate(): Promise<void> {
    await this.$store.dispatch(MistakesActions.GetAll);
  },
});
</script>

<style
  lang="scss"
  scoped>
@use 'sass:color';
@use '../styles/mistakes-journal';

.mj-mistake-items {
  width: 100%;
}

.mj-mistake-item {
  &-link {
    @include mistakes-journal.font-medium;
    display: block;
    width: 100%;
    color: mistakes-journal.color('text');
  }

  &-title {
    padding: 0.25rem 1rem;
    border-radius: 0.125rem;

    &:hover {
      background-color: mistakes-journal.color('secondary', '50');
    }
  }
}

a {
  text-decoration: none;

  &:hover,
  &:focus,
  &:active {
    text-decoration: none;
  }
}

</style>
