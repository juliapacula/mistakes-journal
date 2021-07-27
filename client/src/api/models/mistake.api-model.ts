import { MistakePriority } from '@/model/mistake-priority.enum';

export interface MistakeApiModel {
  id: string;
  name: string;
  goal: string | null;
  priority: MistakePriority;
  tips: string[];
}
