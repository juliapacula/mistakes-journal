import { Mistake } from '@/model/mistake';
import { NewMistake } from '@/model/new-mistake';

export const hasDeepAnalysis: (mistake: Mistake | NewMistake) => boolean = (mistake: Mistake | NewMistake) => mistake.mistakeAdditionalQuestions
  && (!!mistake.mistakeAdditionalQuestions.consequences
    || !!mistake.mistakeAdditionalQuestions.onlyResponsible
    || !!mistake.mistakeAdditionalQuestions.canIFixIt
    || !!mistake.mistakeAdditionalQuestions.whatDidILearn
    || !!mistake.mistakeAdditionalQuestions.whatCanIDoBetter);
