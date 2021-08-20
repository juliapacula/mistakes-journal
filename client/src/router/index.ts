import MistakesList from '@/components/MistakesList.vue';
import NewMistake from '@/components/NewMistake.vue';
import LandingPage from '@/views/LandingPage.vue';
import MainPage from '@/views/MainPage.vue';
import MistakesPage from '@/views/MistakesPage.vue';
import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import SolutionPage from '@/views/SolutionPage.vue';

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
        children: [
          {
            path: '',
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
              solutions: SolutionPage,
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

export default router;
