import { LabelsState } from '@/store/labels-module/state';
import { MistakesState } from '@/store/mistakes-module/state';
import { UserState } from '@/store/user-module/state';
import { UiState } from './ui-state-module/state';

export interface State {
  uiState: UiState;
  mistakes: MistakesState;
  user: UserState;
  labels: LabelsState;
}
