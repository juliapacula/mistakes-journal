import moment from 'moment';

export const formattedDate = (date: moment.Moment): string => date.format('DD/MM/YYYY');
