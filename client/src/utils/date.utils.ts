import moment from 'moment';

export const convertFromTimestamp = (date: string): moment.Moment => moment.utc(date).local();

export const convertToTimestamp = (date: moment.Moment): string => date.utc().format('YYYY-MM-DDTHH:mm:SS');
