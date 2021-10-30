<template>
  <div class="col-12">
    <label class="form-label">{{ $t('MistakeForm.Tips') }}</label>
    <div class="mj-mistake-tips">
      <div
        v-for="(v, index) in $v.tips.$each.$iter"
        :key="index"
        class="input-group">
        <span class="input-group-text">
          <mj-icon
            class="mj-mistake-tips-icon"
            name="tips" />
        </span>
        <input
          :class="{ 'is-invalid': v.$dirty && v.$invalid }"
          :value="tips[index]"
          class="form-control"
          type="text"
          @change="checkIfShouldAddATip(index, $event)"
          @input="handleInput(index, $event.target.value); v.$touch();">
        <mj-error :is-visible="!v.maxLength">
          {{ $t('FormErrors.MaxLength', { max: v.$params.maxLength.max }) }}
        </mj-error>
      </div>
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
    checkIfShouldAddATip(id: string, { target }: InputEvent): void {
      const inputValue = (target as HTMLInputElement).value;
      const index = parseInt(id, 10);

      if (!inputValue && this.tips.length === 1) {
        return;
      }

      if (!inputValue) {
        this.tips.splice(index, 1);
      } else if (index === (this.tips.length - 1)) {
        this.tips.push('');
      }
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
.mj-mistake-tips {
  > div {
    margin-bottom: 1rem;
  }
}
</style>
