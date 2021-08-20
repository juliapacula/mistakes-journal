<template>
  <div class="mj-solution-section">
    <div v-if="mistake">
      <div class="mj-solution-section-title">{{ $t('Solutions.Name') }}</div>
      <div class="mj-solution-section-items-content">
        <fa-icon :icon="['far', 'star']" transform="grow-5"/>
        <span class="mj-solution-section-items-text">
          {{ mistake.name }}
        </span>
      </div>
    </div>
    <div>
      <div class="mj-solution-section-title">{{ $t('Solutions.Goal') }}</div>
      <div v-if="mistake.goal" class="mj-solution-section-items-content">
        <fa-icon :icon="['far', 'dot-circle']" transform="grow-5"/>
        <span class="mj-solution-section-items-text">
          {{ mistake.goal }}
        </span>
      </div>
      <div v-else class="mj-solution-section-items-content">
        <span class="mj-solution-section-items-text">
          {{ $t('Solutions.NoGoal') }}
        </span>
      </div>
    </div>
    <div>
      <div class="mj-solution-section-title">{{ $t('Solutions.Tips') }}</div>
      <div v-if="mistake.tips.length > 0">
        <div
          v-for="tip in mistake.tips"
          :key="tip"
          class="mj-solution-section-items-content">
          <fa-icon :icon="['far', 'lightbulb']" transform="grow-5"/>
          <span class="mj-solution-section-items-text">
            {{ tip }}
          </span>
        </div>
      </div>
      <div v-else>
          <span class="mj-solution-section-items-text">
            {{ $t('Solutions.NoTips') }}
          </span>
      </div>
    </div>
  </div>

</template>

<script lang="ts">
import Vue from 'vue';
import { MistakesActions } from '@/store/mistakes-module/actions';
import { Mistake } from '@/model/mistake';
import { MistakesMutations } from '@/store/mistakes-module/mutations';
import SectionHeader from '@/components/SectionHeader.vue';

export default Vue.extend({
  name: 'SolutionPage',
  computed: {
    mistake(): Mistake {
      return this.$store.state.mistakes.mistake;
    },
  },
  watch: {
    $route: 'fetchMistake',
  },
  async created(): Promise<void> {
    await this.fetchMistake();
  },
  async destroyed(): Promise<void> {
    await this.$store.commit(MistakesMutations.SetMistake, null);
  },
  methods: {
    async fetchMistake(): Promise<void> {
      await this.$store.dispatch(MistakesActions.Get, this.$route.params.id);
    },
  },
});
</script>

<style scoped lang="scss">
@use '../styles/mistakes-journal';

.mj-solution-section {
  display: block;
  padding: 1rem;

  &-title {
    @include mistakes-journal.font-semi-bold;
    padding: 0.25rem;
  }

  &-items-content {
    display: flex;
    align-items: center;
    padding: 1rem;
  }

  &-items-text {
    padding-left: 1rem;
  }
}
</style>
