import { Configuration } from '@/model/configuration';
import { User } from '@/model/user';
import { UserLocationData } from '@/model/user-location-data';
import { MutationTree } from 'vuex';
import { UserState } from './state';

export enum UserMutations {
  SetUser = 'user/SetUser',
  SetConfiguration = 'user/SetConfiguration',
  SetLocation = 'user/SetLocation',
  SetLocationAccess = 'user/SetLocationAccess',
  SetLocationData = 'user/SetLocationData',
}

export const mutations: MutationTree<UserState> = {
  [UserMutations.SetUser](state: UserState, user: User | null): void {
    state.user = user ? { ...user } : null;
  },
  [UserMutations.SetConfiguration](state: UserState, configuration: Configuration): void {
    state.configuration = { ...configuration };
  },
  [UserMutations.SetLocation](state: UserState, location: { latitude: number, longitude: number } | null): void {
    state.location = location ? { ...location } : null;
  },
  [UserMutations.SetLocationAccess](state: UserState, canAccess: boolean | null): void {
    state.canAccessLocation = canAccess;
  },
  [UserMutations.SetLocationData](state: UserState, data: UserLocationData | null): void {
    state.locationData = data ? { ...data } : null;
  },
};
