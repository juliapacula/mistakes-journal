<template>
  <b-dropdown
    dropleft
    no-caret
    variant="link">
    <template #button-content>
      <fa-icon
        :icon="['fas', 'ellipsis-v']" />
    </template>
    <b-dropdown-item-button @click="editMistake">
      <span class="mj-options">
        <fa-icon
          :icon="['fas', 'pen']" />
        <span class="mj-options-text">
          {{ $t('MistakeOptions.Edit') }}
        </span>
      </span>
    </b-dropdown-item-button>
    <b-dropdown-item-button @click="deleteMistake">
      <span class="mj-options">
        <fa-icon
          :icon="['far', 'trash-alt']" />
        <span class="mj-options-text">
          {{ $t('MistakeOptions.Delete') }}
        </span>
      </span>
    </b-dropdown-item-button>
  </b-dropdown>
</template>

<script lang="ts">
import { MistakesActions } from '@/store/mistakes-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakeOptionsMenu',
  props: {
    mistakeId: {
      required: true,
      type: String,
    },
  },
  methods: {
    async deleteMistake(): Promise<void> {
      await this.$store.dispatch(MistakesActions.Delete, this.mistakeId);
      if (this.$route.name === 'MistakeSolutions') {
        await this.$router.push('/journal/mistakes');
      }
    },
    editMistake(): void {
      this.$router.push(`/journal/mistakes/edit/${this.mistakeId}`);
    },
  },
});
</script>

<style
  lang="scss"
  scoped>

.mj-options {
  align-items: center;
  padding: 0.25rem;

  &-text {
    padding-left: 1rem;
  }
}
</style>
