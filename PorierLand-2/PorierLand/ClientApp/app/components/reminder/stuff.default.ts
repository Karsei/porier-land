import { Stuff, ResetPeriod } from './stuff';

export let DailyStuffDefault: Stuff[] = [{
  title: '일일 복권 긁었니?',
  completed: false,
  resetPeriod: ResetPeriod.DAILY,
  resetDay: 1,
  resetHour: 15,
  nextResetDate: new Date()
}, {
  title: '무작위 숙련 다녀왔니?',
  completed: false,
  resetPeriod: ResetPeriod.DAILY,
  resetDay: 1,
  resetHour: 15,
  nextResetDate: new Date()
}, {
  title: '무작위 레벨링 다녀왔니?',
  completed: false,
  resetPeriod: ResetPeriod.DAILY,
  resetDay: 1,
  resetHour: 15,
  nextResetDate: new Date()
}, {
  title: '적정 던전 다녀왔니?',
  completed: false,
  resetPeriod: ResetPeriod.DAILY,
  resetDay: 1,
  resetHour: 15,
  nextResetDate: new Date()
}, {
  title: '무작위 토벌전 다녀왔니?',
  completed: false,
  resetPeriod: ResetPeriod.DAILY,
  resetDay: 1,
  resetHour: 15,
  nextResetDate: new Date()
}, {
  title: '야만족 퀘스트 수주권 다 썼니?',
  completed: false,
  resetPeriod: ResetPeriod.DAILY,
  resetDay: 1,
  resetHour: 15,
  nextResetDate: new Date()
}, {
  title: '채집 조달 다 했니?',
  completed: false,
  resetPeriod: ResetPeriod.DAILY,
  resetDay: 1,
  resetHour: 20,
  nextResetDate: new Date()
}, {
  title: '제작 조달 다 했니?',
  completed: false,
  resetPeriod: ResetPeriod.DAILY,
  resetDay: 1,
  resetHour: 20,
  nextResetDate: new Date()
}];

export let WeeklyStuffDefault: Stuff[] = [{
  title: '주간 복권 긁었니?',
  completed: false,
  resetPeriod: ResetPeriod.WEEKLY,
  resetDay: 6,
  resetHour: 12,
  nextResetDate: new Date()
}, {
  title: '공략 수첩 다 했니?',
  completed: false,
  resetPeriod: ResetPeriod.WEEKLY,
  resetDay: 2,
  resetHour: 8,
  nextResetDate: new Date()
}, {
  title: '쿠로의 공상 수첩 다 했니?',
  completed: false,
  resetPeriod: ResetPeriod.WEEKLY,
  resetDay: 2,
  resetHour: 8,
  nextResetDate: new Date()
}, {
  title: '라바나스터 다녀왔니?',
  completed: false,
  resetPeriod: ResetPeriod.WEEKLY,
  resetDay: 2,
  resetHour: 8,
  nextResetDate: new Date()
}];
