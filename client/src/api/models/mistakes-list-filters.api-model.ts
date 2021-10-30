import { PaginationApiModel } from './pagination.api-model';

export interface MistakesListFiltersApiModel extends PaginationApiModel {
  labelId: string | null;
  includeSolved: boolean;
  includeUnsolved: boolean;
}
