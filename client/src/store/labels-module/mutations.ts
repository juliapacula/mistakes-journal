import { Label } from '@/model/label';
import { MutationTree } from 'vuex';
import { LabelsState } from './state';

export enum LabelsMutations {
  AddLabel = 'labels/AddLabel',
  SetLabels = 'labels/SetLabels',
  SetLabel = 'labels/SetLabel',
  DeleteLabel = 'labels/DeleteLabel',
  UpdateLabel = 'labels/UpdateLabel',
}

export const mutations: MutationTree<LabelsState> = {
  [LabelsMutations.AddLabel](state: LabelsState, label: Label): void {
    state.labels = [...state.labels, label];
  },
  [LabelsMutations.SetLabels](state: LabelsState, labels: Label[]): void {
    state.labels = [...labels];
  },
  [LabelsMutations.SetLabel](state: LabelsState, label: Label | null): void {
    state.label = label ? { ...label } : null;
  },
  [LabelsMutations.DeleteLabel](state: LabelsState, labelId: string): void {
    state.labels = state.labels.filter((l) => l.id !== labelId);
  },
  [LabelsMutations.UpdateLabel](state: LabelsState, label: Label): void {
    state.labels = state.labels.map((l) => (l.id === label.id ? label : l));

    if (state.label?.id === label.id) {
      state.label = { ...label };
    }
  },
};
