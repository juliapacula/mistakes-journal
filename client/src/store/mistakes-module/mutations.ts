import { Mistake } from '@/model/mistake';
import { MutationTree } from 'vuex';
import { MistakesState } from './state';

export enum MistakesMutations {
  AddMistake = 'mistakes/AddMistake',
  SetMistakes = 'mistakes/SetMistakes',
  SetMistake = 'mistakes/SetMistake',
}

export const mutations: MutationTree<MistakesState> = {
  [MistakesMutations.AddMistake](state: MistakesState, mistake: Mistake): void {
    state.mistakes = [...state.mistakes, mistake];
  },
  [MistakesMutations.SetMistakes](state: MistakesState, mistakes: Mistake[]): void {
    state.mistakes = [...mistakes];
  },
  [MistakesMutations.SetMistake](state: MistakesState, mistake: Mistake): void {
    state.mistake = { ...mistake };
  },
};
