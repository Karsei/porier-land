export enum ResetPeriod {
  DAILY = 0,
  WEEKLY = 1
}

export class Stuff {
  title: string;
  completed: boolean;
  resetPeriod: ResetPeriod;
  resetDay: number;
  resetHour: number;
  nextResetDate: Date;
}

export class StuffInputData {
  title: string;
  resetPeriod: string;
  resetLocalDay: string;
  resetLocalHour: string;
  resetLocalNoon: string;
}
