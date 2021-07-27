import { Module } from 'vuex';
import { State } from '../state';
import { actions } from './actions';
import { mutations } from './mutations';
import { MistakesState } from './state';

export default {
  actions,
  mutations,
  state: {
    mistakes: [],
  } as MistakesState,
} as Module<MistakesState, State>;
