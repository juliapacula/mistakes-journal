import { MistakePriority } from '@/model/mistake-priority.enum';

export interface NewMistake {
  name: string;
  goal: string;
  tips: string[];
  labelIds: string[];
  priority: MistakePriority;
  mistakeAdditionalQuestions: {
    consequences: string | null;
    whatCanIDoBetter: string | null;
    whatDidILearn: string | null;
    canIFixIt: string | null;
    onlyResponsible: string | null;
  };
}
