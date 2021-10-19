import { MistakeApiModel } from '@/api/models/mistake.api-model';
import { NewMistakeApiModel } from '@/api/models/new-mistake.api-model';
import { RepetitionApiModel } from '@/api/models/repetition.api-model';
import { Mistake } from '@/model/mistake';
import {
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

  public static async getAll(): Promise<Mistake[]> {
    const results: MistakeApiModel[] = await get('/api/mistakes');

    return results.map((m: MistakeApiModel) => ({
      id: m.id,
      createdAt: convertFromTimestamp(m.createdAt),
      goal: m.goal,
      name: m.name,
      priority: m.priority,
      tips: m.tips,
      repetitionDates: m.repetitionDates.map((r) => convertFromTimestamp(r.occurredAt)),
    }));
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

  public static async updateMistake(mistakeId: string, mistake: NewMistakeApiModel): Promise<Mistake> {
    const result: MistakeApiModel = await put(`/api/mistakes/${mistakeId}`, mistake);

    return this.mapMistake(result);
  }

  private static mapMistake(mistake: MistakeApiModel): Mistake {
    return {
      id: mistake.id,
      createdAt: convertFromTimestamp(mistake.createdAt),
      goal: mistake.goal,
      name: mistake.name,
      priority: mistake.priority,
      tips: mistake.tips,
      repetitionDates: mistake.repetitionDates.map((r) => convertFromTimestamp(r.occurredAt)),
    };
  }
}
