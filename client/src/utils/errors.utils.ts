import { UiStateMutations } from '@/store/ui-state-module/mutations';
import { Commit } from 'vuex';

export const handleDefaultResponseErrors = async (
  commit: Commit,
  response: Response,
): Promise<void> => {
  if (response.status === 404) {
    commit(UiStateMutations.AddErrorMessageKey, 'ServerErrors.NotFound');
  } else if (response.status === 401) {
    commit(UiStateMutations.AddErrorMessageKey, 'ServerErrors.Unauthorized');
  } else if (response.status === 403) {
    commit(UiStateMutations.AddErrorMessageKey, 'ServerErrors.AccessDenied');
  } else if (response.status === 400) {
    const responseBody: { errors: { [key: string]: string[] } } = await response.json();

    if (responseBody.errors) {
      Object.keys(responseBody.errors)
        .flatMap((key: string) => responseBody.errors[key].map((error: string) => `ServerErrors.${error}`))
        .forEach((errorKey: string) => commit(UiStateMutations.AddErrorMessageKey, errorKey));
    } else {
      commit(UiStateMutations.AddErrorMessageKey, `ServerErrors.${responseBody as unknown as string}`);
    }
  } else if (response.status === 500) {
    commit(UiStateMutations.AddErrorMessageKey, 'ServerErrors.UnknownError');
  }
};
