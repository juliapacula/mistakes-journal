<template>
  <div class="mj-mail">
    <remix-icon class="mj-mail-icon ri-mail-line" />
    <a
      class="mj-mail-text"
      href="mailto:mistakesjournal@gmail.com">
      {{ $t('Application.Mail') }}
    </a>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'FooterMail',
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-mail {
  display: flex;
  align-items: center;
  margin-right: 1rem;
  padding-left: 0.5rem;

  &-icon {
    @include mistakes-journal.font-regular(1.5rem);
    margin-right: 0.5rem;
  }

  &-text {
    color: mistakes-journal.color('gray', '500');
    text-decoration: none;

    &:hover,
    &:focus {
      color: mistakes-journal.color('gray', '700');
    }
  }
}
</style>
