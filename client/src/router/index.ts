import MistakeForm from '@/components/mistake/MistakeForm.vue';
import MistakesList from '@/components/mistakes/MistakesList.vue';
import { requireAuth } from '@/router/require-auth.guard';
import LandingPage from '@/views/LandingPage.vue';
import MainPage from '@/views/MainPage.vue';
import MistakeSolutionPage from '@/views/MistakeSolutionPage.vue';
import MistakesPage from '@/views/MistakesPage.vue';
import NotFoundPage from '@/views/NotFoundPage.vue';
import SolvedPage from '@/views/SolvedPage.vue';
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
    beforeEnter: requireAuth,
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
            name: 'NewMistake',
            component: MistakeForm,
          },
          {
            path: 'edit/:id',
            name: 'MistakeEdit',
            component: MistakeForm,
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
      {
        path: 'solved',
        name: 'SolvedMistakes',
        component: SolvedPage,
      },
    ],
  },
  {
    path: '*',
    name: 'NotFound',
    component: NotFoundPage,
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
