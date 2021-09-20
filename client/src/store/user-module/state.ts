import { Configuration } from '@/model/configuration';
import { User } from '@/model/user';

export interface UserState {
  user: User | null;
  configuration: Configuration | null;
}
