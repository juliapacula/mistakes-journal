import { UserApiMethods } from '@/api/methods/user.api-methods';
import { Locale } from '@/i18n/locales';
import { ResearchGroup } from '@/model/research-group.enum';
import { State } from '@/store/state';
import { UiStateActions } from '@/store/ui-state-module/actions';
import { UserMutations } from '@/store/user-module/mutations';
import { post } from '@/utils/api.utils';
import { handleDefaultResponseErrors } from '@/utils/errors.utils';
import {
  ActionContext,
  ActionTree,
} from 'vuex';
import { UserState } from './state';

type Context = ActionContext<UserState, State>;

export enum UserActions {
  Get = 'user/Get',
  GetConfiguration = 'user/GetConfiguration',
  ChangeLanguage = 'user/ChangeLanguage',
  WatchLocation = 'user/WatchLocation',
  UpdateWeatherInfo = 'user/UpdateWeatherInfo',
  Logout = 'user/Logout',
}

export const actions: ActionTree<UserState, State> = {
  async [UserActions.Get]({ commit, dispatch }: Context): Promise<void> {
    try {
      const user = await UserApiMethods.get();
      commit(UserMutations.SetUser, user);

      if (user.group === ResearchGroup.Third) {
        await dispatch(UiStateActions.ChangeSaturationToPlain);
      }
    } catch (response: unknown) {
      commit(UserMutations.SetUser, null);
    }
  },
  async [UserActions.GetConfiguration]({ commit }: Context): Promise<void> {
    try {
      const config = await UserApiMethods.getConfiguration();

      commit(UserMutations.SetConfiguration, config);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },
  async [UserActions.Logout]({ state, commit }: Context): Promise<void> {
    await post(state.configuration?.logoutPath ?? '', null);
    commit(UserMutations.SetUser, null);
  },
  async [UserActions.ChangeLanguage]({ commit, dispatch }: Context, locale: Locale): Promise<void> {
    try {
      await UserApiMethods.changeLanguage(locale);
      await dispatch(UserActions.Get);
    } catch (e) {
      await handleDefaultResponseErrors(commit, e as Response);
      throw e;
    }
  },
  async [UserActions.WatchLocation]({ commit, state, getters }: Context): Promise<void> {
    if (!getters.shouldUpdateLocation || state.canAccessLocation === false) {
      return;
    }

    const successfulLocationRetrieval = (position: GeolocationPosition): void => {
      const hasLocationChanged = position.coords.latitude !== state.location?.latitude || position.coords.longitude !== state.location?.longitude;

      if (!state.location || hasLocationChanged) {
        commit(
          UserMutations.SetLocation,
          { latitude: position.coords.latitude, longitude: position.coords.longitude },
        );
      }

      if (state.canAccessLocation !== true) {
        commit(UserMutations.SetLocationAccess, true);
      }
    };

    const unsuccessfulLocationRetrieval = () => {
      commit(UserMutations.SetLocationAccess, false);
    };

    navigator.geolocation.watchPosition(successfulLocationRetrieval, unsuccessfulLocationRetrieval);
  },
  async [UserActions.UpdateWeatherInfo]({
    commit,
    dispatch,
    state,
    getters,
  }: Context): Promise<void> {
    if (!getters.shouldUpdateLocation || !state.user || !state.location) {
      return;
    }

    try {
      const data = await UserApiMethods.getLocationData(
        state.location.latitude.toString(),
        state.location.longitude.toString(),
      );

      commit(UserMutations.SetLocationData, data);

      if (state.user.group === ResearchGroup.Second) {
        await dispatch(UiStateActions.ChangeSizeBasedOnDayTime, data.timeOfDay);
      } else if (state.user.group === ResearchGroup.First) {
        await dispatch(UiStateActions.ChangeSaturationBasedOnWeather, data.weatherHopelessnessScale);
      }
      // eslint-disable-next-line no-empty
    } catch (e) {}
  },
};
