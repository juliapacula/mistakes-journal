import Vue from 'vue';
import Vuex from 'vuex';
import { State } from './state';
import uiState from './ui-state-module';

Vue.use(Vuex);

export default new Vuex.Store<State>({
  modules: {
    uiState,
  },
});
