import { maxLength } from 'vuelidate/lib/validators';

export const maxShortTextLength = maxLength(100);
export const maxVeryShortTextLength = maxLength(20);
