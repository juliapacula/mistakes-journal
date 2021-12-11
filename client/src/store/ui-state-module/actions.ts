import {
  BEST_WEATHER,
  DEFAULT_SATURATION,
  FONT_SIZE_STEP,
  MIN_FONT_SIZE,
  MIN_SATURATION,
  WORST_WEATHER,
} from '@/config/weather.config';
import { OnBoardingTourSteps } from '@/model/on-boarding-tour-steps.enum';
import { TimeOfDay } from '@/model/time-of-day.enum';
import { State } from '@/store/state';
import { UiStateMutations } from '@/store/ui-state-module/mutations';
import { UiState } from '@/store/ui-state-module/state';
import {
  ActionContext,
  ActionTree,
} from 'vuex';

type Context = ActionContext<UiState, State>;

export enum UiStateActions {
  ToggleSidebar = 'uiState/ToggleSidebar',
  ChangeSaturationToPlain = 'uiState/ChangeSaturationToPlain',
  ChangeSaturationBasedOnWeather = 'uiState/ChangeSaturationBasedOnWeather',
  ChangeSizeBasedOnDayTime = 'uiState/ChangeSizeBasedOnDayTime',
  NextOnBoardingTourStep = 'uiState/NextOnBoardingTourStep',
  ResetUserOnBoardingTour = 'uiState/ResetUserOnBoardingTour',
}

export const actions: ActionTree<UiState, State> = {
  [UiStateActions.ToggleSidebar]({ commit, state }: Context): void {
    if (state.isSidebarVisible) {
      commit(UiStateMutations.CollapseSidebar);
    } else {
      commit(UiStateMutations.ExpandSidebar);
    }
  },
  [UiStateActions.ChangeSaturationToPlain]({ commit }: Context): void {
    commit(UiStateMutations.SetSaturation, MIN_SATURATION);
  },
  [UiStateActions.ChangeSaturationBasedOnWeather]({ commit }: Context, weather: number): void {
    const saturation = MIN_SATURATION + (DEFAULT_SATURATION - MIN_SATURATION) * ((WORST_WEATHER - weather) / (WORST_WEATHER - BEST_WEATHER));
    commit(UiStateMutations.SetSaturation, saturation);
  },
  [UiStateActions.ChangeSizeBasedOnDayTime]({ commit }: Context, daytime: TimeOfDay): void {
    let fontSize: number;

    switch (daytime) {
      case TimeOfDay.DayMorning:
        fontSize = MIN_FONT_SIZE + FONT_SIZE_STEP;
        break;
      case TimeOfDay.DayNoon:
        fontSize = MIN_FONT_SIZE;
        break;
      case TimeOfDay.DayEvening:
        fontSize = MIN_FONT_SIZE + FONT_SIZE_STEP;
        break;
      case TimeOfDay.NightEvening:
        fontSize = MIN_FONT_SIZE + FONT_SIZE_STEP * 2;
        break;
      case TimeOfDay.NightMidnight:
        fontSize = MIN_FONT_SIZE + FONT_SIZE_STEP * 3;
        break;
      case TimeOfDay.NightMorning:
        fontSize = MIN_FONT_SIZE + FONT_SIZE_STEP * 2;
        break;
      default:
        fontSize = MIN_FONT_SIZE;
        break;
    }

    commit(UiStateMutations.SetFontSize, fontSize);
  },
  [UiStateActions.NextOnBoardingTourStep]({ commit, state }: Context): void {
    switch (state.currentOnBoardingTourStep) {
      case OnBoardingTourSteps.MistakesList:
        commit(UiStateMutations.SetOnBoardingTourStep, OnBoardingTourSteps.AddingMistake);
        break;
      case OnBoardingTourSteps.AddingMistake:
        commit(UiStateMutations.SetOnBoardingTourStep, OnBoardingTourSteps.AddingRepetition);
        break;
      case OnBoardingTourSteps.AddingRepetition:
        commit(UiStateMutations.SetOnBoardingTourStep, OnBoardingTourSteps.Finshed);
        break;
      default:
        break;
    }
  },
  [UiStateActions.ResetUserOnBoardingTour]({ commit }: Context): void {
    commit(UiStateMutations.SetOnBoardingTourStep, OnBoardingTourSteps.MistakesList);
  },
};
