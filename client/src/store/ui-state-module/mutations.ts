import { Locale } from '@/i18n/locales';
import { OnBoardingTourSteps } from '@/model/on-boarding-tour-steps.enum';
import { MutationTree } from 'vuex';
import { UiState } from './state';

export enum UiStateMutations {
  ExpandSidebar = 'uiState/ExpandSidebar',
  CollapseSidebar = 'uiState/CollapseSidebar',
  AddErrorMessageKey = 'uiState/AddErrorMessageKey',
  SetSaturation = 'uiState/SetSaturation',
  SetFontSize = 'uiState/SetFontSize',
  SetOnBoardingTourStep = 'uiState/SetOnBoardingTourStep',
  SetLanguage = 'uiState/SetLanguage'
}

export const mutations: MutationTree<UiState> = {
  [UiStateMutations.ExpandSidebar](state: UiState): void {
    state.isSidebarVisible = true;
  },
  [UiStateMutations.CollapseSidebar](state: UiState): void {
    state.isSidebarVisible = false;
  },
  [UiStateMutations.AddErrorMessageKey](state: UiState, messageKey: string): void {
    state.errorMessageKeys = [messageKey, ...state.errorMessageKeys];
  },
  [UiStateMutations.SetSaturation](state: UiState, saturation: number): void {
    state.saturation = saturation;
  },
  [UiStateMutations.SetFontSize](state: UiState, fontSize: number): void {
    state.fontSize = fontSize;
  },
  [UiStateMutations.SetOnBoardingTourStep](state: UiState, tourStep: OnBoardingTourSteps): void {
    state.currentOnBoardingTourStep = tourStep;
  },
  [UiStateMutations.SetLanguage](state: UiState, language: Locale): void {
    state.language = language;
  },
};
