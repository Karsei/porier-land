import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app',
    templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
    ngOnInit(): void {
        if (typeof window === 'undefined') return;

        window.twttr = (function (d, s, id) {
            const fjs = d.getElementsByTagName(s)[0];
            const t = window.twttr || {};
            if (d.getElementById(id)) {
                return t;
            }
            const js: any = d.createElement(s);
            js.id = id;
            js.src = 'https://platform.twitter.com/widgets.js';
            fjs.parentNode!.insertBefore(js, fjs);

            t._e = [];
            t.ready = function (f: any) {
                t._e.push(f);
            };

            return t;
        }(document, 'script', 'twitter-wjs'));
        if (window.twttr.ready()) {
            window.twttr.widgets.load();
        }

        (function (i, s, o, g, r) {
            i['GoogleAnalyticsObject'] = r;
            i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments);
            };
            i[r].l = new Date();
            let a: any = s.createElement(o);
            let m: any = s.getElementsByTagName(o)[0];
            a.async = 1;
            a.src = g;
            m.parentNode.insertBefore(a, m);
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-98394538-1', 'auto');
        ga('send', 'preview');

        $('.ui.dropdown').dropdown();
    }

    openFriendModal(): void {
        $('#friend-modal').modal({ inverted: true }).modal('show');
    }
}

declare let $: {
    (selector: any): any;
};

declare let window: {
    twttr: any;
    [index: string]: any;
};

declare let ga: {
    (s1: any, s2: any, s3?: any): any;
};