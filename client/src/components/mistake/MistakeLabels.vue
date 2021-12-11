<template>
  <div class="col-12">
    <label class="form-label">{{ $t('MistakeForm.Labels') }}</label>
    <div class="mj-mistake-labels">
      <div
        v-for="label in availableLabels"
        :key="label.id"
        class="mj-mistake-label">
        <input
          :id="label.id"
          v-model="labelIds"
          :value="label.id"
          autocomplete="off"
          class="btn-check"
          type="checkbox">
        <label
          :for="label.id"
          class="btn btn-label">
          <label-icon
            :color="label.color"
            class="icon" />
          <span>{{ label.name }}</span>
        </label>
      </div>
      <div
        v-if="availableLabels.length === 0"
        class="mj-no-labels">
        {{ $t('MistakeForm.NoLabels') }}
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Label } from '@/model/label';
import { LabelsActions } from '@/store/labels-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakeLabels',
  props: {
    value: {
      type: Array,
    },
  },
  data(): { labelIds: string[] } {
    return {
      labelIds: [],
    };
  },
  computed: {
    availableLabels(): Label[] {
      return this.$store.state.labels.labels;
    },
  },
  watch: {
    labelIds(selectedLabels: string[]): void {
      this.$emit('input', selectedLabels);
    },
    value(labelIds: string[]): void {
      this.labelIds = labelIds;
    },
  },
  async beforeCreate(): Promise<void> {
    await this.$store.dispatch(LabelsActions.GetAll);
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

label {
  @include mistakes-journal.font-regular(1rem);
}

.btn-label {
  display: inline-flex;
  align-items: center;
  padding: 0.1rem 0.3rem;
  border: 2px solid mistakes-journal.color('gray', '200');
  border-radius: 0.2rem;
  font-size: 0.8em;

  .icon {
    margin-right: 0.2rem;
  }

  &:hover {
    border: 2px solid mistakes-journal.color('primary', '700');
    background-color: mistakes-journal.color('primary', '500');
  }
}

.btn-check:checked + .btn-label {
  border: 2px solid mistakes-journal.color('primary', '500');
  background-color: mistakes-journal.color('primary', '300');

  &:hover {
    border: 2px solid mistakes-journal.color('primary', '500');
    background-color: mistakes-journal.color('primary', '700');
  }
}

.mj-mistake-labels {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
}

.mj-no-labels {
  @include mistakes-journal.font-regular(0.8rem);
  width: 100%;
  padding: 0.5rem 0.2rem;
  border-radius: 0.2rem;
  background-color: mistakes-journal.color('gray', '100');
  text-align: center;
}

.mj-mistake-label {
  margin: 0.1rem 0.2rem;
}
</style>
