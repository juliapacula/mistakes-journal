import { MistakesApiMethods } from '@/api/methods/mistakes.api-methods';
import { NewMistakeApiModel } from '@/api/models/new-mistake.api-model';
import {
  NewMistake,
  NewMistakeTip,
} from '@/model/new-mistake';
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
}

export const actions: ActionTree<MistakesState, State> = {
  async [MistakesActions.AddMistake]({ commit }: Context, mistake: NewMistake): Promise<void> {
    const payload: NewMistakeApiModel = {
      name: mistake.name,
      goal: !mistake.goal ? null : mistake.goal,
      tips: mistake.tips.map((t: NewMistakeTip) => t.value).filter((t: string) => !!t),
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
};
