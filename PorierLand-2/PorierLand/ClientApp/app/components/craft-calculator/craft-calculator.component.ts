import { Component, OnInit, OnDestroy, HostListener } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';

import {
    Accordion,
    CraftCalculatorService,
    Gathering,
    GatheringPoint,
    Item,
    ItemType,
    Job,
    Material,
    Place,
    Recipe,
    Shop
} from './craft-calculator.service';

import { AccordionComponent } from './accordion.component';

@Component({
    selector: 'app-craft-calculator',
    templateUrl: './craft-calculator.component.html',
    animations: [
        trigger('appearTrigger', [
            state('in', style({
                transform: 'scale(1)'
            })),
            transition('void => *', [
                style({
                    transform: 'scale(0)'
                }),
                animate(200)
            ]),
            transition('* => void', [
                animate(200, style({
                    transform: 'scale(0)'
                }))
            ])
        ]),
        trigger('startTrigger', [])
    ]
})
export class CraftCalculatorComponent implements OnInit, OnDestroy {
    loading = false;
    searchText = '';
    searchedItem: Item = new Item();

    autoGathering: boolean = false;
    autoBuying: boolean = false;
    getCrystals: boolean = false;

    accordionComponent: AccordionComponent = new AccordionComponent();

    selectedGatheringItem: Item = new Item();
    selectedGathering: Gathering =
    {
        gatheringJob: new Job()
    } as Gathering;
    gatheringTable: GatheringPoint[][] = [];

    selectedBuyingItem: Item = new Item();
    buyingTable: Place[][] = [];

    requiredItems: Item[] = [];
    craftingItems: Item[] = [];
    gatheringAccordions: Accordion[] = [];
    buyingAccordions: Accordion[] = [];

    @HostListener('window:unload')
    unload(): void {
        this.save();
    }

    ngOnDestroy(): void {
        this.save();
    }
    
    load(): void {
        if (typeof localStorage === 'undefined') return;

        this.loading = true;

        const _this: CraftCalculatorComponent = this;

        let requiredItems: Item[] = [];
        let craftingItems: Item[] = [];
        let gatheringAccordions: Accordion[] = [];
        let buyingAccordions: Accordion[] = [];

        let promise: Promise<void> = Promise.resolve();

        let savedRequiredItems: string | null = localStorage.getItem('requiredItems');

        if (savedRequiredItems) {
            let processedRequiredItems: any[] = JSON.parse(savedRequiredItems);

            processedRequiredItems.forEach(function (processed: any): void {
                promise = promise.then(function (): Promise<Item | null> {
                    return _this.populateItem(processed);
                }).then(function (item: Item | null): void {
                    if (item) {
                        requiredItems.push(item);
                    }
                });
            });
        }

        let savedCraftingItems: string | null = localStorage.getItem('craftingItems');

        if (savedCraftingItems) {
            let processedCraftingItems: any[] = JSON.parse(savedCraftingItems);

            processedCraftingItems.forEach(function (processed: any): void {
                promise = promise.then(function (): Promise<Item | null> {
                    return _this.populateItem(processed);
                }).then(function (item: Item | null): void {
                    if (item) {
                        craftingItems.push(item);
                    }
                });
            });
        }

        let savedGatheringAccordions: string | null = localStorage.getItem('gatheringAccordions');

        if (savedGatheringAccordions) {
            let processedGatheringAccordions: any[] = JSON.parse(savedGatheringAccordions);

            processedGatheringAccordions.forEach(function (processed: any): void {
                promise = promise.then(function (): Promise<Accordion | null> {
                    return _this.populateAccordion(processed);
                }).then(function (accordion: Accordion | null): void {
                    if (accordion) {
                        gatheringAccordions.push(accordion);
                    }
                });
            });
        }

        let savedBuyingAccordions: string | null = localStorage.getItem('buyingAccordions');

        if (savedBuyingAccordions) {
            let processedBuyingAccordions: any[] = JSON.parse(savedBuyingAccordions);

            processedBuyingAccordions.forEach(function (processed: any): void {
                promise = promise.then(function (): Promise<Accordion | null> {
                    return _this.populateAccordion(processed);
                }).then(function (accordion: Accordion | null): void {
                    if (accordion) {
                        buyingAccordions.push(accordion);
                    }
                });
            });
        }

        promise.then(function (): void {
            requiredItems.forEach(function (item: Item): void {
                item.itemType = ItemType.Required;
            });

            craftingItems.forEach(function (item: Item): void {
                item.itemType = ItemType.Crafting;
            });

            gatheringAccordions.forEach(function (accordion: Accordion): void {
                _this.setItemTypes(accordion, ItemType.Gathering);
            });

            buyingAccordions.forEach(function (accordion: Accordion): void {
                _this.setItemTypes(accordion, ItemType.Buying);
            });
        }).then(function (): void {
            gatheringAccordions.forEach(function (accordion: Accordion): void {
                _this.connectAccordions(accordion);
            });

            buyingAccordions.forEach(function (accordion: Accordion): void {
                _this.connectAccordions(accordion);
            });
        }).then(function (): void {
            let allItems: Item[] = requiredItems;

            allItems = allItems.concat(craftingItems);

            allItems = allItems.concat(_this.getAllItems(gatheringAccordions));

            allItems = allItems.concat(_this.getAllItems(buyingAccordions));

            allItems.forEach(function (item: Item): void {
                let candidates: Item[] = [];

                item.childMaterials.forEach(function (material: Material): void {
                    switch (material.childItem.itemType) {
                        case ItemType.Required:
                            candidates = requiredItems;
                            break;
                        case ItemType.Crafting:
                            candidates = craftingItems;
                            break;
                        case ItemType.Gathering:
                            candidates = _this.getAllItems(gatheringAccordions);
                            break;
                        case ItemType.Buying:
                            candidates = _this.getAllItems(buyingAccordions);
                            break;
                    }

                    candidates.forEach(function (_item: Item): void {
                        if (_item.key === material.childItem.key) {
                            material.childItem = _item;
                            material.parentItem = item;

                            if (!_item.parentMaterials) {
                                _item.parentMaterials = [];
                            }

                            _item.parentMaterials.push(material);
                        }
                    });
                });
            });

            _this.requiredItems = requiredItems;
            _this.craftingItems = craftingItems;
            _this.gatheringAccordions = gatheringAccordions;
            _this.buyingAccordions = buyingAccordions;

            _this.loading = false;
        });
    }

