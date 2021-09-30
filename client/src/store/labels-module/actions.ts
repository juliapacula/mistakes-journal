import { LabelsApiMethods } from '@/api/methods/labels.api-methods';
import { LabelApiModel } from '@/api/models/label.api-model';
import { NewLabelApiModel } from '@/api/models/new-label.api-model';
import { Label } from '@/model/label';
import { LabelsMutations } from '@/store/labels-module/mutations';
import { LabelsState } from '@/store/labels-module/state';
import { State } from '@/store/state';
import {
  ActionContext,
  ActionTree,
} from 'vuex';

type Context = ActionContext<LabelsState, State>;

export enum LabelsActions {
  GetAll = 'labels/GetAll',
  Add = 'labels/Add',
  Get = 'labels/Get',
  Delete = 'labels/Delete',
  Update = 'labels/Update',
}

export const actions: ActionTree<LabelsState, State> = {
  async [LabelsActions.Add]({ commit }: Context, label: Label): Promise<void> {
    const payload: NewLabelApiModel = {
      name: label.name,
      color: label.color,
    };

    const addedLabel = await LabelsApiMethods.addNew(payload);

    commit(LabelsMutations.AddLabel, addedLabel);
  },
  async [LabelsActions.GetAll]({ commit }: Context): Promise<void> {
    const labels = await LabelsApiMethods.getAll();

    commit(LabelsMutations.SetLabels, labels);
  },
  async [LabelsActions.Get]({ commit }: Context, labelId: string): Promise<void> {
    const label = await LabelsApiMethods.get(labelId);

    commit(LabelsMutations.SetLabel, label);
  },
  async [LabelsActions.Delete]({ commit }: Context, labelId: string): Promise<void> {
    await LabelsApiMethods.delete(labelId);

    commit(LabelsMutations.DeleteLabel, labelId);
  },
  async [LabelsActions.Update]({ commit }: Context, label: Label): Promise<void> {
    const payload: LabelApiModel = {
      id: label.id,
      name: label.name,
      color: label.color,
      mistakesCounter: label.mistakesCounter,
    };

    const updatedLabel = await LabelsApiMethods.update(payload);

    commit(LabelsMutations.UpdateLabel, updatedLabel);
  },
};
