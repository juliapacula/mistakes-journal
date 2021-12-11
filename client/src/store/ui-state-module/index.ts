import {
  DEFAULT_SATURATION,
  MIN_FONT_SIZE,
} from '@/config/weather.config';
import { Locale } from '@/i18n/locales';
import { OnBoardingTourSteps } from '@/model/on-boarding-tour-steps.enum';
import { Module } from 'vuex';
import { State } from '../state';
import { actions } from './actions';
import { mutations } from './mutations';
import { UiState } from './state';

export default {
  actions,
  mutations,
  state: {
    isSidebarVisible: true,
    errorMessageKeys: [],
    saturation: DEFAULT_SATURATION,
    fontSize: MIN_FONT_SIZE,
    currentOnBoardingTourStep: OnBoardingTourSteps.MistakesList,
    language: Locale.EN,
  } as UiState,
} as Module<UiState, State>;
