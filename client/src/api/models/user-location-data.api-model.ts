import { TimeOfDay } from '@/model/time-of-day.enum';

export interface UserLocationDataApiModel {
  /**
   * Scale from 0 (great weather) to 9 (very bad weather).
   */
  weatherHopelessnessScale: number;
  city: string;
  timeOfDay: TimeOfDay;
}
