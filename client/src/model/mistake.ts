import { MistakePriority } from '@/model/mistake-priority.enum';
import { Moment } from 'moment';

export interface Mistake {
  id: string;
  createdAt: Moment;
  name: string;
  goal: string | null;
  tips: string[];
  repetitionDates: Moment[];
  priority: MistakePriority;
}
