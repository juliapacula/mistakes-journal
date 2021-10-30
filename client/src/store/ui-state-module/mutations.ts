import { MutationTree } from 'vuex';
import { UiState } from './state';

export enum UiStateMutations {
  ExpandSidebar = 'uiState/ExpandSidebar',
  CollapseSidebar = 'uiState/CollapseSidebar',
  AddErrorMessageKey = 'uiState/AddErrorMessageKey',
}

export const mutations: MutationTree<UiState> = {
  [UiStateMutations.ExpandSidebar](state: UiState): void {
    state.isSidebarVisible = true;
  },
  [UiStateMutations.CollapseSidebar](state: UiState): void {
    state.isSidebarVisible = false;
  },
  [UiStateMutations.AddErrorMessageKey](state: UiState, messageKey: string): void {
    state.errorMessageKeys = [messageKey, ...state.errorMessageKeys];
  },
};
