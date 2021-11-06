<template>
  <div
    v-if="mistake"
    class="mj-solution-section">
    <div class="mj-solution-section-title">
      {{ $t('Solutions.Name') }}
    </div>
    <div class="mj-solution-section-items-content">
      <div class="mj-solution-section-items-icon">
        <mj-icon
          name="ice-cream-name-mistake" />
      </div>
      <div class="mj-solution-section-items-text">
        {{ mistake.name }}
      </div>
    </div>
    <div v-if="mistake.mistakeAdditionalQuestions">
      <div
        v-if="mistake.mistakeAdditionalQuestions.consequences"
        class="mj-solution-section-title">
        {{ $t('MistakeForm.Consequences') }}
      </div>
      <div
        v-if="mistake.mistakeAdditionalQuestions.consequences"
        class="mj-solution-section-items-content">
        <div class="mj-solution-section-items-icon">
          <remix-icon
            icon="meteor" />
        </div>
        <div class="mj-solution-section-items-text">
          {{ mistake.mistakeAdditionalQuestions.consequences }}
        </div>
      </div>
      <div
        v-if="mistake.mistakeAdditionalQuestions.whatCanIDoBetter"
        class="mj-solution-section-title">
        {{ $t('MistakeForm.WhatCanIDoBetter') }}
      </div>
      <div
        v-if="mistake.mistakeAdditionalQuestions.whatCanIDoBetter"
        class="mj-solution-section-items-content">
        <div class="mj-solution-section-items-icon">
          <remix-icon
            icon="rocket-2" />
        </div>
        <div class="mj-solution-section-items-text">
          {{ mistake.mistakeAdditionalQuestions.whatCanIDoBetter }}
        </div>
      </div>
      <div
        v-if="mistake.mistakeAdditionalQuestions.whatDidILearn"
        class="mj-solution-section-title">
        {{ $t('MistakeForm.WhatDidILearn') }}
      </div>
      <div
        v-if="mistake.mistakeAdditionalQuestions.whatDidILearn"
        class="mj-solution-section-items-content">
        <div class="mj-solution-section-items-icon">
          <remix-icon
            icon="psychotherapy" />
        </div>
        <div class="mj-solution-section-items-text">
          {{ mistake.mistakeAdditionalQuestions.whatDidILearn }}
        </div>
      </div>
      <div
        v-if="mistake.mistakeAdditionalQuestions.canIFixIt"
        class="mj-solution-section-title">
        {{ $t('MistakeForm.CanIFixIt') }}
      </div>
      <div
        v-if="mistake.mistakeAdditionalQuestions.canIFixIt"
        class="mj-solution-section-items-content">
        <div class="mj-solution-section-items-icon">
          <remix-icon
            icon="tools" />
        </div>
        <div class="mj-solution-section-items-text">
          {{ mistake.mistakeAdditionalQuestions.canIFixIt }}
        </div>
      </div>
      <div
        v-if="mistake.mistakeAdditionalQuestions.onlyResponsible"
        class="mj-solution-section-title">
        {{ $t('MistakeForm.OnlyResponsible') }}
      </div>
      <div
        v-if="mistake.mistakeAdditionalQuestions.onlyResponsible"
        class="mj-solution-section-items-content">
        <div class="mj-solution-section-items-icon">
          <remix-icon
            icon="user-search" />
        </div>
        <div class="mj-solution-section-items-text">
          {{ mistake.mistakeAdditionalQuestions.onlyResponsible }}
        </div>
      </div>
    </div>
    <div class="mj-solution-section-title">
      {{ $t('Solutions.Goal') }}
    </div>
    <div
      v-if="mistake.goal"
      class="mj-solution-section-items-content">
      <div class="mj-solution-section-items-icon">
        <remix-icon
          icon="focus-3" />
      </div>
      <div class="mj-solution-section-items-text">
        {{ mistake.goal }}
      </div>
    </div>
    <div
      v-else
      class="mj-solution-section-items-content">
      <div class="mj-solution-section-items-text">
        {{ $t('Solutions.NoGoal') }}
      </div>
    </div>
    <div class="mj-solution-section-title">
      {{ $t('Solutions.Tips') }}
    </div>
    <div v-if="mistake.tips && mistake.tips.length > 0">
      <div
        v-for="tip in mistake.tips"
        :key="tip"
        class="mj-solution-section-items-content">
        <div class="mj-solution-section-items-icon">
          <remix-icon
            icon="lightbulb" />
        </div>
        <div class="mj-solution-section-items-text">
          {{ tip }}
        </div>
      </div>
    </div>
    <div
      v-else
      class="mj-solution-section-items-content">
      <div class="mj-solution-section-items-text">
        {{ $t('Solutions.NoTips') }}
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Mistake } from '@/model/mistake';
import { MistakesActions } from '@/store/mistakes-module/actions';
import { MistakesMutations } from '@/store/mistakes-module/mutations';
import Vue from 'vue';

export default Vue.extend({
  name: 'MistakeSolutionPage',
  computed: {
    mistake(): Mistake {
      return this.$store.state.mistakes.mistake ? this.$store.state.mistakes.mistake : null;
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

<style
  lang="scss"
  scoped>
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

  &-items-icon {
    $size-width: 1.5rem;
    display: flex;
    justify-content: center;
    width: $size-width;
    font-size: $size-width;
  }

  &-items-text {
    padding-left: 1rem;
    word-wrap: anywhere;
  }
}
</style>
