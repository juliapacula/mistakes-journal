import { Module } from 'vuex';
import { State } from '../state';
import { actions } from './actions';
import { mutations } from './mutations';
import { UserState } from './state';

export default {
  actions,
  mutations,
  state: {
    user: null,
  } as UserState,
} as Module<UserState, State>;
