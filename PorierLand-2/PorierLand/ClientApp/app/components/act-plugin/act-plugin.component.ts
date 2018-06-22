import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-act-plugin',
    templateUrl: './act-plugin.component.html'
})
export class ActPluginComponent implements OnInit {
    ngOnInit(): void {
        if (typeof window === 'undefined') return;

        if (window.twttr.ready()) {
            window.twttr.widgets.load();
        }

        ga('create', 'UA-98394538-1', 'auto');
        ga('send', 'pageview');

        if (location && location.hash && location.hash.length > 1) {
            var id = location.hash;
            id = id.substring(1, id.length);

            if (document && document.getElementById(id)) {
                (document.getElementById(id) as HTMLElement).scrollIntoView();
            }
        }

        $('.ui.accordion').accordion();
    }

    scrollTo(id: string): void {
        location.hash = id;

        if (document && document.getElementById(id)) {
            (document.getElementById(id) as HTMLElement).scrollIntoView();
        }
    }
}

declare let window: {
    twttr: any;
}

declare let ga: {
    (s1: any, s2: any, s3?: any): any;
}

declare let $: {
    (selector: any): any;
}