import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'plugin-release-note',
    templateUrl: './plugin-release-note.component.html'
})
export class PluginReleaseNoteComponent implements OnInit {
    ngOnInit(): void {
        if (typeof window === 'undefined') return;

        if (window.twttr.ready()) {
            window.twttr.widgets.load();
        }

        ga('create', 'UA-98394538-1', 'auto');
        ga('send', 'pageview');
    }

	releaseNotes: ReleaseNote[] = [{
		date: '2018 01 11',
		time: '04:30 PM',
		notes: [{
			title: '1.2',
			changes: [
				'D/HPS 오버레이가 갱신 시 깜빡이던 버그를 수정하였습니다.',
				'오버힐이 0일 경우 오버힐%가 -inf로 출력되는 버그를 수정하였습니다.',
				'오버레이 모서리를 둥글게 만드는 옵션을 추가하였습니다.',
				'게임모드를 켠 채로 종료했다가 재기동 시 클릭 통과가 적용되지 않는 버그를 수정하였습니다.',
				'설정 창의 드랍다운 메뉴 항목들을 수정하였습니다.',
				'폰트 크기 변경 시 설정 창에서 폰트 크기가 잘못 출력되던 버그를 수정하였습니다.'
			]
		}]
	}, {
		date: '2018 01 02',
		time: '11:51 AM',
		notes: [{
			title: '1.1',
			changes: [
				'플러그인 자동 업데이트 기능을 추가하였습니다.',
				'플레이어가 소환사/학자일 경우 소환수 D/HPS 합산이 이루어지지 않던 버그를 수정하였습니다.'
			]
		}]
	}, {
        date: '2017 12 19',
        time: '04:48 AM',
        notes: [{
            title: '1.0',
            changes: [
                '첫 릴리즈!'
            ]
        }]
    }];
}

class ReleaseNote {
    date: string;
    time: string;

    notes: Note[];
}

class Note {
    title: string;
    changes: string[];
}

declare let window: {
    twttr: any;
}

declare let ga: {
    (s1: any, s2: any, s3?: any): any;
}