    save(): void {
        if (typeof localStorage === 'undefined') return;

        const _this: CraftCalculatorComponent = this;

        let requiredItems: any[] = [];
        this.requiredItems.forEach(function (item: Item): void {
            requiredItems.push(_this.processItem(item));
        });
        localStorage.setItem('requiredItems', JSON.stringify(requiredItems));

        let craftingItems: any[] = [];
        this.craftingItems.forEach(function (item: Item): void {
            craftingItems.push(_this.processItem(item));
        });
        localStorage.setItem('craftingItems', JSON.stringify(craftingItems));

        let gatheringAccordions: any[] = [];
        this.gatheringAccordions.forEach(function (accordion: Accordion): void {
            gatheringAccordions.push(_this.processAccordion(accordion));
        });
        localStorage.setItem('gatheringAccordions', JSON.stringify(gatheringAccordions));

        let buyingAccordions: any[] = [];
        this.buyingAccordions.forEach(function (accordion: Accordion): void {
            buyingAccordions.push(_this.processAccordion(accordion));
        });
        localStorage.setItem('buyingAccordions', JSON.stringify(buyingAccordions));
    }

    processItem(item: Item): any {
        let processed: any = {
            key: item.key,
            requiredAmount: item.requiredAmount,
            minimumAmount: item.minimumAmount,
            prepared: item.prepared,
            craftNumber: item.craftNumber,
            selectedRecipeKey: (item.selectedRecipe) ? item.selectedRecipe.key : -1,
            selectedGatheringJobKey: (item.selectedGatheringJob) ? item.selectedGatheringJob.key : -1,
            childMaterials: []
        };

        item.childMaterials.forEach(function (material: Material): void {
            processed.childMaterials.push({
                childItemKey: material.childItem.key,
                childItemType: material.childItem.itemType,
                requiredAmount: material.requiredAmount
            });
        });

        return processed;
    }

    populateItem(processed: any): Promise<Item | null> {
        const _this: CraftCalculatorComponent = this;

        if (!processed) return Promise.resolve(null);
        if (!processed.key) return Promise.resolve(null);

        return _this.craftCalculatorService.getItemByKey(processed.key).then(function (item: Item): Item {
            item.requiredAmount = processed.requiredAmount;
            item.minimumAmount = processed.minimumAmount;
            item.prepared = processed.prepared;
            item.craftNumber = processed.craftNumber;
            item.parentMaterials = [];
            item.childMaterials = [];

            processed.childMaterials.forEach(function (childMaterial: any): void {
                item.childMaterials.push({
                    childItem: {
                        key: childMaterial.childItemKey,
                        itemType: childMaterial.childItemType
                    } as Item,
                    requiredAmount: childMaterial.requiredAmount
                } as Material);
            });

            if (processed.selectedRecipeKey !== -1) {
                item.recipes.forEach(function (recipe: Recipe): void {
                    if (recipe.key === processed.selectedRecipeKey) {
                        item.selectedRecipe = recipe;
                    }
                });
            }

            if (processed.selectedGatheringJobKey !== -1) {
                item.gatherings.forEach(function (gathering: Gathering): void {
                    if (gathering.gatheringJob.key === processed.selectedGatheringJobKey) {
                        item.selectedGatheringJob = gathering.gatheringJob;
                    }
                });
            }

            return item;
        });
    }

    processAccordion(accordion: Accordion): any {
        let processed: any = {
            key: accordion.key,
            placeKey: accordion.placeKey,
            placeNameKo: accordion.placeNameKo,
            placeNameEn: accordion.placeNameEn,
            activeItemCount: accordion.activeItemCount,
            childAccordions: [],
            items: [],
            prepared: accordion.prepared
        };
        
        const _this: CraftCalculatorComponent = this;

        accordion.childAccordions.forEach(function (_accordion: Accordion): void {
            processed.childAccordions.push(_this.processAccordion(_accordion));
        });

        accordion.items.forEach(function (item: Item): void {
            processed.items.push(_this.processItem(item));
        });

        return processed;
    }

