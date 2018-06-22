import 'rxjs/add/operator/toPromise';

import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

@Injectable()
export class CraftCalculatorService {
    constructor(private http: Http) { }

    getItemByKey(key: number): Promise<Item> {
        return this.http.get('api/craft_calculator/item?key=' + key).toPromise().then(function (response: Response): Item {
            return response.json() as Item;
        });
    }

    getItemsByRecipeKey(recipeKey: number, getCrystals: boolean): Promise<Item[]> {
        return this.http.get('api/craft_calculator/items?recipe_key=' + recipeKey + '&get_crystals=' + getCrystals).toPromise().then(function (response: Response): Item[] {
            return response.json() as Item[];
        });
    }
}

export enum ItemType {
    Required = 0,
    Crafting = 1,
    Gathering = 2,
    Buying = 3
}

export class Item {
    key: number;
    nameKo: string;
    nameEn: string;
    iconPath: string;
    requiredAmount: number;
    minimumAmount: number;

    prepared: boolean;
    itemType: ItemType;

    craftNumber: number;
    selectedRecipe: Recipe;

    recipes: Recipe[];
    gatherings: Gathering[];
    selectedGatheringJob: Job;

    shops: Shop[];

    parentMaterials: Material[];
    childMaterials: Material[];

    parentAccordion: Accordion | null;
}

export class Recipe {
    key: number;
    level: number;
    totalCrafted: number;
    numStarsArray: number[];
    craftingJob: Job;
}

export class Job {
    key: number;
    nameKo: string;
    nameEn: string;
    iconPath: string;
}

export class Gathering {
    key: number;
    gatheringJob: Job;
    numStarsArray: number[];
    isHidden: boolean;
    gatheringSubCategory: GatheringSubCategory;
    minLevel: number;

    gatheringPoints: GatheringPoint[];
}

export class GatheringSubCategory {
    key: number;
    nameKo: string;
    nameEn: string;
}

export class GatheringPoint {
    key: number;
    level: number;
    placeKey: number;
    placeNameKo: string;
    placeNameEn: string;
    rowSpan: number;
    existing: boolean;

    children: GatheringPoint[];
}

export class Shop {
    key: number;
    nameKo: string;
    nameEn: string;
    beastTribe: BeastTribe;
    quest: Quest;
    npcs: Npc[];
}

export class BeastTribe {
    key: number;
    nameKo: string;
    nameEn: string;
}

export class Quest {
    key: number;
    nameKo: string;
    nameEn: string;
}

export class Npc {
    key: number;
    nameKo: string;
    nameEn: string;
    x: number;
    y: number;

    place: Place;
    shop: Shop;
}

export class Place {
    key: number;
    nameKo: string;
    nameEn: string;
    rowSpan: number;
    existing: boolean;

    parent: Place;
    children: Place[];

    npc: Npc;
}

export class Material {
    childItem: Item;
    parentItem: Item | null;
    requiredAmount: number;
}

export class Accordion {
    key: number;
    placeKey: number;
    placeNameKo: string;
    placeNameEn: string;
    activeItemCount: number;
    
    parentAccordion: Accordion | null;
    childAccordions: Accordion[];
    items: Item[];
    prepared: boolean;
}