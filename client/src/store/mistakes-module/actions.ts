import { MistakesApiMethods } from '@/api/methods/mistakes.api-methods';
import { NewMistakeApiModel } from '@/api/models/new-mistake.api-model';
import { RepetitionApiModel } from '@/api/models/repetition.api-model';
import { UpdatedMistakeApiModel } from '@/api/models/updated-mistake.api-model';
import { NewMistake } from '@/model/new-mistake';
import { Pagination } from '@/model/pagination';
import { MistakesMutations } from '@/store/mistakes-module/mutations';
import { MistakesState } from '@/store/mistakes-module/state';
import { State } from '@/store/state';
import { convertToTimestamp } from '@/utils/date.utils';
import { Moment } from 'moment';
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
  UpdateMistakesFilters = 'mistakes/UpdateMistakesFilters',
  MistakeRepetition = 'mistakes/AddRepetitionResetCounter',
}

export const actions: ActionTree<MistakesState, State> = {
  async [MistakesActions.AddMistake]({ commit }: Context, mistake: NewMistake): Promise<void> {
    const payload: NewMistakeApiModel = {
      name: mistake.name,
      consequences: mistake.mistakeAdditionalQuestions?.consequences ? mistake.mistakeAdditionalQuestions?.consequences : null,
      whatCanIDoBetter: mistake.mistakeAdditionalQuestions?.whatCanIDoBetter ? mistake.mistakeAdditionalQuestions?.whatCanIDoBetter : null,
      whatDidILearn: mistake.mistakeAdditionalQuestions?.whatDidILearn ? mistake.mistakeAdditionalQuestions?.whatDidILearn : null,
      canIFixIt: mistake.mistakeAdditionalQuestions?.canIFixIt ? mistake.mistakeAdditionalQuestions?.canIFixIt : null,
      onlyResponsible: mistake.mistakeAdditionalQuestions?.onlyResponsible ? mistake.mistakeAdditionalQuestions?.onlyResponsible : null,
      goal: !mistake.goal ? null : mistake.goal,
      tips: mistake.tips.filter((t: string) => !!t),
      priority: mistake.priority,
    };

    const addedMistake = await MistakesApiMethods.addNew(payload);

    commit(MistakesMutations.AddMistake, addedMistake);
  },
  async [MistakesActions.GetAll]({ commit, state }: Context): Promise<void> {
    const { items, totalCount } = await MistakesApiMethods.getAll({
      includeSolved: state.mistakesFilters.includeSolved,
      includeUnsolved: state.mistakesFilters.includeUnsolved,
      labelId: state.mistakesFilters.labelId,
      startAt: state.mistakesFilters.pagination.startAt,
      maxResults: state.mistakesFilters.pagination.maxResults,
    });

    commit(MistakesMutations.SetMistakes, { items, totalCount });
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
    const payload: UpdatedMistakeApiModel = {
      name: mistake.name,
      consequences: mistake.mistakeAdditionalQuestions?.consequences ? mistake.mistakeAdditionalQuestions?.consequences : null,
      whatCanIDoBetter: mistake.mistakeAdditionalQuestions?.whatCanIDoBetter ? mistake.mistakeAdditionalQuestions?.whatCanIDoBetter : null,
      whatDidILearn: mistake.mistakeAdditionalQuestions?.whatDidILearn ? mistake.mistakeAdditionalQuestions?.whatDidILearn : null,
      canIFixIt: mistake.mistakeAdditionalQuestions?.canIFixIt ? mistake.mistakeAdditionalQuestions?.canIFixIt : null,
      onlyResponsible: mistake.mistakeAdditionalQuestions?.onlyResponsible ? mistake.mistakeAdditionalQuestions?.onlyResponsible : null,
      goal: !mistake.goal ? null : mistake.goal,
      tips: mistake.tips.filter((t: string) => !!t),
      priority: mistake.priority,
    };
    const updatedMistake = await MistakesApiMethods.updateMistake(mistakeId, payload);

    commit(MistakesMutations.SetMistake, updatedMistake);
  },

  async [MistakesActions.MistakeRepetition](
    { commit, dispatch }: Context,
    { mistakeId, occurredAt }: { mistakeId: string, occurredAt?: Moment },
  ): Promise<void> {
    const payload: RepetitionApiModel = {
      id: mistakeId,
      occurredAt: occurredAt ? convertToTimestamp(occurredAt) : null,
    };

    const updatedMistake = await MistakesApiMethods.mistakeRepetition(payload);

    commit(MistakesMutations.SetMistake, updatedMistake);
    await dispatch(MistakesActions.GetAll);
  },

  async [MistakesActions.UpdateMistakesFilters](
    { commit, dispatch }: Context,
    filters: { labelId?: string, pagination?: Pagination, includeSolved?: boolean, includeUnsolved?: boolean },
  ): Promise<void> {
    commit(MistakesMutations.SetMistakeFilters, filters);
    await dispatch(MistakesActions.GetAll);
  },
};
