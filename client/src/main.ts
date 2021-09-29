import MJError from '@/components/shared/MJError.vue';
import MJIcon from '@/components/shared/MJIcon.vue';
import { library } from '@fortawesome/fontawesome-svg-core';
import { far } from '@fortawesome/free-regular-svg-icons';
import { fas } from '@fortawesome/free-solid-svg-icons';
import {
  FontAwesomeIcon,
  FontAwesomeLayers,
} from '@fortawesome/vue-fontawesome';
import VTooltip from 'v-tooltip';
import Vue from 'vue';
import Vuelidate from 'vuelidate';
import { DropdownPlugin } from 'bootstrap-vue';
import App from './App.vue';
import i18n from './i18n';
import router from './router';
import store from './store';

library.add(fas);
library.add(far);

Vue.component('fa-icon', FontAwesomeIcon);
Vue.component('fa-layers', FontAwesomeLayers);
Vue.component('mj-icon', MJIcon);
Vue.component('mj-error', MJError);

Vue.use(VTooltip, {
  defaultHtml: false,
  defaultPlacement: 'top',
});

Vue.use(Vuelidate);

Vue.use(DropdownPlugin);

Vue.config.productionTip = false;

new Vue({
  i18n,
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
