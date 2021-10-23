import { MistakePriority } from '@/model/mistake-priority.enum';
import { Moment } from 'moment';

export interface Mistake {
  id: string;
  createdAt: Moment;
  name: string;
  mistakeAdditionalQuestions: {
    consequences: string | null;
    whatCanIDoBetter: string | null;
    whatDidILearn: string | null;
    canIFixIt: string | null;
    onlyResponsible: string | null;
  } | null;
  goal: string | null;
  tips: string[];
  repetitionDates: Moment[];
  priority: MistakePriority;
}
