import { UserApiMethods } from '@/api/methods/user.api-methods';
import { State } from '@/store/state';
import { UserMutations } from '@/store/user-module/mutations';
import { post } from '@/utils/api.utils';
import {
  ActionContext,
  ActionTree,
} from 'vuex';
import { UserState } from './state';

type Context = ActionContext<UserState, State>;

export enum UserActions {
  Get = 'user/Get',
  GetConfiguration = 'user/GetConfiguration',
  Logout = 'user/Logout',
}

export const actions: ActionTree<UserState, State> = {
  async [UserActions.Get]({ commit }: Context): Promise<void> {
    try {
      const user = await UserApiMethods.get();
      commit(UserMutations.SetUser, user);
    } catch (response: unknown) {
      commit(UserMutations.SetUser, null);
    }
  },
  async [UserActions.GetConfiguration]({ commit }: Context): Promise<void> {
    const config = await UserApiMethods.getConfiguration();

    commit(UserMutations.SetConfiguration, config);
  },
  async [UserActions.Logout]({ state, commit }: Context): Promise<void> {
    await post(state.configuration?.logoutPath ?? '', null);
    commit(UserMutations.SetUser, null);
  },
};
