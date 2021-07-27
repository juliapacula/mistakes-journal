import { MistakePriority } from '@/model/mistake-priority.enum';

export interface NewMistakeTip {
  id: number;
  value: string;
}

export interface NewMistake {
  name: string;
  goal: string;
  tips: NewMistakeTip[];
  priority: MistakePriority;
}
