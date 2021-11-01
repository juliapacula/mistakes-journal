<template>
  <b-modal
    id="label-modal"
    :ok-disabled="$v.$invalid"
    :ok-title="isEditingLabel ? $t('LabelModal.Action.Save') : $t('LabelModal.Action.Add')"
    ok-only
    @hidden="resetState()"
    @ok="isEditingLabel ? saveLabel() : addLabel()">
    <template v-slot:modal-header>
      <h1 v-if="isEditingLabel">
        {{ $t('LabelModal.Title.ExistingLabel', { name: editedLabel.name }) }}
      </h1>
      <h1 v-else>
        {{ $t('LabelModal.Title.NewLabel') }}
      </h1>
      <button
        v-tooltip="$t('Modal.Close')"
        class="btn"
        type="button"
        @click="$bvModal.hide('label-modal')">
        <span class="btn-icon">
          <fa-icon :icon="['fas', 'times']" />
        </span>
      </button>
    </template>
    <div class="row">
      <div class="col-12 mb-3">
        <label
          class="form-label"
          for="labelName">{{ $t('LabelModal.Name') }}</label>
        <input
          id="labelName"
          v-model.trim="$v.label.name.$model"
          :class="{ 'is-invalid': $v.label.name.$dirty && $v.label.name.$invalid }"
          class="form-control"
          type="text">
        <mj-error :is-visible="!$v.label.name.required">
          {{ $t('FormErrors.Required') }}
        </mj-error>
        <mj-error :is-visible="!$v.label.name.maxLength">
          {{ $t('FormErrors.MaxLength', { max: $v.label.name.$params.maxLength.max }) }}
        </mj-error>
      </div>
      <div class="col-12 mb-3">
        <label
          class="form-label"
          for="labelColor">{{ $t('LabelModal.Color') }}</label>
        <div class="input-group">
          <div class="input-group-text">
            <span
              :style="{ color: label.color}"
              class="mj-label-color" />
          </div>
          <select
            id="labelColor"
            v-model="$v.label.color.$model"
            :class="{ 'is-invalid': $v.label.color.$dirty && $v.label.color.$invalid }"
            class="form-select">
            <option
              v-for="color in labelColors"
              :key="color.color"
              :value="color.color">
              {{ $t('LabelColors.' + color.nameKey) }}
            </option>
          </select>
          <mj-error :is-visible="!$v.label.color.required">
            {{ $t('FormErrors.Required') }}
          </mj-error>
        </div>
      </div>
    </div>
  </b-modal>
</template>

<script lang="ts">
import { LABEL_COLORS } from '@/config/label-colors.config';
import { Label } from '@/model/label';
import { LabelColor } from '@/model/label-color';
import { LabelsActions } from '@/store/labels-module/actions';
import { LabelsMutations } from '@/store/labels-module/mutations';
import { maxVeryShortTextLength } from '@/utils/validators.utils';
import Vue from 'vue';
import { required } from 'vuelidate/lib/validators';

export default Vue.extend({
  name: 'LabelModal',
  data(): { label: Label, labelColors: LabelColor[] } {
    return {
      label: {
        id: '',
        name: '',
        color: LABEL_COLORS[0].color,
        mistakesCounter: 0,
      },
      labelColors: LABEL_COLORS,
    };
  },
  computed: {
    isEditingLabel(): boolean {
      return this.$store.state.labels.label !== null;
    },
    editedLabel(): Label | null {
      return this.$store.state.labels.label;
    },
  },
  watch: {
    editedLabel(label: Label | null): void {
      if (label?.id) {
        this.label = { ...label };
      } else {
        this.label = {
          id: '',
          name: '',
          color: LABEL_COLORS[0].color,
          mistakesCounter: 0,
        };
        this.$v.$reset();
      }
    },
  },
  validations: {
    label: {
      name: {
        required,
        maxLength: maxVeryShortTextLength,
      },
      color: {
        required,
      },
    },
  },
  methods: {
    resetState(): void {
      this.$store.commit(LabelsMutations.SetLabel, null);
      this.label = {
        id: '',
        name: '',
        color: LABEL_COLORS[0].color,
        mistakesCounter: 0,
      };
      this.$v.$reset();
    },
    async saveLabel(): Promise<void> {
      await this.$store.dispatch(LabelsActions.Update, this.label);
      this.resetState();
    },
    async addLabel(): Promise<void> {
      await this.$store.dispatch(LabelsActions.Add, this.label);
      this.resetState();
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-label-color {
  display: block;
  width: 1rem;
  height: 1rem;
  border: 2px solid mistakes-journal.color('gray', '900');
  border-radius: 50%;
  background-color: currentColor;
}

.modal-header h1 {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}
</style>
