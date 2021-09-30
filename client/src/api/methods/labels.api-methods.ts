import { LabelApiModel } from '@/api/models/label.api-model';
import { NewLabelApiModel } from '@/api/models/new-label.api-model';
import { Label } from '@/model/label';
import {
  get,
  post,
  put,
  remove,
} from '@/utils/api.utils';

export class LabelsApiMethods {
  public static async addNew(label: NewLabelApiModel): Promise<Label> {
    const result: LabelApiModel = await post('/api/labels', label);

    return {
      id: result.id,
      name: result.name,
      color: result.color,
      mistakesCounter: result.mistakesCounter,
    };
  }

  public static async getAll(name = '', color = ''): Promise<Label[]> {
    const results: LabelApiModel[] = await post('/api/labels/search', { name, color });

    return results.map((l: LabelApiModel) => ({
      id: l.id,
      name: l.name,
      color: l.color,
      mistakesCounter: l.mistakesCounter,
    }));
  }

  public static async get(labelId: string): Promise<Label> {
    const result: LabelApiModel = await get(`/api/labels/${labelId}`);

    return {
      id: result.id,
      name: result.name,
      color: result.color,
      mistakesCounter: result.mistakesCounter,
    };
  }

  public static async delete(labelId: string): Promise<void> {
    await remove(`/api/labels/${labelId}`);
  }

  public static async update(label: LabelApiModel): Promise<Label> {
    const result: LabelApiModel = await put(`/api/labels/${label.id}`, label);

    return {
      id: result.id,
      name: result.name,
      color: result.color,
      mistakesCounter: result.mistakesCounter,
    };
  }
}
