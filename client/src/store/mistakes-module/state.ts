import { Mistake } from '@/model/mistake';
import { Pagination } from '@/model/pagination';

export interface MistakesState {
  mistakes: Mistake[];
  mistakesTotalCount: number;
  mistake: Mistake | null;
  mistakesFilters: {
    labelId: string | null;
    pagination: Pagination;
    includeSolved: boolean;
    includeUnsolved: boolean;
  };
}
