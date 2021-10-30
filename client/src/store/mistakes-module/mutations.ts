import { PAGINATION_MAX_RESULTS_STEP } from '@/config/pagination.config';
import { Mistake } from '@/model/mistake';
import { Pagination } from '@/model/pagination';
import { MutationTree } from 'vuex';
import { MistakesState } from './state';

export enum MistakesMutations {
  AddMistake = 'mistakes/AddMistake',
  SetMistakes = 'mistakes/SetMistakes',
  SetMistake = 'mistakes/SetMistake',
  DeleteMistake = 'mistakes/DeleteMistake',
  SetMistakeFilters = 'mistakes/SetMistakeFilters',
}

export const mutations: MutationTree<MistakesState> = {
  [MistakesMutations.AddMistake](state: MistakesState, mistake: Mistake): void {
    state.mistakes = [...state.mistakes, mistake];
  },
  [MistakesMutations.SetMistakes](
    state: MistakesState,
    { items, totalCount }: { items: Mistake[], totalCount: number },
  ): void {
    state.mistakes = [...items];
    state.mistakesTotalCount = totalCount;
  },
  [MistakesMutations.SetMistake](state: MistakesState, mistake: Mistake): void {
    state.mistake = { ...mistake };
  },
  [MistakesMutations.DeleteMistake](state: MistakesState, mistakeId: string): void {
    state.mistakes = state.mistakes.filter((m) => m.id !== mistakeId);
  },
  [MistakesMutations.SetMistakeFilters](
    state: MistakesState,
    filters: { labelId?: string, pagination?: Pagination, includeSolved?: boolean, includeUnsolved?: boolean },
  ): void {
    state.mistakesFilters = {
      ...state.mistakesFilters,
      pagination: { startAt: 0, maxResults: PAGINATION_MAX_RESULTS_STEP },
      ...filters,
    };
  },
};
