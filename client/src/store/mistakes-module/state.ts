import { Mistake } from '@/model/mistake';

export interface MistakesState {
  mistakes: Mistake[];
  mistake: Mistake | null;
}
