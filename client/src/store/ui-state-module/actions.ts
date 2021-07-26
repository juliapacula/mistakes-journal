import { State } from '@/store/state';
import { UiStateMutations } from '@/store/ui-state-module/mutations';
import { UiState } from '@/store/ui-state-module/state';
import {
  ActionContext,
  ActionTree,
} from 'vuex';

type Context = ActionContext<UiState, State>;

export enum UiStateActions {
  ToggleSidebar = 'uiState/ToggleSidebar',
}

export const actions: ActionTree<UiState, State> = {
  [UiStateActions.ToggleSidebar]({ commit, state }: Context): void {
    if (state.isSidebarVisible) {
      commit(UiStateMutations.CollapseSidebar);
    } else {
      commit(UiStateMutations.ExpandSidebar);
    }
  },
};
