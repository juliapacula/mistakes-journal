import { ResearchGroup } from '@/model/research-group.enum';
import { State } from '@/store/state';
import { UserState } from '@/store/user-module/state';
import { GetterTree } from 'vuex';

export const getters: GetterTree<UserState, State> = {
  shouldUpdateLocation: (state: UserState) => state.user && (state.user.group === ResearchGroup.First || state.user.group === ResearchGroup.Second),
};
