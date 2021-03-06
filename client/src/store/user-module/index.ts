import { Module } from 'vuex';
import { State } from '../state';
import { actions } from './actions';
import { getters } from './getters';
import { mutations } from './mutations';
import { UserState } from './state';

export default {
  actions,
  mutations,
  getters,
  state: {
    hasLoadedUser: false,
    user: null,
    configuration: null,
    location: null,
    canAccessLocation: null,
    locationData: null,
    watchedTutorial: null,
  } as UserState,
} as Module<UserState, State>;
