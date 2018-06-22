import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';

import { BisSetService } from './bis-set.service';

import {
    Action,
    AttributeDisplay,
    BisCalculatorService,
    EquipCategory,
        equipCategoryToDisplayName,
        maxEquipCategoryIndex,
    Equipment,
    ItemCategory,
    Job,
    Materia,
    Meal,
    MealRow,
    Race,
    Row,
    Set,
    Table
} from './bis-calculator.service';

@Component({
    selector: 'app-bis-calculator-edit-set',
    templateUrl: './edit-set.component.html',
    styleUrls: ['./edit-set.component.css']
})
export class EditSetComponent implements OnInit, AfterViewInit {
    loading: boolean = false;
    curSetIndex: number = -1;

    race: Race = Race.Miqote1;
    job: Job = Job.Warrior;
    minLevel: number = 310;
    maxLevel: number = 345;

    mealMinLevel: number = 300;
    mealMaxLevel: number = 320;

    currentSet: Set = new Set();
    currentSetEquipments: Equipment[] = [];

    tables: Table[] = [];
    mealRows: MealRow[] = [];

    fourOnePresetEnabled: boolean = false;
    fourTwoPresetEnabled: boolean = false;

    sourceFilters: SourceFilter[] = [{
        name: '델타 영식',
        enabled: false
    }, {
        name: '만물 석판 보강',
        enabled: false
    }, {
        name: '극 신룡 토벌전',
        enabled: false
    }, {
        name: '만물 석판',
        enabled: false
    }, {
        name: '라바나스타 (24인 레이드)',
        enabled: false
    }, {
        name: '제작',
        enabled: false
    }, {
        name: '극 락슈미 토벌전',
        enabled: false
    }, {
        name: '델타 일반',
        enabled: false
    }, {
        name: '극 스사노오 토벌전',
        enabled: false
    }, {
        name: '스칼라 (315 던전)',
        enabled: false
    }, {
        name: '진리 석판',
        enabled: false
    }];

    fourOneSourceFilters: SourceFilter[] = [{
        name: '델타 영식',
        enabled: false
    }, {
        name: '만물 석판 보강',
        enabled: false
    }, {
        name: '극 신룡 토벌전',
        enabled: false
    }, {
        name: '만물 석판',
        enabled: false
    }, {
        name: '라바나스타 (24인 레이드)',
        enabled: false
    }, {
        name: '제작',
        enabled: false
    }, {
        name: '극 락슈미 토벌전',
        enabled: false
    }, {
        name: '델타 일반',
        enabled: false
    }, {
        name: '극 스사노오 토벌전',
        enabled: false
    }, {
        name: '스칼라 (315 던전)',
        enabled: false
    }, {
        name: '진리 석판',
        enabled: false
    }];

    fourTwoSourceFilters: SourceFilter[] = [{
        name: '시그마 영식',
        enabled: false
    }, {
        name: 'Mendacity 석판 보강',
        enabled: false
    }, {
        name: '극 백호 토벌전',
        enabled: false
    }, {
        name: 'Mendacity 석판',
        enabled: false
    }, {
        name: '제작',
        enabled: false
    }, {
        name: '시그마 일반',
        enabled: false
    }];

    constructor(
        private bisCalculatorService: BisCalculatorService,
        private bisSetService: BisSetService,
        private router: Router
    ) { }

    enableFourOnePreset(): void {
        if (!this.fourOnePresetEnabled) {
            this.fourOnePresetEnabled = true;
            this.fourTwoPresetEnabled = false;

            this.sourceFilters = this.fourOneSourceFilters;

            this.minLevel = 310;
            this.maxLevel = 345;

            this.mealMinLevel = 300;
            this.mealMaxLevel = 320;

            this.currentSet.currentPreset = 0;
        }
    }

    enableFourTwoPreset(): void {
        if (!this.fourTwoPresetEnabled) {
            this.fourOnePresetEnabled = false;
            this.fourTwoPresetEnabled = true;

            this.sourceFilters = this.fourTwoSourceFilters;

            this.minLevel = 350;
            this.maxLevel = 375;

            this.mealMinLevel = 320;
            this.mealMaxLevel = 340;

            this.currentSet.currentPreset = 1;
        }
    }
    
