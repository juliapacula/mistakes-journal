import LandingPage from '@/views/LandingPage.vue';
import MainPage from '@/views/MainPage.vue';
import MistakesPage from '@/views/MistakesPage.vue';
import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';

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
    children: [
      {
        path: '',
        redirect: 'mistakes',
      },
      {
        path: 'mistakes',
        name: 'MistakesPage',
        component: MistakesPage,
      },
    ],
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