    populateAccordion(processed: any): Promise<Accordion | null> {
        if (!processed) return Promise.resolve(null);

        let accordion: Accordion = new Accordion();
        accordion.key = processed.key;
        accordion.placeKey = processed.placeKey;
        accordion.placeNameKo = processed.placeNameKo;
        accordion.placeNameEn = processed.placeNameEn;
        accordion.activeItemCount = processed.activeItemCount;
        accordion.childAccordions = [];
        accordion.items = [];
        accordion.prepared = processed.prepared;

        const _this: CraftCalculatorComponent = this;

        let promise: Promise<void> = Promise.resolve();

        processed.childAccordions.forEach(function (childAccordion: any): void {
            promise = promise.then(function (): Promise<Accordion | null> {
                return _this.populateAccordion(childAccordion);
            }).then(function (_accordion: Accordion | null): void {
                if (_accordion) {
                    accordion.childAccordions.push(_accordion);
                }
            });
        });

        processed.items.forEach(function (item: any): void {
            promise = promise.then(function (): Promise<Item | null> {
                return _this.populateItem(item);
            }).then(function (item: Item): void {
                if (item) {
                    accordion.items.push(item);
                }
            });
        });

        return promise.then(function (): Accordion {
            return accordion;
        });
    }

    setItemTypes(accordion: Accordion, itemType: ItemType): void {
        accordion.items.forEach(function (item: Item): void {
            item.itemType = itemType;
        });

        const _this: CraftCalculatorComponent = this;

        accordion.childAccordions.forEach(function (childAccordion: Accordion): void {
            _this.setItemTypes(childAccordion, itemType);
        });
    }

    getAllItems(accordions: Accordion[]): Item[] {
        const _this: CraftCalculatorComponent = this;

        let items: Item[] = [];

        accordions.forEach(function (accordion: Accordion): void {
            accordion.items.forEach(function (item: Item): void {
                items.push(item);
            });

            items = items.concat(_this.getAllItems(accordion.childAccordions));
        });
        
        return items;
    }

    connectAccordions(accordion: Accordion): void {
        const _this: CraftCalculatorComponent = this;

        accordion.childAccordions.forEach(function (childAccordion: Accordion): void {
            childAccordion.parentAccordion = accordion;
            _this.connectAccordions(childAccordion);
        });

        accordion.items.forEach(function (item: Item): void {
            item.parentAccordion = accordion;
        });
    }
    
    constructor(
        private craftCalculatorService: CraftCalculatorService
    ) { }

    ngOnInit(): void {
		if (typeof window === 'undefined') return;

		ga('create', 'UA-98394538-1', 'auto');
		ga('send', 'pageview');
        
        $('#item-search').search.settings.templates.message = function (message: any, type: any) {
            return `
                <div class="message empty">
                    <div class="header">아무것도 검색되지 않았어요!</div>
                    <div class="description">아이템 이름을 제대로 기입했는지 확인해주셔요!</div>
                </div>
            `;
        };

        const _this: CraftCalculatorComponent = this;

        $('#item-search').search({
            apiSettings: {
                url: '/api/craft_calculator/items?name={query}'
            },
            maxResults: 100,
            onSelect: function (result: any, response: any) {
                _this.loading = true;
                _this.craftCalculatorService.getItemByKey(result.price)
                    .then(function (item: Item) {
                        if (!item) {
                            _this.loading = false;
                            $('#error-modal').modal('show');
                            return;
                        }

                        item.requiredAmount = 1;
                        item.minimumAmount = 0;

                        item.prepared = false;
                        item.itemType = ItemType.Required;

                        item.parentMaterials = [];
                        item.childMaterials = [];

                        for (const gathering of item.gatherings) {
                            gathering.minLevel = _this.pickMinimumGatheringPoint(gathering).level;
                        }

                        _this.searchedItem = item;

                        for (const requiredItem of _this.requiredItems) {
                            if (requiredItem.key === _this.searchedItem.key) {
                                requiredItem.requiredAmount += _this.searchedItem.requiredAmount;
                                _this.searchedItem = new Item();
                                _this.searchText = '';
                                _this.loading = false;
                                return;
                            }
                        }

                        _this.requiredItems.push(_this.searchedItem);
                        _this.searchedItem = new Item();
                        _this.searchText = '';
                        _this.save();
                        _this.loading = false;
                    });
            }
        });

        this.load();
    }

    processRowSpansForGathering(gathering: Gathering): void {
        for (const gatheringPoint of gathering.gatheringPoints) {
            this.processRowSpanForGatheringPoint(gatheringPoint);
        }
    }

    processRowSpanForGatheringPoint(gatheringPoint: GatheringPoint): number {
        if (gatheringPoint.children.length === 0) {
            gatheringPoint.rowSpan = 1;
        } else {
            gatheringPoint.rowSpan = 0;

            for (const child of gatheringPoint.children) {
                gatheringPoint.rowSpan += this.processRowSpanForGatheringPoint(child);
            }
        }

        return gatheringPoint.rowSpan;
    }

    processGatheringIntoTable(gathering: Gathering): void {
        this.gatheringTable = [];

        for (const gatheringPoint of gathering.gatheringPoints) {
            this.gatheringTable.push([]);
            this.processGatheringPointIntoTable(gatheringPoint);
        }
    }