    ngOnInit(): void {
        if (typeof window === 'undefined') return;

        ga('create', 'UA-98394538-1', 'auto');
        ga('send', 'pageview');
        
        this.bisSetService.currentEquipment = null;

        if (!this.bisSetService.currentSet) {
            this.bisSetService.currentSet = new Set();
        } else {
            this.curSetIndex = this.bisSetService.sets.indexOf(this.bisSetService.currentSet);
        }
        
        this.currentSet = this.bisSetService.currentSet;
        
        const _this: EditSetComponent = this;

        $('#race-dropdown').dropdown({
            onChange: function (value: string): void {
                _this.race = parseInt(value) as Race;
            }
        });
        $('#job-dropdown').dropdown({
            onChange: function (value: string): void {
                _this.job = parseInt(value) as Job;
            }
        });
        
        if (this.currentSet.race) {
            this.race = this.currentSet.race;
        } else {
            this.race = Race.Miqote1;
        }

        if (this.currentSet.job) {
            this.job = this.currentSet.job;
        } else {
            this.job = Job.Warrior;
        }

        if (this.currentSet.minLevel) {
            this.minLevel = this.currentSet.minLevel;
        } else {
            this.minLevel = 310;
        }

        if (this.currentSet.maxLevel) {
            this.maxLevel = this.currentSet.maxLevel;
        } else {
            this.maxLevel = 345;
        }

        $('#race-dropdown').dropdown('set selected', this.race);
        $('#job-dropdown').dropdown('set selected', this.job);

        if (this.bisSetService.currentTables && this.bisSetService.currentTables.length > 0) {
            this.tables = this.bisSetService.currentTables;

            this.sortTable();

            this.currentSet.populateAttributeInfo();
            this.refreshEquipmentRows();

            this.refreshSet();
        } else {
            this.clearTable();

            if (this.bisSetService.isEditing) {
                this.refresh();
                this.mealSearch();
            }
        }

        if (this.currentSet.currentPreset !== null && this.currentSet.currentPreset !== undefined) {
            switch (this.currentSet.currentPreset) {
                case 0:
                    this.enableFourOnePreset();
                    break;
                case 1:
                    this.enableFourTwoPreset();
                    break;
            }
        }
    }

    ngAfterViewInit(): void {
        if (this.bisSetService.currentPageYOffset && this.bisSetService.currentPageYOffset > 0) {
            window.scrollBy(window.pageXOffset, this.bisSetService.currentPageYOffset);
        }
    }
    
    toggleSourceFilter(sourceFilter: SourceFilter): void {
        sourceFilter.enabled = !sourceFilter.enabled;
    }

    minLevelChanged(): void {
        if (this.minLevel > this.maxLevel) this.minLevel = this.maxLevel;
        if (this.maxLevel - this.minLevel > 50) this.minLevel = this.maxLevel - 50;
    }

    maxLevelChanged(): void {
        if (this.minLevel > this.maxLevel) this.maxLevel = this.minLevel;
        if (this.maxLevel - this.minLevel > 50) this.maxLevel = this.minLevel + 50;
    }

    mealMinLevelChanged(): void {
        if (this.mealMinLevel > this.mealMaxLevel) this.mealMinLevel = this.mealMaxLevel;
        if (this.mealMaxLevel - this.mealMinLevel > 50) this.mealMinLevel = this.mealMaxLevel - 50;
    }

    mealMaxLevelChanged(): void {
        if (this.mealMinLevel > this.mealMaxLevel) this.mealMaxLevel = this.mealMinLevel;
        if (this.mealMaxLevel - this.mealMinLevel > 50) this.mealMaxLevel = this.mealMinLevel + 50;
    }

