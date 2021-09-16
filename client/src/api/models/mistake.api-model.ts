import { MistakePriority } from '@/model/mistake-priority.enum';

export interface MistakeApiModel {
  id: string;
  createdAt: string,
  name: string;
  goal: string | null;
  priority: MistakePriority;
  repetitionDates: { id: string, occurredAt: string }[];
  tips: string[];
}
