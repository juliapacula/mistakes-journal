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
    user: null,
    configuration: null,
    location: null,
    canAccessLocation: null,
    locationData: null,
  } as UserState,
} as Module<UserState, State>;
