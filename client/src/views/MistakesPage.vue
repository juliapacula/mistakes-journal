<template>
  <div class="container-fluid h-100">
    <div class="row position-relative h-100">
      <div
        :class="{ 'col-lg-6': areSolutionsVisible }"
        class="mistakes-panel col-12">
        <section-header name="booklet">
          {{ $t('Mistakes.Title') }}
        </section-header>
        <router-view class="flex-grow-1" />
      </div>
      <div
        v-if="areSolutionsVisible"
        class="solutions-panel col-7 col-lg-6">
        <section-header name="lightbulb">
          {{ $t('Solutions.Title') }}
          <template v-slot:actions>
            <router-link
              class="btn with-icon mj-solution-close"
              role="button"
              tag="a"
              to="/journal/mistakes/">
              <fa-layers>
                <fa-icon
                  :icon="['fas', 'times']"
                  transform="grow-3" />
                <fa-icon
                  :icon="['far', 'circle']"
                  transform="grow-13" />
              </fa-layers>
            </router-link>
          </template>
        </section-header>
        <router-view name="solutions" />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import SectionHeader from '@/components/shared/SectionHeader.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakesPage',
  components: {
    SectionHeader,
  },
  computed: {
    areSolutionsVisible(): boolean {
      return this.$route.name === 'MistakeSolutions';
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../styles/mistakes-journal';

.mistakes-panel {
  display: flex;
  flex-direction: column;
}

.solutions-panel {
  position: absolute;
  right: 0;
  height: 100%;
  overflow-y: auto;
  border-left: 3px solid mistakes-journal.color('secondary', '900');
  background-color: mistakes-journal.color('secondary', '300');

  @include mistakes-journal.media-breakpoint-up(lg) {
    position: static;
    overflow-y: hidden;
    border-left: 0;
    background-color: transparent;
  }
}

.mj-add-mistake {
  .mj-icon {
    width: 1.5rem;
  }
}

.mj-solution-close {
  color: mistakes-journal.color('secondary', '700');

  &:hover {
    color: mistakes-journal.color('secondary', '900');
  }

  &:focus {
    color: mistakes-journal.color('secondary', '900');

  }

  &.active,
  &:active {
    color: mistakes-journal.color('secondary', '500');
  }
}
</style>
