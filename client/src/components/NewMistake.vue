<template>
  <div class="row">
    <div class="col-12 col-md-8 col-xl-6 col-xxl-4">
      <div class="row">
        <div class="col-12">
          <label
            class="form-label"
            for="mistakeName">{{ $t('NewMistake.Name') }}</label>
          <input
            id="mistakeName"
            v-model="mistake.name"
            :placeholder="$t('NewMistake.NamePlaceholder')"
            class="form-control"
            type="email">
        </div>
        <div class="col-12">
          <label
            class="form-label"
            for="mistakeGoal">{{ $t('NewMistake.Goal') }}</label>
          <input
            id="mistakeGoal"
            v-model="mistake.goal"
            :placeholder="$t('NewMistake.GoalPlaceholder')"
            class="form-control"
            type="email">
        </div>
        <div class="col-12">
          <label class="form-label">{{ $t('NewMistake.Tips') }}</label>
          <div class="mj-mistake-tips">
            <div
              v-for="tip in mistake.tips"
              :key="tip.id"
              class="input-group">
              <span class="input-group-text">
                <mj-icon
                  class="mj-mistake-tips-icon"
                  name="tips" /></span>
              <input
                v-model="tip.value"
                class="form-control"
                type="text"
                @change="checkIfShouldAddATip(tip.id, $event)">
            </div>
          </div>
        </div>
        <div class="col-12">
          <label
            class="form-label"
            for="mistakePriority">{{ $t('NewMistake.Priority') }}</label>
          <select
            id="mistakePriority"
            v-model="mistake.priority"
            class="form-select">
            <option
              v-for="availablePriority in availablePriorities"
              :key="availablePriority"
              :value="availablePriority">
              {{ $t('Priority.' + availablePriority) }}
            </option>
          </select>
        </div>
        <div class="col-12 mt-4 mj-action-items">
          <button
            class="btn btn-outline-secondary"
            type="button"
            @click="cancel()">
            {{ $t('NewMistake.CancelButton') }}
          </button>
          <button
            class="btn btn-primary with-icon"
            type="button"
            @click="save()">
            <mj-icon
              class="btn-icon"
              name="add_circle" />
            <span class="btn-text">{{ $t('NewMistake.AddButton') }}</span>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { MistakePriority } from '@/model/mistake-priority.enum';
import {
  NewMistake,
  NewMistakeTip,
} from '@/model/new-mistake';
import { MistakesActions } from '@/store/mistakes-module/actions';
import { getEnumValues } from '@/utils/object.utils';
import Vue from 'vue';

export default Vue.extend({
  name: 'NewMistake',
  data(): { mistake: NewMistake, availablePriorities: string[] } {
    return {
      mistake: {
        name: '',
        goal: '',
        tips: [{ id: 1, value: '' }],
        priority: MistakePriority.Medium,
      },
      availablePriorities: getEnumValues(MistakePriority),
    };
  },
  methods: {
    checkIfShouldAddATip(id: number, { target }: InputEvent): void {
      const inputValue = (target as HTMLInputElement).value;
      const lastElementId = this.mistake.tips[this.mistake.tips.length - 1].id;

      if (!inputValue) {
        const index = this.mistake.tips.findIndex((t: NewMistakeTip) => t.id === id);
        this.mistake.tips.splice(index, 1);
      } else if (id === lastElementId) {
        this.mistake.tips.push({ id: lastElementId + 1, value: '' });
      }
    },
    cancel(): void {
      this.$router.push('/journal/mistakes');
    },
    async save(): Promise<void> {
      await this.$store.dispatch(MistakesActions.AddMistake, this.mistake);
      await this.$router.push('/journal/mistakes');
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
.mj-mistake-tips {
  > div {
    &:not(:last-child) {
      margin-bottom: 1rem;
    }
  }
}

.mj-action-items {
  text-align: right;

  button {
    &:not(:last-child) {
      margin-right: 1rem;
    }
  }
}
</style>
