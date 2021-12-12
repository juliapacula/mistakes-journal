<template>
  <div class="mj-landing-footer">
    <div class="mj-landing-footer-logo-socialmedia">
      <application-title
        :in-nav-bar="true"
        :with-icon="true" />
      <footer-social-media />
    </div>
    <div>
      <footer-mail />
    </div>
  </div>
</template>

<script lang="ts">
import FooterMail from '@/components/footer/FooterMail.vue';
import FooterSocialMedia from '@/components/footer/FooterSocialMedia.vue';
import ApplicationTitle from '@/components/navigation-bar/ApplicationTitle.vue';
import Vue from 'vue';

export default Vue.extend({
  name: 'LandingFooter',
  components: {
    FooterMail,
    FooterSocialMedia,
    ApplicationTitle,
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-landing-footer {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: start;
  padding-left: 1rem;
  background-color: mistakes-journal.color('secondary', '50');
  box-shadow: mistakes-journal.$box-shadow-sm;

  @include mistakes-journal.media-breakpoint-up(sm) {
    flex-wrap: nowrap;
    align-items: center;
    justify-content: space-between;
  }

  @include mistakes-journal.media-breakpoint-up(lg) {
    padding: 0.5rem 3rem;
  }

  &-logo-socialmedia {
    display: flex;
    align-items: center;
    justify-content: start;
  }
}
</style>
