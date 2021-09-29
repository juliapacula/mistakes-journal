<template>
  <div class="mj-sidebar-container">
    <ul class="mj-sidebar-items">
      <router-link
        :is="item.isEnabled ? 'router-link' : 'li'"
        v-for="item in items"
        :key="item.translationKey"
        :class="{ 'disabled': !item.isEnabled }"
        :to="item.path"
        class="mj-sidebar-item"
        tag="li">
        <mj-icon
          :name="item.iconName"
          class="mj-sidebar-item-icon" />
        <a
          v-if="item.isEnabled"
          class="mj-sidebar-item-link">{{ $t(item.translationKey) }}</a>
        <span
          v-else
          class="mj-sidebar-item-link">{{ $t(item.translationKey) }}</span>
      </router-link>
    </ul>
    <application-title
      :with-icon="true"
      class="mj-sidebar-title" />
  </div>
</template>

<script lang="ts">
import ApplicationTitle from '@/components/navigation-bar/ApplicationTitle.vue';
import Vue from 'vue';

interface SidebarItem {
  translationKey: string;
  path: string;
  iconName: string;
  isEnabled: boolean;
}

export default Vue.extend({
  name: 'Sidebar',
  components: {
    ApplicationTitle,
  },
  data(): { items: SidebarItem[] } {
    return {
      items: [
        {
          translationKey: 'Sidebar.Links.Calendar',
          path: '/journal/calendar',
          isEnabled: false,
          iconName: 'calendar',
        },
        {
          translationKey: 'Sidebar.Links.Mistakes',
          path: '/journal/mistakes',
          isEnabled: true,
          iconName: 'mistake',
        },
        {
          translationKey: 'Sidebar.Links.Tips',
          path: '/journal/tips',
          isEnabled: false,
          iconName: 'tips',
        },
        {
          translationKey: 'Sidebar.Links.Groups',
          path: '/journal/tips',
          isEnabled: false,
          iconName: 'group',
        },
        {
          translationKey: 'Sidebar.Links.Labels',
          path: '/journal/tips',
          isEnabled: false,
          iconName: 'label',
        },
        {
          translationKey: 'Sidebar.Links.Solved',
          path: '/journal/tips',
          isEnabled: false,
          iconName: 'solved',
        },
      ] as SidebarItem[],
    };
  },
});
</script>

<style
  lang="scss"
  scoped>
@use 'sass:color';
@use '../../styles/mistakes-journal';

.mj-sidebar-container {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  height: 100%;
  padding: 0.5rem 0;
  background-color: mistakes-journal.color('secondary', '50');
}

.mj-sidebar-items {
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  margin: 0;
  padding: 0 0 0 1rem;
  list-style-type: none;
}

.mj-sidebar-item {
  display: flex;
  align-items: center;
  width: 100%;
  padding: 0.25rem 1rem;
  overflow-x: hidden;
  border-radius: 5rem 0 0 5rem;
  word-break: keep-all;
  white-space: nowrap;

  &-icon {
    margin: 0.25em 0.5rem 0.25rem 0.25rem;
    font-size: 1.5rem;
  }

  &-link {
    @include mistakes-journal.font-medium;
    color: mistakes-journal.color('text');
  }

  a {
    text-decoration: none;

    &:hover,
    &:focus,
    &:active {
      text-decoration: none;
    }
  }

  &.router-link-active {
    background-color: mistakes-journal.color('secondary', '500');

    a {
      @include mistakes-journal.font-semi-bold;
    }
  }

  &.disabled {
    .mj-sidebar-item-link,
    .mj-sidebar-item-icon {
      color: color.scale(mistakes-journal.color('text'), $lightness: 50%);
    }
  }
}
</style>
