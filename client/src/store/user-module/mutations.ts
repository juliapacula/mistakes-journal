import { Configuration } from '@/model/configuration';
import { User } from '@/model/user';
import { MutationTree } from 'vuex';
import { UserState } from './state';

export enum UserMutations {
  SetUser = 'user/SetUser',
  SetConfiguration = 'user/SetConfiguration',
}

export const mutations: MutationTree<UserState> = {
  [UserMutations.SetUser](state: UserState, user: User | null): void {
    state.user = user ? { ...user } : null;
  },
  [UserMutations.SetConfiguration](state: UserState, configuration: Configuration): void {
    state.configuration = { ...configuration };
  },
};
