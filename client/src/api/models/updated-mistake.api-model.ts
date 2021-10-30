import { MistakePriority } from '@/model/mistake-priority.enum';

export interface UpdatedMistakeApiModel {
  name: string;
  goal: string | null;
  priority: MistakePriority;
  tips: string[];
  labels: string[];
  consequences: string | null;
  whatCanIDoBetter: string | null;
  whatDidILearn: string | null;
  canIFixIt: string | null;
  onlyResponsible: string | null;
}
