<template>
  <div class="col-12">
    <label class="form-label">{{ $t('MistakeForm.Tips') }}</label>
    <div class="mj-mistake-tips">
      <div
        v-for="(v, index) in $v.tips.$each.$iter"
        :key="index"
        class="input-group">
        <span class="input-group-text">
          <remix-icon
            class="mj-mistake-tips-icon"
            icon="lightbulb" />
        </span>
        <input
          :class="{ 'is-invalid': v.$dirty && v.$invalid }"
          :placeholder="parseInt(index) === 0 ? $t('MistakeForm.TipPlaceholder') : undefined"
          :value="tips[index]"
          class="form-control"
          type="text"
          @input="handleInput(index, $event.target.value); v.$touch();">
        <button
          v-if="parseInt(index) !== 0"
          class="btn"
          type="button"
          @click="removeTip(index)">
          <fa-icon
            :icon="['far', 'trash-alt']"
            class="btn-icon"
            transform="shrink-1" />
        </button>
        <mj-error :is-visible="!v.maxLength">
          {{ $t('FormErrors.MaxLength', { max: v.$params.maxLength.max }) }}
        </mj-error>
      </div>
      <button
        class="btn btn-outline-primary ms-auto btn-sm with-icon"
        type="button"
        @click="addTip()">
        <span class="btn-icon">
          <fa-icon
            :icon="['fas', 'plus']"
            transform="shrink-1" />
        </span>
        <span class="btn-text">{{ $t('MistakeForm.NewTip') }}</span>
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import { maxShortTextLength } from '@/utils/validators.utils';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakeTips',
  props: {
    value: {
      type: Array,
    },
  },
  data(): { tips: string[] } {
    return {
      tips: this.$props.value,
    };
  },
  validations: {
    tips: {
      $each: {
        maxLength: maxShortTextLength,
      },
    },
  },
  watch: {
    value(tips: string[]): void {
      this.tips = tips;
    },
  },
  methods: {
    addTip(): void {
      this.tips.push('');
    },
    removeTip(index: number): void {
      this.tips.splice(index, 1);
    },
    handleInput(index: number, tip: string): void {
      this.$set(this.tips, index, tip);
      this.$emit('input', this.tips);
    },
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

.mj-mistake-tips {
  display: flex;
  flex-direction: column;

  > div {
    margin-bottom: 1rem;
  }
}
</style>
