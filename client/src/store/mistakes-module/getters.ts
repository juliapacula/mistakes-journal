import { MistakesState } from '@/store/mistakes-module/state';
import { State } from '@/store/state';
import { GetterTree } from 'vuex';

export const getters: GetterTree<MistakesState, State> = {
  areAnyFiltersApplied: (state: MistakesState) => state.mistakesFilters.labelId !== null,
};
