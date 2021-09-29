<template>
  <div class="row">
    <div
      v-if="isMistakeLoaded"
      class="col-12 col-md-8 col-xl-6 col-xxl-4">
      <div class="row">
        <div class="col-12">
          <label
            class="form-label"
            for="mistakeName">{{ $t('MistakeForm.Name') }}</label>
          <input
            id="mistakeName"
            v-model="$v.mistake.name.$model"
            :class="{ 'is-invalid': $v.mistake.name.$dirty && $v.mistake.name.$invalid }"
            :placeholder="$t('MistakeForm.NamePlaceholder')"
            class="form-control"
            type="text">
          <mj-error :is-visible="!$v.mistake.name.required">
            {{ $t('FormErrors.Required') }}
          </mj-error>
          <mj-error :is-visible="!$v.mistake.name.maxLength">
            {{ $t('FormErrors.MaxLength', { max: $v.mistake.name.$params.maxLength.max }) }}
          </mj-error>
        </div>
        <div class="col-12">
          <label
            class="form-label"
            for="mistakeGoal">{{ $t('MistakeForm.Goal') }}</label>
          <input
            id="mistakeGoal"
            v-model="$v.mistake.goal.$model"
            :class="{ 'is-invalid': $v.mistake.goal.$dirty && $v.mistake.goal.$invalid }"
            :placeholder="$t('MistakeForm.GoalPlaceholder')"
            class="form-control"
            type="text">
          <mj-error :is-visible="!$v.mistake.goal.maxLength">
            {{ $t('FormErrors.MaxLength', { max: $v.mistake.goal.$params.maxLength.max }) }}
          </mj-error>
        </div>
        <div class="col-12">
          <label class="form-label">{{ $t('MistakeForm.Tips') }}</label>
          <div class="mj-mistake-tips">
            <div
              v-for="(v, index) in $v.mistake.tips.$each.$iter"
              :key="index"
              class="input-group">
              <span class="input-group-text">
                <mj-icon
                  class="mj-mistake-tips-icon"
                  name="tips" /></span>
              <input
                :class="{ 'is-invalid': v.$dirty && v.$invalid }"
                :value="mistake.tips[index]"
                class="form-control"
                type="text"
                @change="checkIfShouldAddATip(index, $event)"
                @input="$set(mistake.tips, index, $event.target.value); v.$touch()">
              <mj-error :is-visible="!v.maxLength">
                {{ $t('FormErrors.MaxLength', { max: v.$params.maxLength.max }) }}
              </mj-error>
            </div>
          </div>
        </div>
        <div class="col-12">
          <label
            class="form-label"
            for="mistakePriority">{{ $t('MistakeForm.Priority') }}</label>
          <select
            id="mistakePriority"
            v-model="$v.mistake.priority.$model"
            :class="{ 'is-invalid': $v.mistake.priority.$dirty && $v.mistake.priority.$invalid }"
            class="form-select">
            <option
              v-for="availablePriority in availablePriorities"
              :key="availablePriority"
              :value="availablePriority">
              {{ $t('Priority.' + availablePriority) }}
            </option>
          </select>
          <mj-error :is-visible="!$v.mistake.priority.required">
            {{ $t('FormErrors.Required') }}
          </mj-error>
        </div>
        <div class="col-12 mt-4 mj-action-items">
          <button
            class="btn btn-outline-secondary"
            type="button"
            @click="cancel()">
            {{ $t('MistakeForm.CancelButton') }}
          </button>
          <span v-if="!isEditingMistake">
            <button
              :disabled="$v.$invalid"
              class="btn btn-primary with-icon"
              type="button"
              @click="save()">
              <mj-icon
                class="btn-icon"
                name="add_circle" />
              <span class="btn-text">{{ $t('MistakeForm.AddButton') }}</span>
            </button>
          </span>
          <span v-else>
            <button
              :disabled="$v.$invalid"
              class="btn btn-primary with-icon"
              type="button"
              @click="update()">
              <span class="btn-text">{{ $t('MistakeForm.UpdateButton') }}</span>
            </button>
          </span>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Mistake } from '@/model/mistake';
import { MistakePriority } from '@/model/mistake-priority.enum';
import { NewMistake } from '@/model/new-mistake';
import { MistakesActions } from '@/store/mistakes-module/actions';
import { MistakesMutations } from '@/store/mistakes-module/mutations';
import { getEnumValues } from '@/utils/object.utils';
import { maxShortTextLength } from '@/utils/validators.utils';
import Vue from 'vue';
import { Route } from 'vue-router';
import { required } from 'vuelidate/lib/validators';

export default Vue.extend({
  name: 'MistakeForm',
  data(): { mistake: NewMistake, availablePriorities: string[] } {
    return {
      mistake: {
        name: '',
        goal: '',
        tips: [''],
        priority: MistakePriority.Medium,
      },
      availablePriorities: getEnumValues(MistakePriority),
    };
  },
  computed: {
    isEditingMistake(): boolean {
      return this.$route.name === 'MistakeEdit';
    },
    isMistakeLoaded(): boolean {
      return !this.isEditingMistake || (this.isEditingMistake && !!this.mistakeToEdit);
    },
    mistakeToEdit(): Mistake | null {
      return this.$store.state.mistakes.mistake;
    },
  },
  watch: {
    async $route(route: Route) {
      if (route.params.id) {
        await this.fetchMistake();
      }
    },
    mistakeToEdit(mistake: Mistake) {
      if (!this.isEditingMistake) {
        return;
      }

      this.mistake = {
        name: mistake.name,
        goal: mistake.goal ?? '',
        priority: mistake.priority,
        tips: mistake.tips?.length > 0 ? mistake.tips : [''],
      };
    },
  },
  async created(): Promise<void> {
    if (!this.isEditingMistake) {
      return;
    }

    await this.fetchMistake();
  },
  async destroyed(): Promise<void> {
    await this.$store.commit(MistakesMutations.SetMistake, null);
  },
  validations: {
    mistake: {
      name: {
        required,
        maxLength: maxShortTextLength,
      },
      priority: {
        required,
      },
      goal: {
        maxLength: maxShortTextLength,
      },
      tips: {
        $each: {
          maxLength: maxShortTextLength,
        },
      },
    },
  },
  methods: {
    async fetchMistake(): Promise<void> {
      await this.$store.dispatch(MistakesActions.Get, this.$route.params.id);
    },
    checkIfShouldAddATip(id: string, { target }: InputEvent): void {
      const inputValue = (target as HTMLInputElement).value;
      const index = parseInt(id, 10);

      if (!inputValue && this.mistake.tips.length === 1) {
        return;
      }

      if (!inputValue) {
        this.mistake.tips.splice(index, 1);
      } else if (index === (this.mistake.tips.length - 1)) {
        this.mistake.tips.push('');
      }
    },
    async cancel(): Promise<void> {
      await this.goToMistakesList();
    },
    async save(): Promise<void> {
      await this.$store.dispatch(MistakesActions.AddMistake, this.mistake);
      await this.goToMistakesList();
    },
    async update(): Promise<void> {
      await this.$store.dispatch(MistakesActions.UpdateMistake, { mistakeId: this.$route.params.id, mistake: this.mistake });
      await this.goToMistakesList(this.$route.params.id);
    },
    async goToMistakesList(id?: string): Promise<void> {
      if (id) {
        await this.$router.push(`/journal/mistakes/${id}`);
      } else {
        await this.$router.push('/journal/mistakes');
      }
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
.mj-mistake-tips {
  > div {
    margin-bottom: 1rem;
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
