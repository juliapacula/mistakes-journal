import { LabelsApiMethods } from '@/api/methods/labels.api-methods';
import { LabelApiModel } from '@/api/models/label.api-model';
import { NewLabelApiModel } from '@/api/models/new-label.api-model';
import { Label } from '@/model/label';
import { LabelsMutations } from '@/store/labels-module/mutations';
import { LabelsState } from '@/store/labels-module/state';
import { MistakesActions } from '@/store/mistakes-module/actions';
import { State } from '@/store/state';
import { handleDefaultResponseErrors } from '@/utils/errors.utils';
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

    try {
      const addedLabel = await LabelsApiMethods.addNew(payload);

      commit(LabelsMutations.AddLabel, addedLabel);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },
  async [LabelsActions.GetAll]({ commit }: Context): Promise<void> {
    try {
      const labels = await LabelsApiMethods.getAll();

      commit(LabelsMutations.SetLabels, labels);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },
  async [LabelsActions.Get]({ commit }: Context, labelId: string): Promise<void> {
    try {
      const label = await LabelsApiMethods.get(labelId);

      commit(LabelsMutations.SetLabel, label);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },
  async [LabelsActions.Delete]({ commit }: Context, labelId: string): Promise<void> {
    try {
      await LabelsApiMethods.delete(labelId);

      commit(LabelsMutations.DeleteLabel, labelId);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },
  async [LabelsActions.Update]({ commit, dispatch }: Context, label: Label): Promise<void> {
    const payload: LabelApiModel = {
      id: label.id,
      name: label.name,
      color: label.color,
      mistakesCounter: label.mistakesCounter,
    };

    try {
      const updatedLabel = await LabelsApiMethods.update(payload);

      commit(LabelsMutations.UpdateLabel, updatedLabel);
      await dispatch(MistakesActions.GetAll);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },
};