    mealSearch(): void {
        if (this.mealMinLevel > this.mealMaxLevel) this.mealMinLevel = this.mealMaxLevel;
        if (this.mealMaxLevel - this.mealMinLevel > 50) this.mealMinLevel = this.mealMaxLevel - 50;
        if (!this.currentSet) return;

        this.loading = true;

        this.mealRows = [];

        const _this: EditSetComponent = this;
        this.bisCalculatorService.getMeals(this.mealMinLevel, this.mealMaxLevel).then(function (meals: Meal[]): void {
            if (!meals || meals.length === 0) {
                $('#error-modal').modal('show');
                _this.loading = false;
            } else {
                _this.currentSet.populateAttributeInfo();

                meals.forEach(function (meal: Meal): void {
                    let mealRow: MealRow = new MealRow();
                    mealRow.meal = meal;
                    mealRow.selected = false;
                    mealRow.attributeValues = [];
                    _this.mealRows.push(mealRow);
                });

                if (_this.currentSet.meal) {
                    _this.checkMeal(_this.currentSet.meal);
                }

                _this.mealRows.forEach(function (mealRow: MealRow): void {
                    _this.currentSet.attributeDisplays.forEach(function (): void {
                        mealRow.attributeValues.push('');
                    });

                    _this.currentSet.attributeDisplays.forEach(function (value: AttributeDisplay, index: number): void {
                        let attributeAction: Action | undefined = mealRow.meal.attributeActions.get(value.attribute);
                        if (typeof attributeAction !== 'undefined') {
                            let actualAction: Action = attributeAction as Action;

                            if (actualAction.isFixed && actualAction.fixedAmount !== -1) {
                                mealRow.attributeValues[index] = '+' + actualAction.fixedAmount;
                            } else if (!actualAction.isFixed && actualAction.rate !== -1 && actualAction.limit !== -1) {
                                mealRow.attributeValues[index] = '+' + actualAction.rate + '% (최대 ' + actualAction.limit + ')';
                            }
                        }
                    });
                });
                
                _this.mealRows.sort(function (a: MealRow, b: MealRow): number {
                    return a.meal.itemLevel - b.meal.itemLevel;
                });
                
                _this.loading = false;
            }
        });
    }

    refresh(): void {
        if (!this.job || !this.race || !this.minLevel || !this.maxLevel) return;
        if (this.minLevel > this.maxLevel) this.minLevel = this.maxLevel;
        if (this.maxLevel - this.minLevel > 50) this.minLevel = this.maxLevel - 50;

        this.loading = true;
        
        if (this.currentSet.job && this.currentSet.job !== this.job) {
            this.currentSet = new Set();
        }
        
        this.currentSet.race = this.race;
        this.currentSet.job = this.job;
        this.currentSet.minLevel = this.minLevel;
        this.currentSet.maxLevel = this.maxLevel;

        this.clearTable();

        const _this: EditSetComponent = this;
        
        this.bisCalculatorService.getEquipments(this.job, this.minLevel, this.maxLevel).then(function (equipments: Equipment[]): void {
            if (!equipments || equipments.length === 0) {
                $('#error-modal').modal('show');
                _this.loading = false;
            } else {
                let sourceFilterStrings: string[] = _this.sourceFilters
                    .filter(function (sourceFilter: SourceFilter): boolean {
                        return sourceFilter.enabled;
                    })
                    .map(function (sourceFilter: SourceFilter): string {
                        return sourceFilter.name;
                    });

                let filteredEquipments: Equipment[] = equipments.filter(function (equipment: Equipment): boolean {
                    return sourceFilterStrings.length === 0 || sourceFilterStrings.indexOf(equipment.source) !== -1;
                });

                filteredEquipments.forEach(function (value: Equipment): void {
                    _this.pushEquipment(value, false);
                });

                if (_this.currentSet.equipments) {
                    _this.currentSet.equipments.forEach(function (value: Equipment, key: EquipCategory): void {
                        _this.checkEquipment(value, key);
                    });
                }

                _this.sortTable();

                _this.currentSet.populateAttributeInfo();
                _this.refreshEquipmentRows();

                _this.refreshSet();

                _this.loading = false;
            }
        });
    }

