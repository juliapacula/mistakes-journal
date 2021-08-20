import { MistakeApiModel } from '@/api/models/mistake.api-model';
import { NewMistakeApiModel } from '@/api/models/new-mistake.api-model';
import { Mistake } from '@/model/mistake';
import {
  get,
  post,
} from '@/utils/api.utils';

export class MistakesApiMethods {
  public static async addNew(mistake: NewMistakeApiModel): Promise<Mistake> {
    const result: MistakeApiModel = await post('/api/mistakes', mistake);

    return {
      id: result.id,
      goal: result.goal,
      name: result.name,
      priority: result.priority,
      tips: result.tips,
    };
  }

  public static async getAll(): Promise<Mistake[]> {
    const results: MistakeApiModel[] = await get('/api/mistakes');

    return results.map((m: MistakeApiModel) => ({
      id: m.id,
      goal: m.goal,
      name: m.name,
      priority: m.priority,
      tips: m.tips,
    }));
  }

  public static async get(mistakeId: string): Promise<Mistake> {
    const m: MistakeApiModel = await get(`/api/mistakes/${mistakeId}`);

    return {
      id: m.id,
      goal: m.goal,
      name: m.name,
      priority: m.priority,
      tips: m.tips,
    };
  }
}
