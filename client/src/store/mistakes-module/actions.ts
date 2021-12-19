import { MistakesApiMethods } from '@/api/methods/mistakes.api-methods';
import { NewMistakeApiModel } from '@/api/models/new-mistake.api-model';
import { RepetitionApiModel } from '@/api/models/repetition.api-model';
import { UpdatedMistakeApiModel } from '@/api/models/updated-mistake.api-model';
import { GOOGLE_EVENTS } from '@/config/google-analytics-events.config';
import { NewMistake } from '@/model/new-mistake';
import { Pagination } from '@/model/pagination';
import { MistakesMutations } from '@/store/mistakes-module/mutations';
import { MistakesState } from '@/store/mistakes-module/state';
import { State } from '@/store/state';
import { convertToTimestamp } from '@/utils/date.utils';
import { handleDefaultResponseErrors } from '@/utils/errors.utils';
import { Moment } from 'moment';
import { event } from 'vue-gtag';
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
    if (mistake.mistakeAdditionalQuestions
      && mistake.mistakeAdditionalQuestions.consequences !== null
      && mistake.mistakeAdditionalQuestions.onlyResponsible !== null
      && mistake.mistakeAdditionalQuestions.canIFixIt !== null
      && mistake.mistakeAdditionalQuestions.whatDidILearn !== null
      && mistake.mistakeAdditionalQuestions.whatCanIDoBetter !== null) {
      event(GOOGLE_EVENTS.NEW_MISTAKE_WITH_DEEP_ANALYZER);
    } else {
      event(GOOGLE_EVENTS.NEW_MISTAKE_WITHOUT_DEEP_ANALYZER);
    }

    const payload: NewMistakeApiModel = {
      name: mistake.name,
      consequences: mistake.mistakeAdditionalQuestions?.consequences ? mistake.mistakeAdditionalQuestions?.consequences : null,
      whatCanIDoBetter: mistake.mistakeAdditionalQuestions?.whatCanIDoBetter ? mistake.mistakeAdditionalQuestions?.whatCanIDoBetter : null,
      whatDidILearn: mistake.mistakeAdditionalQuestions?.whatDidILearn ? mistake.mistakeAdditionalQuestions?.whatDidILearn : null,
      canIFixIt: mistake.mistakeAdditionalQuestions?.canIFixIt ? mistake.mistakeAdditionalQuestions?.canIFixIt : null,
      onlyResponsible: mistake.mistakeAdditionalQuestions?.onlyResponsible ? mistake.mistakeAdditionalQuestions?.onlyResponsible : null,
      goal: !mistake.goal ? null : mistake.goal,
      tips: mistake.tips.filter((t: string) => !!t),
      labels: mistake.labelIds,
      priority: mistake.priority,
    };

    try {
      const addedMistake = await MistakesApiMethods.addNew(payload);

      commit(MistakesMutations.AddMistake, addedMistake);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },
  async [MistakesActions.GetAll]({ commit, state }: Context): Promise<void> {
    try {
      const { items, totalCount } = await MistakesApiMethods.getAll({
        includeSolved: state.mistakesFilters.includeSolved,
        includeUnsolved: state.mistakesFilters.includeUnsolved,
        labelId: state.mistakesFilters.labelId,
        startAt: state.mistakesFilters.pagination.startAt,
        maxResults: state.mistakesFilters.pagination.maxResults,
      });

      commit(MistakesMutations.SetMistakes, { items, totalCount });
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },

  async [MistakesActions.Get]({ commit }: Context, mistakeId: string): Promise<void> {
    try {
      const mistake = await MistakesApiMethods.get(mistakeId);

      commit(MistakesMutations.SetMistake, mistake);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },

  async [MistakesActions.Delete]({ commit }: Context, mistakeId: string): Promise<void> {
    try {
      await MistakesApiMethods.delete(mistakeId);

      commit(MistakesMutations.DeleteMistake, mistakeId);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
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
      labels: mistake.labelIds,
      priority: mistake.priority,
    };
    try {
      const updatedMistake = await MistakesApiMethods.updateMistake(mistakeId, payload);

      commit(MistakesMutations.SetMistake, updatedMistake);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },

  async [MistakesActions.MistakeRepetition](
    { commit, dispatch }: Context,
    { mistakeId, occurredAt }: { mistakeId: string, occurredAt?: Moment },
  ): Promise<void> {
    event(GOOGLE_EVENTS.NEW_REPETITION);

    const payload: RepetitionApiModel = {
      id: mistakeId,
      occurredAt: occurredAt ? convertToTimestamp(occurredAt) : null,
    };

    try {
      const updatedMistake = await MistakesApiMethods.mistakeRepetition(payload);

      commit(MistakesMutations.SetMistake, updatedMistake);
      await dispatch(MistakesActions.GetAll);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },

  async [MistakesActions.UpdateMistakesFilters](
    { commit, dispatch }: Context,
    filters: { labelId?: string, pagination?: Pagination, includeSolved?: boolean, includeUnsolved?: boolean },
  ): Promise<void> {
    commit(MistakesMutations.SetMistakeFilters, filters);
    await dispatch(MistakesActions.GetAll);
  },
};
