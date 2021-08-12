import { UiState } from './ui-state-module/state';
import { MistakesState } from '@/store/mistakes-module/state';

export interface State {
  uiState: UiState;
  mistakes: MistakesState;
}
