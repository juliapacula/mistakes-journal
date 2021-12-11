import { PAGINATION_MAX_RESULTS_STEP } from '@/config/pagination.config';
import { Module } from 'vuex';
import { State } from '../state';
import { actions } from './actions';
import { getters } from './getters';
import { mutations } from './mutations';
import { MistakesState } from './state';

export default {
  actions,
  mutations,
  getters,
  state: {
    mistakes: [],
    mistakesTotalCount: 0,
    mistake: null,
    mistakesFilters: {
      labelId: null,
      pagination: {
        startAt: 0,
        maxResults: PAGINATION_MAX_RESULTS_STEP,
      },
      includeUnsolved: true,
      includeSolved: true,
    },
  } as MistakesState,
} as Module<MistakesState, State>;
