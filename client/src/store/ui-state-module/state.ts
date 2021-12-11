import { Locale } from '@/i18n/locales';
import { OnBoardingTourSteps } from '@/model/on-boarding-tour-steps.enum';

export interface UiState {
  isSidebarVisible: boolean;
  errorMessageKeys: string[];
  saturation: number;
  fontSize: number;
  currentOnBoardingTourStep: OnBoardingTourSteps;
  language: Locale;
}
