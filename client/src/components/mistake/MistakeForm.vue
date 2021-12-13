<template>
  <div class="row">
    <div
      v-if="isMistakeLoaded"
      class="col-12 col-md-10 col-lg-8 col-xl-7 col-xxl-5">
      <div class="row align-items-center flex-sm-row-reverse">
        <div
          id="step-5"
          class="col-12 col-md-6 mj-form-group mb-0 text-end">
          <div class="form-check form-switch">
            <label
              class="form-check-label"
              for="isDeepAnalyzer">{{ $t('MistakeForm.DeepAnalyzer') }}</label>
            <input
              id="isDeepAnalyzer"
              v-model="isDeepAnalyzer"
              class="form-check-input"
              type="checkbox">
          </div>
        </div>
        <div class="col-12 col-md-6 mj-mistake-section">
          {{ $t('MistakeForm.Identify') }}
        </div>
      </div>
      <div class="row">
        <div class="col-12 mj-form-group">
          <label
            class="form-label"
            for="mistakeName">{{ $t('MistakeForm.Name') }}</label>
          <div class="input-group">
            <span class="input-group-text">
              <mj-icon name="ice-cream-name-mistake" />
            </span>
            <input
              id="mistakeName"
              v-model.trim="$v.mistake.name.$model"
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
        </div>
        <div v-if="isDeepAnalyzer">
          <div class="col-12 mj-mistake-section">
            {{ $t('MistakeForm.Analyze') }}
          </div>
          <div class="col-12 mj-form-group">
            <label
              class="form-label"
              for="mistakeConsequences">{{ $t('MistakeForm.Consequences') }}</label>
            <div class="input-group">
              <span class="input-group-text">
                <remix-icon icon="meteor" />
              </span>
              <input
                id="mistakeConsequences"
                v-model.trim="$v.mistake.mistakeAdditionalQuestions.consequences.$model"
                :class="{ 'is-invalid': $v.mistake.mistakeAdditionalQuestions.consequences.$dirty && $v.mistake.mistakeAdditionalQuestions.consequences.$invalid }"
                class="form-control"
                type="text">
              <mj-error :is-visible="!$v.mistake.mistakeAdditionalQuestions.consequences.maxLength">
                {{
                  $t('FormErrors.MaxLength', { max: $v.mistake.mistakeAdditionalQuestions.consequences.$params.maxLength.max })
                }}
              </mj-error>
            </div>
          </div>
          <div class="col-12 mj-form-group">
            <label
              class="form-label"
              for="mistakeWhatCanIDoBetter">{{ $t('MistakeForm.WhatCanIDoBetter') }}</label>
            <div class="input-group">
              <span class="input-group-text">
                <remix-icon icon="rocket-2" />
              </span>
              <input
                id="mistakeWhatCanIDoBetter"
                v-model.trim="$v.mistake.mistakeAdditionalQuestions.whatCanIDoBetter.$model"
                :class="{ 'is-invalid': $v.mistake.mistakeAdditionalQuestions.whatCanIDoBetter.$dirty && $v.mistake.mistakeAdditionalQuestions.whatCanIDoBetter.$invalid }"
                class="form-control"
                type="text">
              <mj-error :is-visible="!$v.mistake.mistakeAdditionalQuestions.whatCanIDoBetter.maxLength">
                {{
                  $t('FormErrors.MaxLength', { max: $v.mistake.mistakeAdditionalQuestions.whatCanIDoBetter.$params.maxLength.max })
                }}
              </mj-error>
            </div>
          </div>
          <div class="col-12 mj-form-group">
            <label
              class="form-label"
              for="mistakeWhatDidILearn">{{ $t('MistakeForm.WhatDidILearn') }}</label>
            <div class="input-group">
              <span class="input-group-text">
                <remix-icon icon="psychotherapy" />
              </span>
              <input
                id="mistakeWhatDidILearn"
                v-model.trim="$v.mistake.mistakeAdditionalQuestions.whatDidILearn.$model"
                :class="{ 'is-invalid': $v.mistake.mistakeAdditionalQuestions.whatDidILearn.$dirty && $v.mistake.mistakeAdditionalQuestions.whatDidILearn.$invalid }"
                class="form-control"
                type="text">
              <mj-error :is-visible="!$v.mistake.mistakeAdditionalQuestions.whatDidILearn.maxLength">
                {{
                  $t('FormErrors.MaxLength', { max: $v.mistake.mistakeAdditionalQuestions.whatDidILearn.$params.maxLength.max })
                }}
              </mj-error>
            </div>
          </div>
          <div class="col-12 mj-form-group">
            <label
              class="form-label"
              for="mistakeCanIFixIt">{{ $t('MistakeForm.CanIFixIt') }}</label>
            <div class="input-group">
              <span class="input-group-text">
                <remix-icon icon="tools" />
              </span>
              <input
                id="mistakeCanIFixIt"
                v-model.trim="$v.mistake.mistakeAdditionalQuestions.canIFixIt.$model"
                :class="{ 'is-invalid': $v.mistake.mistakeAdditionalQuestions.canIFixIt.$dirty && $v.mistake.mistakeAdditionalQuestions.canIFixIt.$invalid }"
                class="form-control"
                type="text">
              <mj-error :is-visible="!$v.mistake.mistakeAdditionalQuestions.canIFixIt.maxLength">
                {{
                  $t('FormErrors.MaxLength', { max: $v.mistake.mistakeAdditionalQuestions.canIFixIt.$params.maxLength.max })
                }}
              </mj-error>
            </div>
          </div>
          <div class="col-12 mj-form-group">
            <label
              class="form-label"
              for="mistakeOnlyResponsible">{{ $t('MistakeForm.OnlyResponsible') }}</label>
            <div class="input-group">
              <span class="input-group-text">
                <remix-icon icon="user-search" />
              </span>
              <input
                id="mistakeOnlyResponsible"
                v-model.trim="$v.mistake.mistakeAdditionalQuestions.onlyResponsible.$model"
                :class="{ 'is-invalid': $v.mistake.mistakeAdditionalQuestions.onlyResponsible.$dirty && $v.mistake.mistakeAdditionalQuestions.onlyResponsible.$invalid }"
                class="form-control"
                type="text">
              <mj-error :is-visible="!$v.mistake.mistakeAdditionalQuestions.onlyResponsible.maxLength">
                {{
                  $t('FormErrors.MaxLength', { max: $v.mistake.mistakeAdditionalQuestions.onlyResponsible.$params.maxLength.max })
                }}
              </mj-error>
            </div>
          </div>
        </div>
        <div class="col-12 mj-mistake-section">
          {{ $t('MistakeForm.Control') }}
        </div>
        <div class="col-12 mj-form-group">
          <label
            class="form-label"
            for="mistakeGoal">{{ $t('MistakeForm.Goal') }}</label>
          <div class="input-group">
            <span class="input-group-text">
              <remix-icon icon="focus-3" />
            </span>
            <input
              id="mistakeGoal"
              v-model.trim="$v.mistake.goal.$model"
              :class="{ 'is-invalid': $v.mistake.goal.$dirty && $v.mistake.goal.$invalid }"
              :placeholder="$t('MistakeForm.GoalPlaceholder')"
              class="form-control"
              type="text">
            <mj-error :is-visible="!$v.mistake.goal.maxLength">
              {{ $t('FormErrors.MaxLength', { max: $v.mistake.goal.$params.maxLength.max }) }}
            </mj-error>
          </div>
        </div>
        <mistake-tips
          v-model="$v.mistake.tips.$model"
          class="mj-form-group" />
        <mistake-labels
          v-model="mistake.labelIds"
          class="mj-form-group" />
        <div class="col-12 mj-form-group">
          <label
            class="form-label"
            for="mistakePriority">{{ $t('MistakeForm.Priority') }}</label>
          <select
            id="mistakePriority"
            v-model.trim="$v.mistake.priority.$model"
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
        <div class="col-12 my-4 mj-action-items">
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
              <span class="btn-icon">
                <fa-icon :icon="['fas', 'plus']" />
              </span>
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
    <adding-mistake-on-boarding />
  </div>
