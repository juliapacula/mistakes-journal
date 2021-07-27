import { MistakePriority } from '@/model/mistake-priority.enum';

export interface Mistake {
  id: string;
  name: string;
  goal: string | null;
  tips: string[];
  priority: MistakePriority;
}
