import { Injectable } from '@angular/core';

import {
    Equipment,
    Set,
    Table
} from './bis-calculator.service';

@Injectable()
export class BisSetService {
    sets: Set[];

    currentSet: Set;
    currentTables: Table[];
    currentEquipment: Equipment | null;
    isEditing: boolean;
    currentPageYOffset: number;

    constructor() {
        this.sets = [];

        this.currentSet = new Set();
        this.currentTables = [];
        this.currentEquipment = null;
        this.isEditing = false;
        this.currentPageYOffset = 0;
    }
}