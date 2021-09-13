import MistakesList from '@/components/MistakesList.vue';
import NewMistake from '@/components/NewMistake.vue';
import LandingPage from '@/views/LandingPage.vue';
import MainPage from '@/views/MainPage.vue';
import MistakeSolutionPage from '@/views/MistakeSolutionPage.vue';
import MistakesPage from '@/views/MistakesPage.vue';
import Vue from 'vue';
import VueRouter, {
  NavigationGuardNext,
  Route,
  RouteConfig,
  RouteRecord,
} from 'vue-router';
import store from '../store';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'LandingPage',
    component: LandingPage,
  },
  {
    path: '/journal',
    component: MainPage,
    meta: {
      requiresLogin: true,
    },
    children: [
      {
        path: '',
        redirect: 'mistakes',
      },
      {
        path: 'mistakes',
        component: MistakesPage,
        children: [
          {
            path: '',
            name: 'MistakesPage',
            component: MistakesList,
          },
          {
            path: 'new',
            component: NewMistake,
          },
          {
            path: ':id',
            name: 'MistakeSolutions',
            components: {
              default: MistakesList,
              solutions: MistakeSolutionPage,
            },
          },
        ],
      },
    ],
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to: Route, from: Route, next: NavigationGuardNext) => {
  const isLoggedIn = store.state.user.user !== null;
  const loginUrl = store.state.user.configuration?.loginPath.substr(1) ?? null;
  const requiresLogin = to.matched.some((record: RouteRecord) => record.meta.requiresLogin);

  if (requiresLogin && !isLoggedIn && loginUrl) {
    window.location.href = process.env.NODE_ENV === 'development' ? `http://localhost:5001/${loginUrl}` : loginUrl;
  } else if (requiresLogin && !isLoggedIn) {
    next('/');
  } else {
    next();
  }
});

export default router;
