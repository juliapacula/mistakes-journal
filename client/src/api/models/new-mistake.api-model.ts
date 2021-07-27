import { MistakePriority } from '@/model/mistake-priority.enum';

export interface NewMistakeApiModel {
  name: string;
  goal: string | null;
  priority: MistakePriority;
  tips: string[];
}
