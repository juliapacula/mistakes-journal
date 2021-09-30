import Vue from 'vue';
import Vuex from 'vuex';
import labels from './labels-module';
import mistakes from './mistakes-module';
import { State } from './state';
import uiState from './ui-state-module';
import user from './user-module';

Vue.use(Vuex);

export default new Vuex.Store<State>({
  modules: {
    mistakes,
    uiState,
    user,
    labels,
  },
});