    processGatheringPointIntoTable(gatheringPoint: GatheringPoint): void {
        let existingAccordion: Accordion | null = this.findGatheringAccordionRecursively(gatheringPoint);

        if (existingAccordion) {
            gatheringPoint.existing = true;
        } else {
            gatheringPoint.existing = false;
        }

        this.gatheringTable[this.gatheringTable.length - 1].push(gatheringPoint);

        if (gatheringPoint.children.length > 0) {
            for (const child of gatheringPoint.children) {
                this.processGatheringPointIntoTable(child);
                this.gatheringTable.push([]);
            }

            this.gatheringTable.pop();
        }
    }

    processRowSpanForPlace(place: Place): number {
        place.rowSpan = 0;

        if (place.children.length === 0) {
            place.rowSpan = 1;
        } else {
            for (const child of place.children) {
                place.rowSpan += this.processRowSpanForPlace(child);
            }
        }

        return place.rowSpan;
    }

    processPlaceIntoTable(place: Place): void {
        let existingAccordion: Accordion | null = this.findPlaceAccordionRecursively(place);

        if (existingAccordion) {
            place.existing = true;
        } else {
            place.existing = false;
        }

        this.buyingTable[this.buyingTable.length - 1].push(place);

        if (place.children.length > 0) {
            for (const child of place.children) {
                this.processPlaceIntoTable(child);
                this.buyingTable.push([]);
            }

            this.buyingTable.pop();
        }
    }

    processShopsIntoTable(shops: Shop[]): void {
        let allPlaces: Place[] = [];
        let topPlaces: Place[] = [];

        for (const shop of shops) {
            for (const npc of shop.npcs) {
                npc.shop = shop;

                let place: Place = {
                    key: -1,
                    nameKo: '',
                    nameEn: '',
                    rowSpan: 0,
                    existing: false,
                    parent: npc.place,
                    children: [],
                    npc: npc
                };
                
                allPlaces.push(place);

                while (place.parent != null) {
                    place.parent.children = [];
                    place.parent.children.push(place);
                    place = place.parent;
                    allPlaces.push(place);
                }

                topPlaces.push(place);
            }
        }
        
        let processedPlaces: Place[] = [];

        for (const place of allPlaces) {
            if (place.key === -1) {
                processedPlaces.push(place);
                continue;
            }

            let existingPlace: Place | null = null;

            for (const _place of processedPlaces) {
                if (_place.key === place.key) {
                    existingPlace = _place;
                    break;
                }
            }

            if (existingPlace) {
                for (const child of place.children) {
                    existingPlace.children.push(child);
                    child.parent = existingPlace;
                }
                
                if (place.parent) {
                    place.parent.children.splice(place.parent.children.indexOf(place), 1);
                } else {
                    topPlaces.splice(topPlaces.indexOf(place), 1);
                }
            } else {
                processedPlaces.push(place);
            }
        }

        let npcPlaces: Place[] = [];

        for (const place of processedPlaces) {
            if (place.key === -1) {
                let alreadyExists: boolean = false;

                for (const _place of npcPlaces) {
                    if (_place.key === place.parent.key) {
                        alreadyExists = true;
                        break;
                    }
                }

                if (!alreadyExists) {
                    npcPlaces.push(place.parent);
                }
            }
        }

        for (const place of npcPlaces) {
            let places: Place[] = [];

            for (const child of place.children) {
                let isExisting: boolean = false;

                for (const _place of places) {
                    if (_place.npc.key === child.npc.key) {
                        isExisting = true;
                        break;
                    }
                }

                if (!isExisting) {
                    places.push(child);
                }
            }

            place.children = places;
        }

        for (const place of topPlaces) {
            this.processRowSpanForPlace(place);
        }

        this.buyingTable = [];

        for (const place of topPlaces) {
            this.buyingTable.push([]);
            this.processPlaceIntoTable(place);
        }
    }

    pickPlace(shops: Shop[]): Place {
        let npcPlaces: Place[] = [];

        for (const shop of shops) {
            for (const npc of shop.npcs) {
                npc.shop = shop;

                let place: Place = {
                    key: -1,
                    nameKo: '',
                    nameEn: '',
                    rowSpan: 0,
                    existing: false,
                    parent: npc.place,
                    children: [],
                    npc: npc
                };

                npcPlaces.push(place);
            }
        }

        for (const npcPlace of npcPlaces) {
            let places: Place[] = [];
            let currentPlace: Place = npcPlace;

            while (currentPlace != null) {
                places.push(currentPlace);
                currentPlace = currentPlace.parent;
            }

            for (const place of places) {
                if (this.findPlaceAccordionRecursively(place)) {
                    return npcPlace;
                }
            }
        }

        return npcPlaces[0];
    }
    
    popupButtonTriggerStarted($event: any): void {
        $($event.element).popup({
            inline: true,
            exclusive: true,
            variation: 'basic inverted'
        });
    }

    mergeParents(item1: Item, item2: Item): void {
        for (const material of item2.parentMaterials) {
            let alreadyExists: boolean = false;

            for (const _material of item1.parentMaterials) {
                if (_material.parentItem === material.parentItem) {
                    alreadyExists = true;
                    break;
                }
            }

            if (!alreadyExists) {
                material.childItem = item1;
                item1.parentMaterials.push(material);
            }
        }

        item2.parentMaterials = [];
    }

    reset(): void {
        this.searchText = '';

        this.requiredItems = [];
        this.craftingItems = [];
        this.gatheringAccordions = [];
        this.buyingAccordions = [];
    }

