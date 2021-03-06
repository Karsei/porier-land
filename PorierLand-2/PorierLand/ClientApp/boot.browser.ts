﻿import 'reflect-metadata';
import 'zone.js';
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module.browser';

if (module.hot) {
    module.hot.accept();
    module.hot.dispose(() => {
        const oldRootElm = document.querySelector('app');
        const newRootElm = document.createElement('app');
        oldRootElm!.parentNode!.insertBefore(newRootElm, oldRootElm);
        modulePromise.then(appModule => appModule.destroy());
    });
} else {
    enableProdMode();
}

const modulePromise = platformBrowserDynamic().bootstrapModule(AppModule);