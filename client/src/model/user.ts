import { Locale } from '@/i18n/locales';
import { ResearchGroup } from '@/model/research-group.enum';

export interface User {
  firstName: string;
  lastName: string;
  email: string;
  group: ResearchGroup;
  language: Locale;
  watchedTutorial: boolean;
}
