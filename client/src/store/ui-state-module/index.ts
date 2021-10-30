import { Module } from 'vuex';
import { State } from '../state';
import { actions } from './actions';
import { mutations } from './mutations';
import { UiState } from './state';

export default {
  actions,
  mutations,
  state: {
    isSidebarVisible: true,
    errorMessageKeys: [],
  } as UiState,
} as Module<UiState, State>;
