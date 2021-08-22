import en from './en.json';
import pl from './pl.json';

export enum Locale {
  PL = 'pl',
  EN = 'en',
}

export const LOCALES = [
  {
    value: Locale.EN,
    caption: 'English',
    icon: '🇬🇧',
  },
  {
    value: Locale.PL,
    caption: 'Polski',
    icon: '🇵🇱',
  },
];

export const MESSAGES = {
  [Locale.PL]: pl,
  [Locale.EN]: en,
};

export const DEFAULT_LOCALE = Locale.EN;
