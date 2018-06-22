import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
    ngOnInit(): void {
        if (typeof window === 'undefined') return;

        if (window.twttr.ready()) {
            window.twttr.widgets.load();
        }

        ga('create', 'UA-98394538-1', 'auto');
        ga('send', 'pageview');
    }
}

declare let window: {
    twttr: any;
}

declare let ga: {
    (s1: any, s2: any, s3?: any): any;
}