    calculateCraftNumber(item: Item): void {
        item.craftNumber = Math.ceil(item.requiredAmount / item.selectedRecipe.totalCrafted);
    }

    toggleItem(item: Item): void {
        item.prepared = !item.prepared;
    }

    findRequiredItem(item: Item): Item | null {
        for (const requiredItem of this.requiredItems) {
            if (requiredItem.key === item.key) {
                return requiredItem;
            }
        }

        return null;
    }

    findCraftingItem(item: Item, recipeKey: number): Item | null {
        for (const craftingItem of this.craftingItems) {
            if (craftingItem.key === item.key && craftingItem.selectedRecipe.key === recipeKey) {
                return craftingItem;
            }
        }

        return null;
    }

    pickMinimumGatheringPoint(gathering: Gathering): GatheringPoint {
        let gatheringPoints: GatheringPoint[] = [];

        for (const gatheringPoint of gathering.gatheringPoints) {
            gatheringPoints = this.collectGatheringPointsWithLevel(gatheringPoint, gatheringPoints);
        }

        let gatheringPoint: GatheringPoint = gatheringPoints[0];

        for (const _gatheringPoint of gatheringPoints) {
            if (_gatheringPoint.level < gatheringPoint.level) {
                gatheringPoint = _gatheringPoint;
            }
        }

        return gatheringPoint;
    }

    collectGatheringPointsWithLevel(gatheringPoint: GatheringPoint, gatheringPoints: GatheringPoint[]): GatheringPoint[] {
        if (gatheringPoint.level) {
            gatheringPoints.push(gatheringPoint);
        }

        for (const child of gatheringPoint.children) {
            gatheringPoints = this.collectGatheringPointsWithLevel(child, gatheringPoints);
        }

        return gatheringPoints;
    }
    
    findGatheringPointRouteFromGathering(gathering: Gathering, target: GatheringPoint): GatheringPoint[] {
        let route: GatheringPoint[] = [];

        for (const gatheringPoint of gathering.gatheringPoints) {
            this.findGatheringPoint(route, gatheringPoint, target);
        }

        return route;
    }

    findGatheringPoint(route: GatheringPoint[], gatheringPoint: GatheringPoint, target: GatheringPoint): boolean {
        if (gatheringPoint === target) {
            route.push(gatheringPoint);
            return true;
        } else {
            for (const child of gatheringPoint.children) {
                if (this.findGatheringPoint(route, child, target)) {
                    route.push(gatheringPoint);
                    return true;
                }
            }
        }

        return false;
    }

    findGatheringAccordionRecursively(target: GatheringPoint): Accordion | null {
        if (target.placeKey === null) return null;

        let result: Accordion | null = null;

        for (const accordion of this.gatheringAccordions) {
            result = this.findGatheringAccordion(target, accordion);

            if (result != null) {
                return result;
            }
        }

        return result;
    }

    findGatheringAccordion(target: GatheringPoint, accordion: Accordion): Accordion | null {
        if (target.placeKey === accordion.placeKey) {
            return accordion;
        } else if (accordion.childAccordions.length === 0) {
            return null;
        } else {
            let result: Accordion | null = null;

            for (const childAccordion of accordion.childAccordions) {
                result = this.findGatheringAccordion(target, childAccordion);

                if (result) {
                    return result;
                }
            }

            return result;
        }
    }

    findPlaceAccordionRecursively(target: Place): Accordion | null {
        let result: Accordion | null = null;

        for (const accordion of this.buyingAccordions) {
            result = this.findPlaceAccordion(target, accordion);

            if (result != null) {
                return result;
            }
        }
        
        return result;
    }

    findPlaceAccordion(target: Place, accordion: Accordion): Accordion | null {
        if (target.key === -1) {
            if (accordion.placeKey === -1 && accordion.key === target.npc.key) {
                return accordion;
            }
        } else {
            if (target.key === accordion.placeKey) {
                return accordion;
            }

            if (accordion.childAccordions.length === 0) {
                return null;
            }

        }

        let result: Accordion | null = null;

        for (const childAccordion of accordion.childAccordions) {
            result = this.findPlaceAccordion(target, childAccordion);

            if (result) {
                return result;
            }
        }

        return result;
    }

    mergeNullPlaces(accordions: Accordion[]): void {
        for (const accordion of accordions) {
            let nullAccordions: Accordion[] = [];

            for (const child of accordion.childAccordions) {
                if (child.placeKey === null) {
                    nullAccordions.push(child);
                }
            }

            if (nullAccordions.length > 1) {
                let nullAccordion: Accordion = nullAccordions[0];

                for (let i = 1; i < nullAccordions.length; i++) {
                    accordion.childAccordions.splice(accordion.childAccordions.indexOf(nullAccordions[i]), 1);

                    for (const child of nullAccordions[i].childAccordions) {
                        child.parentAccordion = nullAccordion;
                        nullAccordion.childAccordions.push(child);
                    }

                    for (const item of nullAccordions[i].items) {
                        let selectedItem: Item | null = null;

                        for (const _item of nullAccordion.items) {
                            if (_item.key === item.key && _item.selectedGatheringJob.key === item.selectedGatheringJob.key) {
                                selectedItem = _item;
                                break;
                            }
                        }

                        if (selectedItem) {
                            this.mergeParents(selectedItem, item);
                            this.updateItem(selectedItem, item.requiredAmount, item.minimumAmount);
                        } else {
                            selectedItem = item;
                            selectedItem.prepared = false;
                            selectedItem.parentAccordion = nullAccordion;
                            nullAccordion.items.push(selectedItem);
                        }
                    }
                }
            }

            this.mergeNullPlaces(accordion.childAccordions);
        }
    }

