<template>
  <ul class="mj-sidebar-items">
    <li class="mj-sidebar-item">
      <remix-icon
        class="mj-sidebar-item-icon"
        icon="add-circle" />
      <button
        v-b-modal.label-modal
        class="btn btn-link"
        type="button">
        {{ $t('Sidebar.Labels.Create') }}
      </button>
    </li>
    <li
      v-for="label in labels"
      :key="label.id"
      :class="{ 'selected': label.id === mistakesListFilterLabelId }"
      class="mj-sidebar-item mj-label"
      @click="filterViaLabel(label.id)">
      <label-icon
        :color="label.color"
        class="mj-sidebar-item-icon" />
      <div class="mj-label-name">
        <span>{{ label.name }}</span>
        <span class="ms-auto">{{ label.mistakesCounter }}</span>
      </div>
      <button
        v-tooltip.right="$t('LabelOptions.Edit')"
        class="btn mj-label-action"
        type="button"
        @click.stop="editLabel(label.id)">
        <span class="btn-icon">
          <fa-icon :icon="['fas', 'pen']" />
        </span>
      </button>
      <button
        v-tooltip.right="$t('LabelOptions.Delete')"
        class="btn mj-label-action"
        type="button"
        @click.stop="deleteLabel(label.id, label.name)">
        <span class="btn-icon">
          <fa-icon :icon="['far', 'trash-alt']" />
        </span>
      </button>
    </li>
  </ul>
</template>

<script lang="ts">
import { Label } from '@/model/label';
import { LabelsActions } from '@/store/labels-module/actions';
import { MistakesActions } from '@/store/mistakes-module/actions';
import Vue from 'vue';

export default Vue.extend({
  name: 'SidebarLabelsList',
  data(): { isLabelModalOpened: boolean } {
    return {
      isLabelModalOpened: false,
    };
  },
  computed: {
    labels(): Label[] {
      return this.$store.state.labels.labels;
    },
    mistakesListFilterLabelId(): string | null {
      return this.$store.state.mistakes.mistakesFilters.labelId;
    },
  },
  async created() {
    await this.$store.dispatch(LabelsActions.GetAll);
  },
  methods: {
    async filterViaLabel(id: string): Promise<void> {
      if (this.mistakesListFilterLabelId === id) {
        await this.$store.dispatch(MistakesActions.UpdateMistakesFilters, { labelId: null });
      } else {
        await this.$store.dispatch(MistakesActions.UpdateMistakesFilters, { labelId: id });
      }
    },
    async editLabel(id: string): Promise<void> {
      await this.$store.dispatch(LabelsActions.Get, id);
      this.$bvModal.show('label-modal');
    },
    async deleteLabel(id: string, name: string): Promise<void> {
      const shouldDelete = await this.$bvModal.msgBoxConfirm(this.$t('DeleteLabel.Content', { name }) as string, {
        title: this.$t('DeleteLabel.Title') as string,
        okTitle: this.$t('DeleteLabel.Ok') as string,
        cancelTitle: this.$t('DeleteLabel.Cancel') as string,
        cancelVariant: 'info',
      });

      if (shouldDelete) {
        await this.$store.dispatch(LabelsActions.Delete, id);
      }
    },
  },
});
</script>

<style
  lang="scss"
  scoped>
@use '../../styles/mistakes-journal';

.mj-sidebar-item {
  .btn-link {
    padding: 0;
  }

  .btn {
    font-size: 1em;
  }
}

.mj-label {
  display: flex;
  cursor: pointer;
}

.selected {
  .mj-label-name {
    @include mistakes-journal.font-semi-bold;
  }
}

.mj-label-name {
  display: flex;
  flex: 1;
  align-items: center;
  overflow: hidden;

  span:first-of-type {
    display: inline-block;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }
}

.mj-label-action {
  padding: 0.2rem 0.4rem;
}
</style>
