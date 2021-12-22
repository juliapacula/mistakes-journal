import { State } from '@/store/state';
import { UiStateMutations } from '@/store/ui-state-module/mutations';
import {
  NavigationGuardNext,
  Route,
} from 'vue-router';
import store from '../store';

export const requireAuth = (to: Route, from: Route, next: NavigationGuardNext): void => {
  const determineIfCanLoad = () => {
    if (store.state.user.hasLoadedUser) {
      if (store.state.user.user) {
        next();
      } else {
        next('/');
        store.commit(UiStateMutations.AddErrorMessageKey, 'ServerErrors.Unauthorized');
      }
    } else {
      store.watch((s: State) => s.user.hasLoadedUser, () => {
        determineIfCanLoad();
      });
    }
  };

  determineIfCanLoad();
};