    recountActiveItem(accordions: Accordion[]): void {
        for (let accordion of accordions) {
            this.recountActiveItem(accordion.childAccordions);

            accordion.activeItemCount = 0;
            
            for (let item of accordion.items) {
                if (!item.prepared) accordion.activeItemCount++;
            }

            for (let child of accordion.childAccordions) {
                accordion.activeItemCount += child.activeItemCount;
            }

            accordion.prepared = accordion.activeItemCount === 0;
        }
    }

    craftItem(item: Item, recipe: Recipe): void {
        this.loading = true;
        
        const _this: CraftCalculatorComponent = this;
        let promise: Promise<void> = Promise.resolve();
        let targetRequiredAmount: number = item.requiredAmount;
        let targetMinimumAmount: number = item.minimumAmount;

        let selectedCraftingItem: Item | null = this.findCraftingItem(item, recipe.key);

        if (selectedCraftingItem) {
            this.removeRequiredItem(item);
            this.mergeParents(selectedCraftingItem, item);
        } else {
            promise = promise.then(function (): Promise<Item[]> {
                return _this.craftCalculatorService.getItemsByRecipeKey(recipe.key, _this.getCrystals);
            }).then(function (items: Item[]): Promise<void> {
                if (!items) {
                    $('#error-modal').modal('show');
                    return Promise.resolve();
                }

                _this.removeRequiredItem(item);

                selectedCraftingItem = item;
                selectedCraftingItem.requiredAmount = 0;
                selectedCraftingItem.minimumAmount = 0;
                selectedCraftingItem.prepared = false;
                selectedCraftingItem.itemType = ItemType.Crafting;
                selectedCraftingItem.craftNumber = 0;
                selectedCraftingItem.selectedRecipe = recipe;

                _this.craftingItems.push(selectedCraftingItem);

                for (const item of items) {
                    let material: Material = new Material();
                    material.requiredAmount = item.requiredAmount;
                    material.parentItem = selectedCraftingItem as Item;

                    let selectedItem: Item | null = _this.findRequiredItem(item);

                    if (!selectedItem) {
                        selectedItem = item;
                        selectedItem.requiredAmount = 0;
                        selectedItem.minimumAmount = 0;
                        selectedItem.prepared = false;
                        selectedItem.itemType = ItemType.Required;
                        selectedItem.parentMaterials = [];
                        selectedItem.childMaterials = [];
                        
                        for (const gathering of selectedItem.gatherings) {
                            gathering.minLevel = _this.pickMinimumGatheringPoint(gathering).level;
                        }

                        _this.searchedItem = item;

                        _this.requiredItems.push(selectedItem);
                    }

                    material.childItem = selectedItem as Item;

                    selectedItem.parentMaterials.push(material);
                    (selectedCraftingItem as Item).childMaterials.push(material);
                }
                
                return Promise.resolve();
            });
        }

        promise.then(function () {
            if (selectedCraftingItem) {
                _this.updateItem(selectedCraftingItem as Item, targetRequiredAmount, targetMinimumAmount);
            }
            _this.save();
            _this.loading = false;
        });
    }

    gatherItem(item: Item, gathering: Gathering): void {
        this.selectedGatheringItem = item;
        this.selectedGatheringItem.selectedGatheringJob = gathering.gatheringJob;
        this.selectedGathering = gathering;

        if (!this.autoGathering) {
            this.processRowSpansForGathering(gathering);
            this.processGatheringIntoTable(gathering);

            $('#gathering-modal').modal({
                autofocus: false,
                observeChanges: true,
                keyboardShortcuts: false,
                closable: true
            }).modal('show');
        } else {
            this.selectGatheringData(this.pickMinimumGatheringPoint(this.selectedGathering));
        }
    }

