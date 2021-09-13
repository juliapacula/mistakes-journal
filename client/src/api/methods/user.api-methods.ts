import { ConfigurationApiModel } from '@/api/models/configuration.api-model';
import { UserApiModel } from '@/api/models/user.api-model';
import { Configuration } from '@/model/configuration';
import { User } from '@/model/user';
import { get } from '@/utils/api.utils';

export class UserApiMethods {
  public static async get(): Promise<User> {
    const user: UserApiModel = await get('/api/self');

    return {
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
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
}
