import { MistakePriority } from '@/model/mistake-priority.enum';

export interface NewMistake {
  name: string;
  goal: string;
  tips: string[];
  priority: MistakePriority;
}
