import { MistakeApiModel } from './mistake.api-model';
import { PaginatedListApiModel } from './paginated-list.api-model';

export interface MistakesListApiModel extends PaginatedListApiModel<MistakeApiModel> {}
