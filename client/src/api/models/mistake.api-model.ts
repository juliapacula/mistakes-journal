import { MistakePriority } from '@/model/mistake-priority.enum';

export interface MistakeApiModel {
  id: string;
  createdAt: string,
  name: string;
  mistakeAdditionalQuestions: {
    consequences: string | null;
    whatCanIDoBetter: string | null;
    whatDidILearn: string | null;
    canIFixIt: string | null;
    onlyResponsible: string | null;
  } | null;
  goal: string | null;
  priority: MistakePriority;
  repetitionDates: { id: string, occurredAt: string }[];
  tips: string[];
}
