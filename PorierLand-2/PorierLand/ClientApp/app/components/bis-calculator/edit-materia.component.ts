import { Component, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { Router } from '@angular/router';

import { BisSetService } from './bis-set.service';

import {
    Attribute,
        attributeToDisplayName,
    AttributeDisplay,
    Equipment,
    Materia,
        attributeToMateriaNamePrepend,
        materiaTypeToMateriaNameAppend,
    MateriaType,
    Set
} from './bis-calculator.service';

@Component({
    selector: 'app-bis-calculator-edit-materia',
    templateUrl: './edit-materia.component.html',
    styleUrls: ['./edit-materia.component.css'],
    animations: [
        trigger('dropdownTrigger', [
            state('in', style({}))
        ])
    ]
})
export class EditMateriaComponent implements OnInit {
    loading: boolean = false;

    currentSet: Set;
    currentEquipment: Equipment;

    equipmentAttributeValues: number[];
    availableAttributeValues: number[];
    materiaAttributeValues: number[];

    attributeNumbers: number[] = [];
    materiaTypeNumbers: number[] = [];

    availableMaterias: AvailableMateria[];

    constructor(
        private bisSetService: BisSetService,
        private router: Router) { }

    ngOnInit(): void {
        if (typeof window === 'undefined') return;

        ga('create', 'UA-98394538-1', 'auto');
        ga('send', 'pageview');
        
        if (!this.bisSetService.currentEquipment) {
            this.router.navigate(['/bis-calculator/edit-set']);
            return;
        }

        this.currentSet = this.bisSetService.currentSet;
        this.currentEquipment = this.bisSetService.currentEquipment;

        this.currentEquipment.populateAttributes();

        this.currentSet.populateAttributeInfo();
        this.currentSet.calculate();

        this.refreshAvailableMaterias();
        this.refreshAvailability();
        this.refreshAttributeTable();
    }

    refreshAvailableMaterias(): void {
        this.availableMaterias = [];

        const _this: EditMateriaComponent = this;

        this.currentEquipment.materias.forEach(function (): void {
            _this.availableMaterias.push(new AvailableMateria);
        });

        this.currentEquipment.materias.forEach(function (materia: Materia, index: number): void {
            let attributes: Attribute[] = [
                _this.currentSet.speedAttribute,
                Attribute.DirectHit,
                Attribute.CriticalHit,
                Attribute.Determination
            ];

            if (_this.currentSet.isTank) {
                attributes.push(Attribute.Tenacity);
            }

            if (_this.currentSet.isHeal) {
                attributes.push(Attribute.Piety);
            }

            if (!materia.isForbidden) {
                attributes.push(Attribute.Vitality);
                attributes.push(_this.currentSet.damageAttribute);
            }
            
            attributes.sort(function (a: Attribute, b: Attribute): number {
                return a - b;
            });

            _this.availableMaterias[index].attributes = attributes;

            let attributeDisplayNames: string[] = [];
            let attributeMateriaNames: string[] = [];
            let attributeIsAvailable: boolean[] = [];

            attributes.forEach(function (): void {
                attributeDisplayNames.push('');
                attributeMateriaNames.push('');
                attributeIsAvailable.push(false);
            });

            attributes.forEach(function (attribute: Attribute, index2: number): void {
                attributeDisplayNames[index2] = attributeToDisplayName.get(attribute) as string;
                attributeMateriaNames[index2] = attributeToMateriaNamePrepend.get(attribute) as string;
            });

            _this.availableMaterias[index].attributeDisplayNames = attributeDisplayNames;
            _this.availableMaterias[index].attributeMateriaNames = attributeMateriaNames;
            _this.availableMaterias[index].attributeIsAvailable = attributeIsAvailable;

            let maxMateriaType: MateriaType = materia.highestType;

            let materiaTypes: MateriaType[] = [];

            for (let materiaType = MateriaType.I; materiaType <= maxMateriaType; materiaType++) {
                materiaTypes.push(materiaType);
            }

            _this.availableMaterias[index].materiaTypes = materiaTypes;

            let materiaTypeDisplayNames: string[] = [];

            materiaTypes.forEach(function (): void {
                materiaTypeDisplayNames.push('');
            });

            materiaTypes.forEach(function (materiaType: MateriaType, index2: number): void {
                materiaTypeDisplayNames[index2] = materiaTypeToMateriaNameAppend.get(materiaType) as string;
            });

            _this.availableMaterias[index].materiaTypeDisplayNames = materiaTypeDisplayNames;

            _this.availableMaterias[index].isForbidden = materia.isForbidden;
        });
    }

    refreshAvailability(): void {
        const _this: EditMateriaComponent = this;

        this.availableMaterias.forEach(function (availableMateria: AvailableMateria): void {
            for (let i = 0; i < availableMateria.attributes.length; i++) {
                availableMateria.attributeIsAvailable[i] = _this.currentEquipment.availableAttributes.get(availableMateria.attributes[i]) as number > 0;
            }
        });
    }

    refreshAttributeTable(): void {
        this.equipmentAttributeValues = [];
        this.availableAttributeValues = [];
        this.materiaAttributeValues = [];

        const _this: EditMateriaComponent = this;

        if (this.currentSet && this.currentSet.attributeDisplays) {
            this.currentSet.attributeDisplays.forEach(function (): void {
                _this.equipmentAttributeValues.push(0);
                _this.availableAttributeValues.push(0);
                _this.materiaAttributeValues.push(0);
            });

            this.currentSet.attributeDisplays.forEach(function (attributeDisplay: AttributeDisplay, index: number): void {
                _this.equipmentAttributeValues[index] = _this.currentEquipment.attributes.get(attributeDisplay.attribute) as number;
                _this.availableAttributeValues[index] = _this.currentEquipment.availableAttributes.get(attributeDisplay.attribute) as number;
                _this.materiaAttributeValues[index] = _this.currentEquipment.materiaAttributes.get(attributeDisplay.attribute) as number;
            });
        }
    }

    initializeAttributeDropdown($event: any, index: number): void {
        while (this.attributeNumbers.length <= index) {
            this.attributeNumbers.push(0);
        }

        const _this: EditMateriaComponent = this;

        $($event.element).dropdown({
            onChange: function (value: string, text: any, $selectedItem: any) {
                let object: any = JSON.parse(value.substring(0, value.length - 6));
                let index: number = parseInt(object['index']);
                let attributeNumber: number = parseInt(object['attribute']);

                _this.attributeNumbers[index] = attributeNumber;

                if (attributeNumber === -1) {
                    if (_this.materiaTypeNumbers.length <= index || _this.materiaTypeNumbers[index] !== -1) {
                        _this.updateMateriaTypeDropdown(index, -1);
                    }
                    _this.currentEquipment.materias[index].materiaType = MateriaType.None;
                } else {
                    _this.currentEquipment.materias[index].attribute = attributeNumber as number;
                }
                
                _this.currentEquipment.populateAttributes();
                _this.currentSet.calculate();

                _this.refreshAttributeTable();
                _this.refreshAvailability();
            }
        });

        if (this.currentEquipment.materias[index].materiaType === MateriaType.None) {
            this.updateAttributeDropdown(index, -1);
        } else {
            this.updateAttributeDropdown(index, this.currentEquipment.materias[index].attribute as number);
        }
    }

    updateAttributeDropdown(index: number, attributeNumber: number): void {
        $('#attribute-dropdown-' + index).dropdown('set selected', JSON.stringify({
            index: index,
            attribute: attributeNumber
        }) + 'string');
    }

    initializeMateriaTypeDropdown($event: any, index: number): void {
        while (this.materiaTypeNumbers.length <= index) {
            this.materiaTypeNumbers.push(0);
        }

        const _this: EditMateriaComponent = this;

        $($event.element).dropdown({
            onChange: function (value: string, text: any, $selectedItem: any) {
                let object: any = JSON.parse(value.substring(0, value.length - 6));
                let index: number = parseInt(object['index']);
                let materiaTypeNumber: number = parseInt(object['materiaType']);

                _this.materiaTypeNumbers[index] = materiaTypeNumber;

                if (materiaTypeNumber === -1) {
                    if (_this.attributeNumbers.length <= index || _this.attributeNumbers[index] !== -1) {
                        _this.updateAttributeDropdown(index, -1);
                    }
                    _this.currentEquipment.materias[index].materiaType = MateriaType.None;
                } else {
                    _this.currentEquipment.materias[index].materiaType = materiaTypeNumber as number;
                }
                
                _this.currentEquipment.populateAttributes();
                _this.currentSet.calculate();
                
                _this.refreshAttributeTable();
                _this.refreshAvailability();
            }
        });

        if (this.currentEquipment.materias[index].materiaType === MateriaType.None) {
            this.updateMateriaTypeDropdown(index, -1);
        } else {
            this.updateMateriaTypeDropdown(index, this.currentEquipment.materias[index].materiaType as number);
        }
    }

    updateMateriaTypeDropdown(index: number, materiaTypeNumber: number): void {
        $('#materia-type-dropdown-' + index).dropdown('set selected', JSON.stringify({
            index: index,
            materiaType: materiaTypeNumber
        }) + 'string');
    }

    stringify(object: any): string {
        return JSON.stringify(object);
    }

    goBack(): void {
        this.bisSetService.currentEquipment = null;

        this.router.navigate(['/bis-calculator/edit-set']);
    }
}

class AvailableMateria {
    attributes: Attribute[];
    attributeMateriaNames: string[]
    attributeDisplayNames: string[];
    attributeIsAvailable: boolean[];

    materiaTypes: MateriaType[];
    materiaTypeDisplayNames: string[];

    isForbidden: boolean;

    constructor() {
        this.attributes = [];
        this.attributeMateriaNames = [];
        this.attributeDisplayNames = [];
        this.attributeIsAvailable = [];

        this.materiaTypes = [];
        this.materiaTypeDisplayNames = [];

        this.isForbidden = false;
    }
}

declare let ga: {
    (s1: any, s2: any, s3?: any): any;
}

declare let $: {
    (selector: any): any;
}