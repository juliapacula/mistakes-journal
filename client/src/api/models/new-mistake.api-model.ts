import { MistakePriority } from '@/model/mistake-priority.enum';

export interface NewMistakeApiModel {
  name: string;
  goal: string | null;
  priority: MistakePriority;
  consequences: string | null;
  whatCanIDoBetter: string | null;
  whatDidILearn: string | null;
  canIFixIt: string | null;
  onlyResponsible: string | null;
  tips: string[];
  labels: string[];
}
