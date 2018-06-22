import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { BisSetService } from './bis-set.service';
import {
    BisCalculatorService,
    EquipCategory,
        maxEquipCategoryIndex,
    Equipment,
    EquipmentInfo,
    jobToDisplayName,
    jobToIconPath,
    Set,
    raceToDisplayName
} from './bis-calculator.service';

@Component({
    selector: 'app-bis-calculator',
    templateUrl: './bis-calculator.component.html',
    styleUrls: ['./bis-calculator.component.css']
})
export class BisCalculatorComponent implements OnInit {
    loading: boolean = false;
    shareButtonLoading: boolean = false;
    shareLink: string;

    equipmentDisplayMap: number[][] = [
        [EquipCategory.Weapon, -1],
        [EquipCategory.Head, EquipCategory.Shield],
        [EquipCategory.Body, EquipCategory.Earrings],
        [EquipCategory.Hands, EquipCategory.Necklace],
        [EquipCategory.Waist, EquipCategory.Bracelets],
        [EquipCategory.Legs, EquipCategory.LeftRing],
        [EquipCategory.Feet, EquipCategory.RightRing]
    ];

    sets: Set[];
    equipments: (Equipment | null)[][];
    
    jobDisplayNames: string[];
    raceDisplayNames: string[];
    jobIconPaths: string[];
    
    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private bisCalculatorService: BisCalculatorService,
        private bisSetService: BisSetService) {
        this.shareLink = '';

        this.sets = [];
        this.equipments = [];

        this.jobDisplayNames = [];
        this.raceDisplayNames = [];
        this.jobIconPaths = [];
    }

    ngOnInit(): void {
        if (typeof window === 'undefined') return;

        ga('create', 'UA-98394538-1', 'auto');
        ga('send', 'pageview');

        let routeId: string | null = this.route.snapshot.paramMap.get('id');

        if (routeId) {
            this.loading = true;

            const _this: BisCalculatorComponent = this;

            this.bisCalculatorService.getBisSets(routeId as string).then(function (sets: Set[]): void {
                if (!sets || sets.length === 0) {
                    $('#error-modal').modal('show');
                    _this.bisSetService.sets = [];
                    _this.sets = _this.bisSetService.sets;

                    _this.loading = false;
                } else {
                    let promise: Promise<void> = Promise.resolve();

                    _this.bisSetService.sets = sets;
                    _this.sets = _this.bisSetService.sets;
                    
                    _this.sets.forEach(function (set: Set): void {
                        set.equipments.forEach(function (equipment: Equipment) {
                            if (equipment.key) {
                                promise = promise.then(function (): Promise<EquipmentInfo | null> {
                                    return _this.bisCalculatorService.getEquipmentInfo(equipment.key);
                                }).then(function (equipmentInfo: EquipmentInfo | null): Promise<void> {
                                    if (equipmentInfo) {
                                        equipment.nameKo = equipmentInfo.nameKo;
                                        equipment.nameEn = equipmentInfo.nameEn;
                                        equipment.iconPath = equipmentInfo.iconPath;
                                        equipment.source = equipmentInfo.source;
                                    }

                                    return Promise.resolve();
                                });
                            }
                        });
                    });
                    
                    promise.then(function (): void {
                        _this.refreshScreen();
                        _this.loading = false;
                    });
                }
            });
        } else {
            if (!this.bisSetService.sets) {
                this.bisSetService.sets = [];
            }

            this.sets = this.bisSetService.sets;

            this.refreshScreen();
        }
    }

    refreshScreen(): void {
        this.sets.forEach(function (set: Set): void {
            set.populateAttributeInfo();
            set.calculate();
        });

        this.sets.sort(function (a: Set, b: Set): number {
            return a.job - b.job;
        });

        this.jobDisplayNames = [];
        this.jobIconPaths = [];
        this.raceDisplayNames = [];

        this.equipments = [];

        const _this: BisCalculatorComponent = this;

        this.sets.forEach(function (): void {
            _this.jobDisplayNames.push('');
            _this.jobIconPaths.push('');
            _this.raceDisplayNames.push('');

            let temp: (Equipment | null)[] = [];
            for (let i = 0; i <= maxEquipCategoryIndex; i++) {
                temp.push(null);
            }
            _this.equipments.push(temp);
        });

        this.sets.forEach(function (set: Set, index: number): void {
            _this.jobDisplayNames[index] = jobToDisplayName.get(set.job) as string;
            _this.jobIconPaths[index] = jobToIconPath.get(set.job) as string;
            _this.raceDisplayNames[index] = raceToDisplayName.get(set.race) as string;

            set.equipments.forEach(function (equipment: Equipment, equipCategory: EquipCategory): void {
                _this.equipments[index][equipCategory] = equipment;
            });
        });
    }

    addNewSet(): void {
        this.bisSetService.sets = this.sets;

        this.bisSetService.currentSet = new Set();
        this.bisSetService.currentTables = [];
        this.bisSetService.isEditing = false;

        this.router.navigate(['/bis-calculator/edit-set']);
    }

    removeAllSets(): void {
        this.bisSetService.sets = [];
        this.sets = this.bisSetService.sets;
    }

    editSet(set: Set): void {
        this.bisSetService.sets = this.sets;

        this.bisSetService.currentSet = set;
        this.bisSetService.currentTables = [];
        this.bisSetService.isEditing = true;

        this.router.navigate(['/bis-calculator/edit-set']);
    }

    duplicate(set: Set): void {
        let newSet: Set = new Set();
        newSet.clone(set);
        this.sets.push(newSet);
        this.refreshScreen();
    }

    deleteSet(set: Set): void {
        this.sets.splice(this.sets.indexOf(set), 1);
        this.refreshScreen();
    }

    shareSets(): void {
        if (this.sets && this.sets.length > 0) {
            this.shareButtonLoading = true;
            this.shareLink = '';

            const _this: BisCalculatorComponent = this;
            
            this.bisCalculatorService.postBisSets(this.sets).then(function (id: string): void {
                if (id && id.length > 0) {
                    _this.shareLink = 'https://porier-land.tk/bis-calculator/' + id;
                } else {
                    $('#error-modal').modal('show');
                }

                _this.shareButtonLoading = false;
            });
        }
    }

    copyShareLink(): void {
        (document.getElementById('shareLinkInput') as any).select();
        document.execCommand('copy');
    }
}

declare let ga: {
    (s1: any, s2: any, s3?: any): any;
}

declare let $: {
    (selector: any): any;
}