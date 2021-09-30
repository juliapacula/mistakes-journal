import { Module } from 'vuex';
import { State } from '../state';
import { actions } from './actions';
import { mutations } from './mutations';
import { LabelsState } from './state';

export default {
  actions,
  mutations,
  state: {
    labels: [],
    label: null,
  } as LabelsState,
} as Module<LabelsState, State>;