</template>

<script lang="ts">
import MistakeLabels from '@/components/mistake/MistakeLabels.vue';
import MistakeTips from '@/components/mistake/MistakeTips.vue';
import AddingMistakeOnBoarding from '@/components/on-boarding/AddingMistakeOnBoarding.vue';
import { Label } from '@/model/label';
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
  components: { AddingMistakeOnBoarding, MistakeLabels, MistakeTips },
  data(): { isDeepAnalyzer: boolean, mistake: NewMistake, availablePriorities: string[] } {
    return {
      isDeepAnalyzer: false,
      mistake: {
        name: '',
        mistakeAdditionalQuestions: null,
        goal: '',
        tips: [''],
        labelIds: [],
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
    isDeepAnalyzer(isDeepAnalyzer: boolean) {
      if (!isDeepAnalyzer) {
        this.mistake.mistakeAdditionalQuestions = null;
      } else {
        this.mistake.mistakeAdditionalQuestions = {
          consequences: '',
          whatCanIDoBetter: '',
          whatDidILearn: '',
          canIFixIt: '',
          onlyResponsible: '',
        };
      }
    },
    mistakeToEdit(mistake: Mistake) {
      if (!this.isEditingMistake) {
        return;
      }

      if (mistake.mistakeAdditionalQuestions
        && mistake.mistakeAdditionalQuestions.consequences !== null
        && mistake.mistakeAdditionalQuestions.onlyResponsible !== null
        && mistake.mistakeAdditionalQuestions.canIFixIt !== null
        && mistake.mistakeAdditionalQuestions.whatDidILearn !== null
        && mistake.mistakeAdditionalQuestions.whatCanIDoBetter !== null) {
        this.isDeepAnalyzer = true;
      }

      this.mistake = {
        name: mistake.name,
        mistakeAdditionalQuestions: {
          consequences: mistake.mistakeAdditionalQuestions?.consequences ?? '',
          whatCanIDoBetter: mistake.mistakeAdditionalQuestions?.whatCanIDoBetter ?? '',
          whatDidILearn: mistake.mistakeAdditionalQuestions?.whatDidILearn ?? '',
          canIFixIt: mistake.mistakeAdditionalQuestions?.canIFixIt ?? '',
          onlyResponsible: mistake.mistakeAdditionalQuestions?.onlyResponsible ?? '',
        },
        labelIds: mistake.labels?.map((l: Label) => l.id) ?? [],
        goal: mistake.goal ?? '',
        tips: mistake.tips?.length > 0 ? mistake.tips : [''],
        priority: mistake.priority,
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
      mistakeAdditionalQuestions: {
        consequences: {
          maxLength: maxShortTextLength,
        },
        whatCanIDoBetter: {
          maxLength: maxShortTextLength,
        },
        whatDidILearn: {
          maxLength: maxShortTextLength,
        },
        canIFixIt: {
          maxLength: maxShortTextLength,
        },
        onlyResponsible: {
          maxLength: maxShortTextLength,
        },
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
    async cancel(): Promise<void> {
      await this.goToMistakesList();
    },
    async save(): Promise<void> {
      await this.$store.dispatch(MistakesActions.AddMistake, this.mistake);
      await this.goToMistakesList();
    },
    async update(): Promise<void> {
      await this.$store.dispatch(
        MistakesActions.UpdateMistake,
        { mistakeId: this.$route.params.id, mistake: this.mistake },
      );
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
@use '../../styles/mistakes-journal';

.mj-mistake-section {
  @include mistakes-journal.font-medium(0.9rem);
  display: flex;
  align-items: center;
  margin: 0.5rem 0;
  color: mistakes-journal.color('gray', '400');
  text-transform: lowercase;

  &::before {
    content: '';
    display: block;
    width: 5rem;
    height: 2px;
    margin-right: 0.5rem;
    border-radius: 0.2rem;
    background-color: mistakes-journal.color('gray', '400');
  }
}

.mj-form-group {
  margin-bottom: 0.5rem;

  label {
    @include mistakes-journal.font-regular(1rem);
  }

  .form-switch {
    padding-right: 0;
    padding-left: 0;
  }

  .form-check-input {
    float: right;
  }

  .form-check-label {
    width: 100%;
    padding-right: 2.5rem;
    float: left;
    text-transform: lowercase;
  }
}

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