    selectGatheringData(gatheringPoint: GatheringPoint): void {
        this.loading = true;

        this.removeRequiredItem(this.selectedGatheringItem);

        $('#gathering-modal').modal('hide');

        let route: GatheringPoint[] = this.findGatheringPointRouteFromGathering(this.selectedGathering, gatheringPoint);

        let i: number = 0;
        let selectedAccordion: Accordion | null = null;

        for (i = 0; i < route.length; i++) {
            selectedAccordion = this.findGatheringAccordionRecursively(route[i]);

            if (selectedAccordion) break;
        }

        if (!selectedAccordion) {
            i = i - 1;
            selectedAccordion = new Accordion();
            selectedAccordion.key = route[i].key;
            selectedAccordion.placeKey = route[i].placeKey;
            selectedAccordion.placeNameKo = route[i].placeNameKo;
            selectedAccordion.placeNameEn = route[i].placeNameEn;
            selectedAccordion.childAccordions = [];
            selectedAccordion.items = [];
            selectedAccordion.prepared = false;
            selectedAccordion.activeItemCount = 0;
            this.gatheringAccordions.push(selectedAccordion);
        }

        for (i = i - 1; i >= 0; i--) {
            let newAccordion = new Accordion();
            newAccordion.key = route[i].key;
            newAccordion.placeKey = route[i].placeKey;
            newAccordion.placeNameKo = route[i].placeNameKo;
            newAccordion.placeNameEn = route[i].placeNameEn;
            newAccordion.childAccordions = [];
            newAccordion.items = [];
            newAccordion.prepared = false;
            newAccordion.activeItemCount = 0;
            newAccordion.parentAccordion = selectedAccordion;
            selectedAccordion.childAccordions.push(newAccordion);
            selectedAccordion = newAccordion;
        }

        let selectedItem: Item | null = null;

        for (const item of selectedAccordion.items) {
            if (item.key === this.selectedGatheringItem.key && item.selectedGatheringJob.key === this.selectedGatheringItem.selectedGatheringJob.key) {
                selectedItem = item;
                break;
            }
        }

        if (selectedItem) {
            this.mergeParents(selectedItem, this.selectedGatheringItem);
            this.updateItem(selectedItem, this.selectedGatheringItem.requiredAmount, this.selectedGatheringItem.minimumAmount);
        } else {
            selectedItem = this.selectedGatheringItem;
            selectedItem.prepared = false;
            selectedItem.itemType = ItemType.Gathering;
            selectedItem.parentAccordion = selectedAccordion;
            selectedAccordion.items.push(selectedItem);
        }

        this.mergeNullPlaces(this.gatheringAccordions);
        this.recountActiveItem(this.gatheringAccordions);

        this.save();

        this.loading = false;
    }

    buyItem(item: Item): void {
        this.selectedBuyingItem = item;

        if (!this.autoBuying) {
            this.processShopsIntoTable(item.shops);

            $('#buying-modal').modal({
                autofocus: false,
                observeChanges: true,
                keyboardShortcuts: false,
                closable: true
            }).modal('show');
        } else {
            this.selectBuyingPlace(this.pickPlace(item.shops));
        }
    }

    selectBuyingPlace(place: Place): void {
        this.loading = true;

        this.removeRequiredItem(this.selectedBuyingItem);

        $('#buying-modal').modal('hide');

        let places: Place[] = [];
        let currentPlace: Place = place;

        while (currentPlace != null) {
            places.push(currentPlace);
            currentPlace = currentPlace.parent;
        }
        
        let i: number = 0;
        let selectedAccordion: Accordion | null = null;

        for (i = 0; i < places.length; i++) {
            selectedAccordion = this.findPlaceAccordionRecursively(places[i]);
            
            if (selectedAccordion) break;
        }
        
        if (!selectedAccordion) {
            i = i - 1;
            selectedAccordion = new Accordion();
            selectedAccordion.key = places[i].key;
            selectedAccordion.placeKey = places[i].key;
            selectedAccordion.placeNameKo = places[i].nameKo;
            selectedAccordion.placeNameEn = places[i].nameEn;
            selectedAccordion.childAccordions = [];
            selectedAccordion.items = [];
            selectedAccordion.prepared = false;
            selectedAccordion.activeItemCount = 0;
            this.buyingAccordions.push(selectedAccordion);
        }

        for (i = i - 1; i > 0; i--) {
            let newAccordion = new Accordion();
            newAccordion.key = places[i].key;
            newAccordion.placeKey = places[i].key;
            newAccordion.placeNameKo = places[i].nameKo;
            newAccordion.placeNameEn = places[i].nameEn;
            newAccordion.childAccordions = [];
            newAccordion.items = [];
            newAccordion.prepared = false;
            newAccordion.activeItemCount = 0;
            newAccordion.parentAccordion = selectedAccordion;
            selectedAccordion.childAccordions.push(newAccordion);
            selectedAccordion = newAccordion;
        }
        
        if (selectedAccordion.placeKey !== -1) {
            let newAccordion = new Accordion();
            newAccordion.key = places[0].npc.key;
            newAccordion.placeKey = -1;
            newAccordion.placeNameKo = places[0].npc.nameKo;
            newAccordion.placeNameEn = places[0].npc.nameEn;
            newAccordion.childAccordions = [];
            newAccordion.items = [];
            newAccordion.prepared = false;
            newAccordion.activeItemCount = 0;
            newAccordion.parentAccordion = selectedAccordion;
            selectedAccordion.childAccordions.push(newAccordion);
            selectedAccordion = newAccordion;
        }
        
        let selectedItem: Item | null = null;

        for (const item of selectedAccordion.items) {
            if (item.key === this.selectedBuyingItem.key) {
                selectedItem = item;
                break;
            }
        }

        if (selectedItem) {
            this.mergeParents(selectedItem, this.selectedBuyingItem);
            this.updateItem(selectedItem, this.selectedBuyingItem.requiredAmount, this.selectedBuyingItem.minimumAmount);
        } else {
            selectedItem = this.selectedBuyingItem;
            selectedItem.prepared = false;
            selectedItem.itemType = ItemType.Buying;
            selectedItem.parentAccordion = selectedAccordion;
            selectedAccordion.items.push(selectedItem);
        }
        
        this.recountActiveItem(this.buyingAccordions);
        this.save();

        this.loading = false;
    }

