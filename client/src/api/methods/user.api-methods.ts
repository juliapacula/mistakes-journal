import { ConfigurationApiModel } from '@/api/models/configuration.api-model';
import { UserLocationDataApiModel } from '@/api/models/user-location-data.api-model';
import { UserApiModel } from '@/api/models/user.api-model';
import { Locale } from '@/i18n/locales';
import { Configuration } from '@/model/configuration';
import { User } from '@/model/user';
import { UserLocationData } from '@/model/user-location-data';
import {
  get,
  put,
} from '@/utils/api.utils';

export class UserApiMethods {
  public static async get(): Promise<User> {
    const user: UserApiModel = await get('/api/self');

    return {
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      language: user.language,
      group: user.group,
    };
  }

  public static async getConfiguration(): Promise<Configuration> {
    const config: ConfigurationApiModel = await get('/api/configuration');

    return {
      deniedPath: config.deniedPath,
      loginPath: config.loginPath,
      logoutPath: config.logoutPath,
    };
  }

  public static async changeLanguage(language: Locale): Promise<void> {
    return put('/api/self/language', { language });
  }

  public static async getLocationData(
    latitude: string,
    longitude: string,
  ): Promise<UserLocationData> {
    const data: UserLocationDataApiModel = await get(`/api/user-location-data?lat=${latitude}&lon=${longitude}`);

    return {
      city: data.city,
      weatherHopelessnessScale: data.weatherHopelessnessScale,
      timeOfDay: data.timeOfDay,
    };
  }
}
