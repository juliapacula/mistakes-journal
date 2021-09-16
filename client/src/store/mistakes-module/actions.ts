import { MistakesApiMethods } from '@/api/methods/mistakes.api-methods';
import { MistakeApiModel } from '@/api/models/mistake.api-model';
import { NewMistakeApiModel } from '@/api/models/new-mistake.api-model';
import { NewMistake } from '@/model/new-mistake';
import { MistakesMutations } from '@/store/mistakes-module/mutations';
import { MistakesState } from '@/store/mistakes-module/state';
import { State } from '@/store/state';
import {
  ActionContext,
  ActionTree,
} from 'vuex';

type Context = ActionContext<MistakesState, State>;

export enum MistakesActions {
  AddMistake = 'mistakes/AddNewMistake',
  GetAll = 'mistakes/GetAll',
  Get = 'mistakes/Get',
  Delete = 'mistakes/Delete',
  UpdateMistake = 'mistakes/UpdateMistake',
}

export const actions: ActionTree<MistakesState, State> = {
  async [MistakesActions.AddMistake]({ commit }: Context, mistake: NewMistake): Promise<void> {
    const payload: NewMistakeApiModel = {
      name: mistake.name,
      goal: !mistake.goal ? null : mistake.goal,
      tips: mistake.tips.filter((t: string) => !!t),
      priority: mistake.priority,
    };

    const addedMistake = await MistakesApiMethods.addNew(payload);

    commit(MistakesMutations.AddMistake, addedMistake);
  },
  async [MistakesActions.GetAll]({ commit }: Context): Promise<void> {
    const mistakes = await MistakesApiMethods.getAll();

    commit(MistakesMutations.SetMistakes, mistakes);
  },

  async [MistakesActions.Get]({ commit }: Context, mistakeId: string): Promise<void> {
    const mistake = await MistakesApiMethods.get(mistakeId);

    commit(MistakesMutations.SetMistake, mistake);
  },

  async [MistakesActions.Delete]({ commit }: Context, mistakeId: string): Promise<void> {
    await MistakesApiMethods.delete(mistakeId);

    commit(MistakesMutations.DeleteMistake, mistakeId);
  },

  async [MistakesActions.UpdateMistake](
    { commit }: Context,
    { mistakeId, mistake }: { mistakeId: string, mistake: NewMistake },
  ): Promise<void> {
    const payload: MistakeApiModel = {
      id: mistakeId,
      name: mistake.name,
      goal: !mistake.goal ? null : mistake.goal,
      tips: mistake.tips.filter((t: string) => !!t),
      priority: mistake.priority,
    };

    const updatedMistake = await MistakesApiMethods.updateMistake(payload);

    commit(MistakesMutations.SetMistake, updatedMistake);
  },
};