    clearTable(): void {
        this.mealRows = [];

        this.tables = [];

        for (let i = 0; i <= maxEquipCategoryIndex; i++) {
            let categoryName: string = '';

            if (i > 0) {
                categoryName = equipCategoryToDisplayName.get(i as EquipCategory) as string;
            }

            this.tables.push({
                equipCategory: i as EquipCategory,
                equipCategoryName: categoryName,
                rows: []
            });
        }
    }

    sortTable(): void {
        for (let i = 1; i <= maxEquipCategoryIndex; i++) {
            this.tables[i].rows.sort(function (a: Row, b: Row): number {
                return a.equipment.itemLevel - b.equipment.itemLevel;
            });
        }
    }

    refreshSet(): void {
        this.currentSet.calculate();

        this.currentSetEquipments = [];

        const _this: EditSetComponent = this;

        this.currentSet.equipments.forEach(function (value: Equipment): void {
            _this.currentSetEquipments.push(value);
        });

        this.currentSetEquipments.sort(function (a: Equipment, b: Equipment): number {
            return a.itemCategory - b.itemCategory;
        });
    }

    refreshEquipmentRows(): void {
        const _this: EditSetComponent = this;

        for (let i = 1; i <= maxEquipCategoryIndex; i++) {
            this.tables[i].rows.forEach(function (row: Row): void {
                row.equipment.populateAttributes();

                row.attributeValues = [];
                row.materiaAttributeValues = [];

                if (_this.currentSet) {
                    _this.currentSet.attributeDisplays.forEach(function (): void {
                        row.attributeValues.push(0);
                        row.materiaAttributeValues.push(0);
                    });

                    _this.currentSet.attributeDisplays.forEach(function (value: AttributeDisplay, index: number): void {
                        let attributeValue: number | undefined = row.equipment.attributes.get(value.attribute);
                        let actualValue: number = 0;
                        if (typeof attributeValue !== 'undefined') {
                            actualValue = attributeValue as number;
                        }
                        row.attributeValues[index] = actualValue;

                        let materiaAttributeValue: number | undefined = row.equipment.materiaAttributes.get(value.attribute);
                        actualValue = 0;
                        if (typeof materiaAttributeValue !== 'undefined') {
                            actualValue = materiaAttributeValue as number;
                        }
                        row.materiaAttributeValues[index] = actualValue;
                    });
                }

                row.materiaNames = '';
                
                row.equipment.materias.forEach(function (value: Materia): void {
                    if (value.materiaName && value.materiaName.length > 0) {
                        row.materiaNames += value.materiaName + ', ';
                    }
                });

                if (row.materiaNames && row.materiaNames.length > 2) {
                    row.materiaNames = row.materiaNames.substring(0, row.materiaNames.length - 2);
                }
            });
        }
    }

    pushEquipment(equipment: Equipment, selected: boolean): void {
        let equipCategories: EquipCategory[] = this.getEquipCategoriesFromItemCategory(equipment.itemCategory);

        const _this: EditSetComponent = this;

        equipCategories.forEach(function (value: EquipCategory): void {
            let newEquipment: Equipment = new Equipment();
            newEquipment.fromObject(equipment.originalObject);

            _this.tables[value as number].rows.push({
                equipment: newEquipment,
                selected: selected,
                attributeValues: [],
                materiaAttributeValues: [],
                materiaNames: ''
            });
        });
    }

    checkEquipment(equipment: Equipment, equipCategory: EquipCategory): void {
        let found: boolean = false;

        this.tables[equipCategory as number].rows.forEach(function (value: Row): void {
            if (value.equipment.key === equipment.key) {
                value.equipment = equipment;
                value.selected = true;
                found = true;
            }
        });

        if (!found) {
            this.tables[equipCategory as number].rows.push({
                equipment: equipment,
                selected: true,
                attributeValues: [],
                materiaAttributeValues: [],
                materiaNames: ''
            });
        }
    }

