<template>
  <div class="mj-sidebar-container">
    <ul class="mj-sidebar-items">
      <router-link
        class="mj-sidebar-item"
        tag="li"
        to="/journal/mistakes">
        <remix-icon
          class="mj-sidebar-item-icon"
          icon="booklet" />
        <a class="mj-sidebar-item-link">{{ $t('Sidebar.Links.Mistakes') }}</a>
      </router-link>
      <li class="mj-sidebar-item disabled">
        <remix-icon
          class="mj-sidebar-item-icon"
          icon="lightbulb" />
        <span class="mj-sidebar-item-link">{{ $t('Sidebar.Links.Tips') }}</span>
      </li>
      <li
        class="mj-sidebar-item clickable"
        role="option"
        @click="toggleLabels()">
        <remix-icon
          class="mj-sidebar-item-icon"
          icon="price-tag-3" />
        <span class="mj-sidebar-item-link">
          {{ $t('Sidebar.Links.Labels') }}
        </span>
        <button
          class="btn btn-icon mj-sidebar-item-toggle"
          type="button"
          @click.stop="toggleLabels()">
          <fa-icon :icon="['fa', areLabelsExpanded ? 'chevron-up' : 'chevron-down']" />
        </button>
      </li>
      <li
        v-if="areLabelsExpanded"
        class="mj-sidebar-sub-items">
        <sidebar-labels-list />
      </li>
      <li class="mj-sidebar-item disabled">
        <remix-icon
          class="mj-sidebar-item-icon"
          icon="vip-diamond" />
        <span class="mj-sidebar-item-link">{{ $t('Sidebar.Links.Solved') }}</span>
      </li>
    </ul>
    <application-title
      :with-icon="true"
      class="mj-sidebar-title" />
  </div>
</template>

<script lang="ts">
import SidebarLabelsList from '@/components/labels/SidebarLabelsList.vue';
import ApplicationTitle from '@/components/navigation-bar/ApplicationTitle.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'Sidebar',
  components: {
    SidebarLabelsList,
    ApplicationTitle,
  },
  data(): { areLabelsExpanded: boolean } {
    return {
      areLabelsExpanded: false,
    };
  },
  methods: {
    toggleLabels(): void {
      this.areLabelsExpanded = !this.areLabelsExpanded;
    },
  },
});
</script>

<style lang="scss">
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
  padding: 0.25em 1em;
  overflow-x: hidden;
  border-radius: 5em 0 0 5em;
  word-break: keep-all;
  white-space: nowrap;

  &.clickable {
    cursor: pointer;
  }

  &-icon {
    margin: 0 0.25em;
    font-size: 1.5em;
  }

  &-link {
    @include mistakes-journal.font-medium;
    flex: 1;
    color: mistakes-journal.color('text');

    &.btn {
      padding: 0;
    }
  }

  &-toggle {
    margin-left: auto;
  }

  a {
    text-decoration: none;

    &:hover,
    &:focus,
    &:active {
      text-decoration: none;
    }
  }

  &:not(.disabled) {
    &:hover {
      background-color: mistakes-journal.color('secondary', '500');
    }
  }

  &.router-link-active,
  &.selected {
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

.mj-sidebar-sub-items {
  width: 100%;
  padding: 0.25em 0;
  font-size: 0.8em;
}
</style>
