import { LabelApiModel } from '@/api/models/label.api-model';
import { MistakeApiModel } from '@/api/models/mistake.api-model';
import { MistakesListFiltersApiModel } from '@/api/models/mistakes-list-filters.api-model';
import { MistakesListApiModel } from '@/api/models/mistakes-list.api-model';
import { NewMistakeApiModel } from '@/api/models/new-mistake.api-model';
import { RepetitionApiModel } from '@/api/models/repetition.api-model';
import { UpdatedMistakeApiModel } from '@/api/models/updated-mistake.api-model';
import { Mistake } from '@/model/mistake';
import { PaginatedList } from '@/model/paginated-list';
import {
  createParams,
  get,
  post,
  put,
  remove,
} from '@/utils/api.utils';
import { convertFromTimestamp } from '@/utils/date.utils';

export class MistakesApiMethods {
  public static async addNew(mistake: NewMistakeApiModel): Promise<Mistake> {
    const result: MistakeApiModel = await post('/api/mistakes', mistake);

    return this.mapMistake(result);
  }

  public static async getAll(filters: MistakesListFiltersApiModel): Promise<PaginatedList<Mistake>> {
    const queryParams = new URLSearchParams(createParams(filters));
    const {
      totalCount,
      items,
    }: MistakesListApiModel = await get(`/api/mistakes?${queryParams.toString()}`);

    return {
      totalCount,
      items: items.map((m: MistakeApiModel) => ({
        id: m.id,
        createdAt: convertFromTimestamp(m.createdAt),
        mistakeAdditionalQuestions: m.mistakeAdditionalQuestions,
        goal: m.goal,
        name: m.name,
        priority: m.priority,
        tips: m.tips,
        labels: m.labels.map((l: LabelApiModel) => ({
          id: l.id,
          name: l.name,
          color: l.color,
          mistakesCounter: l.mistakesCounter,
        })),
        repetitionDates: m.repetitionDates.map((r) => convertFromTimestamp(r.occurredAt)),
      })),
    };
  }

  public static async get(mistakeId: string): Promise<Mistake> {
    const result: MistakeApiModel = await get(`/api/mistakes/${mistakeId}`);

    return this.mapMistake(result);
  }

  public static async mistakeRepetition(repetition: RepetitionApiModel): Promise<Mistake> {
    const result: MistakeApiModel = await post(
      `/api/mistakes/${repetition.id}/add-repetition`,
      repetition.occurredAt,
    );

    return this.mapMistake(result);
  }

  public static async delete(mistakeId: string): Promise<void> {
    await remove(`/api/mistakes/${mistakeId}`);
  }

  public static async updateMistake(
    mistakeId: string,
    mistake: UpdatedMistakeApiModel,
  ): Promise<Mistake> {
    const result: MistakeApiModel = await put(`/api/mistakes/${mistakeId}`, mistake);

    return this.mapMistake(result);
  }

  private static mapMistake(mistake: MistakeApiModel): Mistake {
    return {
      id: mistake.id,
      createdAt: convertFromTimestamp(mistake.createdAt),
      mistakeAdditionalQuestions: mistake.mistakeAdditionalQuestions ? {
        consequences: mistake.mistakeAdditionalQuestions.consequences,
        whatCanIDoBetter: mistake.mistakeAdditionalQuestions.whatCanIDoBetter,
        whatDidILearn: mistake.mistakeAdditionalQuestions.whatDidILearn,
        canIFixIt: mistake.mistakeAdditionalQuestions.canIFixIt,
        onlyResponsible: mistake.mistakeAdditionalQuestions.onlyResponsible,
      } : null,
      goal: mistake.goal,
      name: mistake.name,
      priority: mistake.priority,
      tips: mistake.tips,
      labels: mistake.labels.map((l: LabelApiModel) => ({
        id: l.id,
        name: l.name,
        color: l.color,
        mistakesCounter: l.mistakesCounter,
      })),
      repetitionDates: mistake.repetitionDates.map((r) => convertFromTimestamp(r.occurredAt)),
    };
  }
}
