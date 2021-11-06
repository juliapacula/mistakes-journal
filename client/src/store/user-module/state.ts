import { Configuration } from '@/model/configuration';
import { User } from '@/model/user';
import { UserLocationData } from '@/model/user-location-data';

export interface UserState {
  user: User | null;
  configuration: Configuration | null;
  location: { latitude: number, longitude: number } | null;
  canAccessLocation: boolean | null;
  locationData: UserLocationData | null;
  watchedTutorial: boolean | null;
}
