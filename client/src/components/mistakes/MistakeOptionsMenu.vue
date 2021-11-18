<template>
  <b-dropdown
    v-tooltip="$t('MistakeOptions.Label')"
    dropleft
    no-caret
    variant="link">
    <template #button-content>
      <remix-icon
        class="mj-options-icon"
        icon="more-2" />
    </template>
    <b-dropdown-item :to="'/journal/mistakes/edit/' + mistakeId">
      <span class="mj-options">
        <fa-icon
          :icon="['fas', 'pen']" />
        <span class="mj-options-text">
          {{ $t('MistakeOptions.Edit') }}
        </span>
      </span>
    </b-dropdown-item>
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

  &-icon {
    font-size: 1.5em;
  }
}
</style>