    checkMeal(meal: Meal): void {
        let found: boolean = false;

        this.mealRows.forEach(function (mealRow: MealRow): void {
            if (mealRow.meal.key === meal.key) {
                mealRow.meal = meal;
                mealRow.selected = true;
                found = true;
            }
        });

        if (!found) {
            let newMealRow: MealRow = new MealRow();
            newMealRow.meal = meal;
            newMealRow.attributeValues = [];
            newMealRow.selected = true;
            this.mealRows.push(newMealRow);
        }
    }
    
    getEquipCategoriesFromItemCategory(itemCategory: ItemCategory): EquipCategory[] {
        let equipCategories: EquipCategory[] = [];

        switch (itemCategory) {
            case ItemCategory.OneHandedArm:
            case ItemCategory.TwoHandedArm:
                equipCategories.push(EquipCategory.Weapon);
                break;
            case ItemCategory.Shield:
                equipCategories.push(EquipCategory.Shield);
                break;

            case ItemCategory.Head:
                equipCategories.push(EquipCategory.Head);
                break;
            case ItemCategory.Body:
                equipCategories.push(EquipCategory.Body);
                break;
            case ItemCategory.Hands:
                equipCategories.push(EquipCategory.Hands);
                break;
            case ItemCategory.Waist:
                equipCategories.push(EquipCategory.Waist);
                break;
            case ItemCategory.Legs:
                equipCategories.push(EquipCategory.Legs);
                break;
            case ItemCategory.Feet:
                equipCategories.push(EquipCategory.Feet);
                break;

            case ItemCategory.Earrings:
                equipCategories.push(EquipCategory.Earrings);
                break;
            case ItemCategory.Necklace:
                equipCategories.push(EquipCategory.Necklace);
                break;
            case ItemCategory.Bracelets:
                equipCategories.push(EquipCategory.Bracelets);
                break;
            case ItemCategory.Ring:
                equipCategories.push(EquipCategory.LeftRing);
                equipCategories.push(EquipCategory.RightRing);
                break;
        }

        return equipCategories;
    }

    selectRow(table: Table, row: Row): void {
        if (row.selected) {
            row.selected = false;
            this.currentSet.equipments.delete(table.equipCategory);
        } else {
            const _this: EditSetComponent = this;

            table.rows.forEach(function (value: Row): void {
                if (value.selected) {
                    _this.selectRow(table, value);
                }
            });

            row.selected = true;
            this.currentSet.equipments.set(table.equipCategory, row.equipment);
        }

        this.refreshSet();
    }

    selectMealRow(mealRow: MealRow): void {
        if (mealRow.selected) {
            mealRow.selected = false;
            this.currentSet.meal = null;
        } else {
            const _this: EditSetComponent = this;

            this.mealRows.forEach(function (value: MealRow): void {
                if (value.selected) {
                    _this.selectMealRow(value);
                }
            });

            mealRow.selected = true;
            this.currentSet.meal = mealRow.meal;
        }

        this.refreshSet();
    }

    editMateria(equipment: Equipment): void {
        this.bisSetService.currentEquipment = equipment;
        this.bisSetService.currentSet = this.currentSet;
        this.bisSetService.currentTables = this.tables;
        this.bisSetService.currentPageYOffset = window.pageYOffset;

        this.router.navigate(['/bis-calculator/edit-materia']);
    }

    addSet(): void {
        if (this.curSetIndex !== -1) {
            this.bisSetService.sets.splice(this.curSetIndex, 1);
        }

        this.bisSetService.sets.push(this.currentSet);

        this.bisSetService.sets.sort(function (a: Set, b: Set): number {
            return a.job - b.job;
        });

        this.bisSetService.currentSet = new Set();
        this.bisSetService.currentEquipment = null;
        this.bisSetService.currentTables = [];

        this.router.navigate(['/bis-calculator']);
    }
}

class SourceFilter {
    name: string;
    enabled: boolean;

    constructor() {
        this.name = '';
        this.enabled = false;
    }
}

declare let $: {
    (selector: any): any;
}

declare let ga: {
    (s1: any, s2: any, s3?: any): any;
}