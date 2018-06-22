import { Observable, Subscription } from 'rxjs/Rx';

import { Component, OnInit, OnDestroy } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';

import { Stuff, StuffInputData, ResetPeriod } from './stuff';

import { DailyStuffDefault, WeeklyStuffDefault } from './stuff.default';

@Component({
  selector: 'app-reminder',
  templateUrl: './reminder.component.html',
  animations: [
    trigger('appearTrigger', [
      state('in', style({
        transform: 'scale(1)'
      })),
      transition('void => *', [
        style({
          transform: 'scale(0)'
        }),
        animate(200)
      ]),
      transition('* => void', [
        animate(200, style({
          transform: 'scale(0)'
        }))
      ])
    ]),
    trigger('dropdownTrigger', [state('in', style({}))])
  ]
})
export class ReminderComponent implements OnInit, OnDestroy {
  checkReset: Subscription;

  dailyStuffs: Stuff[] = [];
  weeklyStuffs: Stuff[] = [];

  newStuff: StuffInputData = new StuffInputData();

  saveDailyStuffs(): void {
    localStorage.setItem('dailyStuffs', JSON.stringify(this.dailyStuffs));
  }

  saveWeeklyStuffs(): void {
    localStorage.setItem('weeklyStuffs', JSON.stringify(this.weeklyStuffs));
  }

  reset(): void {
    this.dailyStuffs = [];
    for (const dailyStuff of DailyStuffDefault) {
      this.dailyStuffs.push(dailyStuff);
    }
    this.saveDailyStuffs();

    this.weeklyStuffs = [];
    for (const weeklyStuff of WeeklyStuffDefault) {
      this.weeklyStuffs.push(weeklyStuff);
    }
  }

  ngOnInit(): void {
      if (typeof window === 'undefined') return;

      ga('create', 'UA-98394538-1', 'auto');
      ga('send', 'pageview');

    $('select.ui.dropdown').dropdown();

    const _this: ReminderComponent = this;

    const daily = localStorage.getItem('dailyStuffs');

    if (daily) {
      this.dailyStuffs = JSON.parse(daily);
      for (const dailyStuff of this.dailyStuffs) {
        dailyStuff.nextResetDate = new Date(dailyStuff.nextResetDate);
      }
    } else {
      this.dailyStuffs = [];
      for (const dailyStuff of DailyStuffDefault) {
        this.dailyStuffs.push(dailyStuff);
      }
      this.saveDailyStuffs();
    }

    const weekly = localStorage.getItem('weeklyStuffs');

    if (weekly) {
      this.weeklyStuffs = JSON.parse(weekly);
      for (const weeklyStuff of this.weeklyStuffs) {
        weeklyStuff.nextResetDate = new Date(weeklyStuff.nextResetDate);
      }
    } else {
      this.weeklyStuffs = [];
      for (const weeklyStuff of WeeklyStuffDefault) {
        this.weeklyStuffs.push(weeklyStuff);
      }
      this.saveWeeklyStuffs();
    }

    this.checkReset = Observable.timer(0, 1000).subscribe(function () {
      _this.checkResetTimes();
    });
  }

  ngOnDestroy(): void {
      if (typeof window === 'undefined') return;

    this.checkReset.unsubscribe();

    this.saveDailyStuffs();
    this.saveWeeklyStuffs();
  }

  createDropdown(): void {
    $('select.ui.dropdown').dropdown();
  }

  checkResetTimes(): void {
    const current: Date = new Date(Date.now());
    let changed = false;

    for (const dailyStuff of this.dailyStuffs) {
      if (dailyStuff.completed) {
        if (dailyStuff.nextResetDate <= current) {
          dailyStuff.completed = false;
          changed = true;
        }
      }
    }

    if (changed) {
      this.saveDailyStuffs();
    }

    changed = false;

    for (const weeklyStuff of this.weeklyStuffs) {
      if (weeklyStuff.completed) {
        if (weeklyStuff.nextResetDate <= current) {
          weeklyStuff.completed = false;
          changed = true;
        }
      }
    }

    if (changed) {
      this.saveWeeklyStuffs();
    }
  }

  toggleCompleted(stuff: Stuff): void {
    stuff.completed = !stuff.completed;

    if (stuff.completed) {
      stuff.nextResetDate = new Date(Date.now());
      stuff.nextResetDate.setMilliseconds(0);
      stuff.nextResetDate.setSeconds(0);
      stuff.nextResetDate.setMinutes(0);

      switch (stuff.resetPeriod) {
        case ResetPeriod.DAILY:
          if (stuff.nextResetDate.getUTCHours() >= stuff.resetHour) {
            stuff.nextResetDate.setUTCDate(stuff.nextResetDate.getUTCDate() + 1);
          }
          stuff.nextResetDate.setUTCHours(stuff.resetHour);
          break;

        case ResetPeriod.WEEKLY:
          let dateDiff = stuff.resetDay - stuff.nextResetDate.getUTCDay();

          if (dateDiff <= 0) {
            dateDiff = dateDiff + 7;
          }

          stuff.nextResetDate.setUTCDate(stuff.nextResetDate.getUTCDate() + dateDiff);
          stuff.nextResetDate.setUTCHours(stuff.resetHour);
          break;
      }
    }

    switch (stuff.resetPeriod) {
      case ResetPeriod.DAILY:
        this.saveDailyStuffs();
        break;
      case ResetPeriod.WEEKLY:
        this.saveWeeklyStuffs();
        break;
    }
  }

  addNewStuff(): void {
    const stuff: Stuff = new Stuff();
    stuff.title = this.newStuff.title;
    stuff.completed = false;
    stuff.resetPeriod = parseInt(this.newStuff.resetPeriod, 10);

    const current = new Date(Date.now());

    if (stuff.resetPeriod === ResetPeriod.WEEKLY) {
      const dateDiff = parseInt(this.newStuff.resetLocalDay, 10) - current.getDay();
      current.setDate(current.getDate() + dateDiff);
    }

    current.setHours(parseInt(this.newStuff.resetLocalHour, 10));

    if (this.newStuff.resetLocalNoon === '1') {
      current.setHours(current.getHours() + 12);
    }

    stuff.resetDay = current.getUTCDay();
    stuff.resetHour = current.getUTCHours();
    stuff.nextResetDate = new Date();

    switch (stuff.resetPeriod) {
      case ResetPeriod.DAILY:
        this.dailyStuffs.push(stuff);
        this.saveDailyStuffs();
        break;

      case ResetPeriod.WEEKLY:
        this.weeklyStuffs.push(stuff);
        this.saveWeeklyStuffs();
        break;
    }

    this.newStuff = new StuffInputData();
  }

  removeDaily(dailyStuff: Stuff): void {
    this.dailyStuffs.splice(this.dailyStuffs.indexOf(dailyStuff), 1);
    this.saveDailyStuffs();
  }

  removeWeekly(weeklyStuff: Stuff): void {
    this.weeklyStuffs.splice(this.weeklyStuffs.indexOf(weeklyStuff), 1);
    this.saveWeeklyStuffs();
  }
}

declare let $: {
  (selector: any): any;
};

declare let ga: {
  (s1: any, s2: any, s3?: any): any;
};
