<template>
  <div class="mj-mistake-priority">
    <div
      v-if="priority === mistakePriority.Low"
      class="mj-mistake-priority-low">
      <fa-layers>
        <fa-icon
          :icon="['fas', 'exclamation']"
          transform="shrink-6" />
        <fa-icon
          :icon="['far', 'circle']"
          transform="grow-5" />
      </fa-layers>
    </div>
    <div
      v-else-if="priority === mistakePriority.Medium"
      class="mj-mistake-priority-medium">
      <fa-layers>
        <fa-icon
          :icon="['fas', 'exclamation']"
          transform="shrink-6 right-2" />
        <fa-icon
          :icon="['fas', 'exclamation']"
          transform="shrink-6 left-2" />
        <fa-icon
          :icon="['far', 'circle']"
          transform="grow-5" />
      </fa-layers>
    </div>
    <div
      v-else-if="priority === mistakePriority.High"
      class="mj-mistake-priority-high">
      <fa-layers>
        <fa-icon
          :icon="['fas', 'exclamation']"
          transform="shrink-6 right-4" />
        <fa-icon
          :icon="['fas', 'exclamation']"
          transform="shrink-6" />
        <fa-icon
          :icon="['fas', 'exclamation']"
          transform="shrink-6 left-4" />
        <fa-icon
          :icon="['far', 'circle']"
          transform="grow-5" />
      </fa-layers>
    </div>

    <span class="mj-mistake-priority-text">{{ $t('Priority.' + priority) }}</span>
  </div>
</template>

<script lang="ts">
import { MistakePriority } from '@/model/mistake-priority.enum';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakePriority',
  props: {
    priority: {
      required: true,
      type: String,
    },
  },
  data() {
    return { mistakePriority: MistakePriority };
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-mistake-priority {
  display: flex;
  align-items: center;
  padding: 0.5rem 1rem;

  > :first-child {
    display: flex;
  }

  &-low {
    color: mistakes-journal.color('success');
  }

  &-medium {
    color: mistakes-journal.color('warning');
  }

  &-high {
    color: mistakes-journal.color('error');
  }

  &-text {
    @include mistakes-journal.font-semi-bold(0.8em);
    padding-left: 1rem;
  }
}
</style>