    updateItem(item: Item, requiredAmount: number, minimumAmount: number): void {
        item.prepared = false;
        
        item.requiredAmount += requiredAmount;
        item.minimumAmount += minimumAmount;

        switch (item.itemType) {
            case ItemType.Buying:
                this.accordionComponent.topAccordions = this.buyingAccordions;
                if (item.parentAccordion) {
                    this.accordionComponent.refreshAccordion(item.parentAccordion);
                }
                break;
            case ItemType.Gathering:
                this.accordionComponent.topAccordions = this.gatheringAccordions;
                if (item.parentAccordion) {
                    this.accordionComponent.refreshAccordion(item.parentAccordion);
                }
                break;
        }

        if (item.itemType === ItemType.Crafting) {
            const prevCraftNumber: number = item.craftNumber;
            this.calculateCraftNumber(item);
            const diffCraftNumber: number = item.craftNumber - prevCraftNumber;

            if (diffCraftNumber !== 0) {
                let materials: Material[] = [];

                for (const childMaterial of item.childMaterials) {
                    materials.push(childMaterial);
                }

                for (const childMaterial of materials) {
                    this.updateItem(childMaterial.childItem, childMaterial.requiredAmount * diffCraftNumber, childMaterial.requiredAmount * diffCraftNumber);
                }
            }
        }

        if (item.requiredAmount === 0) {
            switch (item.itemType) {
                case ItemType.Required:
                    this.removeRequiredItem(item);
                    break;
                case ItemType.Crafting:
                    this.removeCraftingItem(item);
                    break;
                case ItemType.Buying:
                    this.accordionComponent.topAccordions = this.buyingAccordions;
                    this.accordionComponent.removeItem(item);
                    break;
                case ItemType.Gathering:
                    this.accordionComponent.topAccordions = this.gatheringAccordions;
                    this.accordionComponent.removeItem(item);
                    break;
            }

            for (const parentMaterial of item.parentMaterials) {
                if (parentMaterial.parentItem) {
                    parentMaterial.parentItem.childMaterials.splice(parentMaterial.parentItem.childMaterials.indexOf(parentMaterial), 1);
                }
            }

            item.parentMaterials = [];

            for (const childMaterial of item.childMaterials) {
                childMaterial.childItem.parentMaterials.splice(childMaterial.childItem.parentMaterials.indexOf(childMaterial), 1);
            }

            item.childMaterials = [];
        }

        
    }

    cancelCraftingItem(item: Item): void {
        this.loading = true;

        this.removeCraftingItem(item);

        let materials: Material[] = [];

        for (const childMaterial of item.childMaterials) {
            materials.push(childMaterial);
        }

        for (const childMaterial of materials) {
            this.updateItem(childMaterial.childItem, childMaterial.requiredAmount * item.craftNumber * -1, childMaterial.requiredAmount * item.craftNumber * -1);
        }

        for (const childMaterial of item.childMaterials) {
            childMaterial.childItem.parentMaterials.splice(childMaterial.childItem.parentMaterials.indexOf(childMaterial), 1);
        }

        let targetRequiredAmount: number = item.requiredAmount;
        let targetMinimumAmount: number = item.minimumAmount;
        
        let selectedRequiredItem: Item | null = this.findRequiredItem(item);

        if (selectedRequiredItem) {
            for (const material of item.parentMaterials) {
                material.childItem = selectedRequiredItem;
                selectedRequiredItem.parentMaterials.push(material);
            }
        } else {
            selectedRequiredItem = item;
            selectedRequiredItem.requiredAmount = 0;
            selectedRequiredItem.minimumAmount = 0;
            selectedRequiredItem.prepared = false;
            selectedRequiredItem.itemType = ItemType.Required;
            selectedRequiredItem.craftNumber = 0;
            selectedRequiredItem.childMaterials = [];

            this.requiredItems.push(selectedRequiredItem);
        }

        this.updateItem(selectedRequiredItem, targetRequiredAmount, targetMinimumAmount);
        this.save();

        this.loading = false;
    }
    
    cancelItem(item: Item): void {
        this.loading = true;
        
        let targetRequiredAmount: number = item.requiredAmount;
        let targetMinimumAmount: number = item.minimumAmount;

        let selectedRequiredItem: Item | null = this.findRequiredItem(item);

        if (selectedRequiredItem) {
            for (const material of item.parentMaterials) {
                material.childItem = selectedRequiredItem;
                selectedRequiredItem.parentMaterials.push(material);
            }
        } else {
            selectedRequiredItem = item;
            selectedRequiredItem.requiredAmount = 0;
            selectedRequiredItem.minimumAmount = 0;
            selectedRequiredItem.prepared = false;
            selectedRequiredItem.itemType = ItemType.Required;
            selectedRequiredItem.craftNumber = 0;
            selectedRequiredItem.childMaterials = [];

            this.requiredItems.push(selectedRequiredItem);
        }

        this.updateItem(selectedRequiredItem, targetRequiredAmount, targetMinimumAmount);
        this.loading = false;
    }
    
    removeRequiredItem(requiredItem: Item): void {
        let index: number = this.requiredItems.indexOf(requiredItem);

        if (index !== -1) {
            this.requiredItems.splice(index, 1);
        }
    }

    removeCraftingItem(craftingItem: Item): void {
        let index: number = this.craftingItems.indexOf(craftingItem);

        if (index !== -1) {
            this.craftingItems.splice(index, 1);
        }
    }
}

declare let ga: {
    (s1: any, s2: any, s3?: any): any;
}

declare let $: {
    (selector: any): any;
}