<template>
  <div class="mj-mail">
    <remix-icon
      class="mj-mail-icon ri-mail-line" />
    <span
      class="mj-mail-text">
      {{ $t('Application.Mail') }}
    </span>
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
}
</style>
