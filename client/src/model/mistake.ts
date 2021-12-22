import { Label } from '@/model/label';
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
  };
  goal: string | null;
  tips: string[];
  labels: Label[];
  repetitionDates: Moment[];
  priority: MistakePriority;
}
