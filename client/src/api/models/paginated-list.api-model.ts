export interface PaginatedListApiModel<T> {
  items: T[];
  totalCount: number;
}
