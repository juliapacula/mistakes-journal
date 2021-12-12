<template>
  <div class="mj-social-media">
    <!-- TODO change links to proper social media accouts -->
    <a href="https://facebook.com">
      <remix-icon
        class="mj-social-media-icon ri-facebook-circle-fill" />
    </a>
    <a href="https://instagram.com">
      <remix-icon
        class="mj-social-media-icon ri-instagram-fill" />
    </a>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'FooterSocialMedia',
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-social-media {
  display: flex;
  margin-left: 1rem;

  > * {
    margin-right: 1rem;
  }

  &-icon {
    @include mistakes-journal.font-regular(1.5rem);
    color: mistakes-journal.color('primary', '700');
  }
}

a:link,
a:visited,
a:hover,
a:active {
  text-decoration: none;
}
</style>
