import 'rxjs/add/operator/toPromise';

import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';

@Injectable()
export class BisCalculatorService {
    constructor(private http: Http) { }

    getEquipments(job: Job, minLevel: number, maxLevel: number): Promise<Equipment[]> {
        return this.http.get('api/bis_calculator/equipments?job=' + job + '&minLevel=' + minLevel + '&maxLevel=' + maxLevel).toPromise().then(function (response: Response): Equipment[] {
            let payload = response.json() as any[];
            let equipments: Equipment[] = [];

            payload.forEach(function (value: any): void {
                let newEquipment: Equipment = new Equipment();
                newEquipment.fromObject(value);
                equipments.push(newEquipment);
            });

            return equipments;
        }).catch(function (error: any): Equipment[] {
            return [];
        });
    }

    postBisSets(sets: Set[]): Promise<string> {
        let payload: string[] = [];
        sets.forEach(function (set: Set): void {
            payload.push(set.toString());
        });

        let params = new URLSearchParams();
        params.set('payload', JSON.stringify(payload));

        let headers = new Headers();
        headers.set('Content-Type', 'application/x-www-form-urlencoded');
        
        return this.http.post('api/bis_calculator/bis-sets', params.toString(), { headers: headers }).toPromise().then(function (response: Response): string {
            return response.text();
        }).catch(function (error: any): string {
            return '';
        });
    }

    getBisSets(id: string): Promise<Set[]> {
        return this.http.get('api/bis_calculator/bis-sets?id=' + id).toPromise().then(function (response: Response): Set[] {
            let payload = response.text();
            let setStrings: string[] = JSON.parse(payload);
            let sets: Set[] = [];

            setStrings.forEach(function (setString: string): void {
                let newSet: Set = new Set();
                newSet.fromString(setString);
                sets.push(newSet);
            });

            return sets;
        }).catch(function (error: any): Set[] {
            return [];
        });
    }

    getEquipmentInfo(key: number): Promise<EquipmentInfo | null> {
        return this.http.get('api/bis_calculator/equipment-info?key=' + key).toPromise().then(function (response: Response): EquipmentInfo {
            return response.json() as EquipmentInfo;
        }).catch(function (error: any): null {
            return null;
        });
    }

    getMeals(minLevel: number, maxLevel: number): Promise<Meal[]> {
        return this.http.get('api/bis_calculator/meals?minLevel=' + minLevel + '&maxLevel=' + maxLevel).toPromise().then(function (response: Response): Meal[] {
            let payload = response.json() as any[];
            let meals: Meal[] = [];
            
            payload.forEach(function (value: any): void {
                let newMeal: Meal = new Meal();
                newMeal.fromObject(value);
                meals.push(newMeal);
            });

            return meals;
        }).catch(function (error: any): Meal[] {
            return [];
        });
    }
}

export const maxAttributeIndex: number = 12;
export enum Attribute {
    Strength = 1,
    Dexterity = 2,
    Intelligence = 3,
    Mind = 4,

    SkillSpeed = 5,
    SpellSpeed = 6,

    Vitality = 7,

    Tenacity = 8,
    Piety = 9,

    DirectHit = 10,
    CriticalHit = 11,
    Determination = 12
}

export const maxEquipCategoryIndex: number = 13;
export enum EquipCategory {
    Weapon = 1,
    Shield = 2,
    Head = 3,
    Body = 4,
    Hands = 5,
    Waist = 6,
    Legs = 7,
    Feet = 8,
    Earrings = 9,
    Necklace = 10,
    Bracelets = 11,
    LeftRing = 12,
    RightRing = 13
}

export enum ItemCategory {
    OneHandedArm = 1,
    TwoHandedArm = 2,
    Shield = 3,
    Head = 4,
    Body = 5,
    Hands = 6,
    Waist = 7,
    Legs = 8,
    Feet = 9,
    Earrings = 10,
    Necklace = 11,
    Bracelets = 12,
    Ring = 13
}

export enum Job {
    Paladin = 1,
    Warrior = 2,
    DarkKnight = 3,
    WhiteMage = 4,
    Scholar = 5,
    Astrologian = 6,
    Monk = 7,
    Dragoon = 8,
    Ninja = 9,
    Samurai = 10,
    Bard = 11,
    Machinist = 12,
    BlackMage = 13,
    Summoner = 14,
    RedMage = 15
}

export enum MateriaType {
    None = 0,
    I = 1,
    II = 2,
    III = 3,
    IV = 4,
    V = 5,
    VI = 6
}

export enum Race {
    Hyur1 = 1,
    Hyur2 = 2,
    Miqote1 = 3,
    Miqote2 = 4,
    Elezen1 = 5,
    Elezen2 = 6,
    Roegadyn1 = 7,
    Roegadyn2 = 8,
    Lalafell1 = 9,
    Lalafell2 = 10,
    Aura1 = 11,
    Aura2 = 12
}

export class Set {
    job: Job;
    race: Race;
    minLevel: number;
    maxLevel: number;

    equipments: Map<EquipCategory, Equipment>;

    hp: number;

    damageEstimate: number;
    globalCooldown: number;
    damageReduction: number;

    attributes: Map<Attribute, number>;
    relativeAttributes: Attribute[];

    damageAttribute: Attribute;
    speedAttribute: Attribute;

    attributeDisplays: AttributeDisplay[] = [];
    attributeValues: number[] = [];

    isTank: boolean = false;
    isHeal: boolean = false;
    isTankOrHeal: boolean = false;

    meal: Meal | null;

    requiredMaterias: string[];

    currentPreset: number;
    
    constructor() {
        this.job = Job.Warrior;
        this.race = Race.Miqote1;
        this.minLevel = 310;
        this.maxLevel = 345;

        this.equipments = new Map();

        this.meal = null;

        this.hp = 0;

        this.damageEstimate = 0;
        this.globalCooldown = 0;
        this.damageReduction = 0;

        this.attributes = new Map();
        this.relativeAttributes = [];

        this.damageAttribute = Attribute.Strength;
        this.speedAttribute = Attribute.SkillSpeed;

        this.requiredMaterias = [];

        this.currentPreset = 0;
    }

    clone(set: Set): void {
        this.job = set.job;
        this.race = set.race;
        this.minLevel = set.minLevel;
        this.maxLevel = set.maxLevel;

        if (set.meal) {
            this.meal = set.meal;
        } else {
            this.meal = null;
        }

        this.equipments = new Map();

        const _this: Set = this;

        set.equipments.forEach(function (equipment: Equipment, key: EquipCategory): void {
            let newEquipment: Equipment = new Equipment();
            newEquipment.fromObject(equipment.originalObject);
            equipment.materias.forEach(function (materia: Materia, index: number): void {
                newEquipment.materias[index].attribute = materia.attribute;
                newEquipment.materias[index].materiaType = materia.materiaType;
                newEquipment.materias[index].isForbidden = materia.isForbidden;
            });
            _this.equipments.set(key, newEquipment);
        });

        this.currentPreset = set.currentPreset;
    }

    toString(): string {
        let o: any = {
            job: this.job,
            race: this.race,
            minLevel: this.minLevel,
            maxLevel: this.maxLevel,
            equipments: [],
            currentPreset: this.currentPreset
        };

        if (this.meal) {
            o['meal'] = this.meal.toString();
        }

        this.equipments.forEach(function (equipment: Equipment, equipCategory: EquipCategory): void {
            o.equipments.push(JSON.stringify({
                equipment: equipment.toString(),
                equipCategory: equipCategory
            }));
        });

        return JSON.stringify(o);
    }
    
    fromString(s: string): void {
        let o: any = JSON.parse(s);

        this.job = o['job'];
        this.race = o['race'];
        this.minLevel = o['minLevel'];
        this.maxLevel = o['maxLevel'];

        if (o['meal']) {
            this.meal = new Meal();
            this.meal.fromString(o['meal']);
        } else {
            this.meal = null;
        }

        this.equipments = new Map();

        const _this: Set = this;

        o['equipments'].forEach(function (value: string): void {
            let o2: any = JSON.parse(value);
            let equipment: Equipment = new Equipment();
            equipment.fromString(o2['equipment']);
            _this.equipments.set(o2['equipCategory'], equipment);
        });

        if (o['currentPreset']) {
            this.currentPreset = o['currentPreset'];
        } else {
            this.currentPreset = 0;
        }
    }

    populateAttributeInfo(): void {
        if (!this.job) return;

        this.isTank = [Job.Paladin, Job.Warrior, Job.DarkKnight].indexOf(this.job) !== -1;
        this.isHeal = [Job.WhiteMage, Job.Scholar, Job.Astrologian].indexOf(this.job) !== -1;
        this.isTankOrHeal = this.isTank || this.isHeal;

        this.damageAttribute = jobToDamageAttribute.get(this.job) as Attribute;
        this.speedAttribute = jobToSpeedAttribute.get(this.job) as Attribute;

        this.relativeAttributes = [
            this.damageAttribute,
            this.speedAttribute,

            Attribute.Vitality,

            Attribute.DirectHit,
            Attribute.CriticalHit,
            Attribute.Determination
        ];

        if (this.job === Job.Paladin || this.job === Job.Warrior || this.job === Job.DarkKnight) {
            this.relativeAttributes.push(Attribute.Tenacity);
        } else if (this.job === Job.WhiteMage || this.job === Job.Scholar || this.job === Job.Astrologian) {
            this.relativeAttributes.push(Attribute.Piety);
        }

        this.relativeAttributes.sort(function (a: Attribute, b: Attribute): number {
            return a - b;
        });

        this.attributeDisplays = [];

        const _this: Set = this;

        this.relativeAttributes.forEach(function (value: Attribute): void {
            _this.attributeDisplays.push({
                attribute: value,
                displayName: attributeToDisplayName.get(value) as string
            });
        });
        
        this.attributeDisplays.sort(function (a: AttributeDisplay, b: AttributeDisplay): number {
            return a.attribute - b.attribute;
        });
    }
    
    calculate(): void {
        for (let i = 1; i <= maxAttributeIndex; i++) {
            let defaultValue: number = 0;

            if (defaultSubAttributes.has(i as Attribute)) {
                defaultValue = defaultSubAttributes.get(i as Attribute) as number;
            }

            if (this.job && this.race && defaultBaseAttributesByJob.has(this.job) && defaultBaseAttributesByRace.has(this.race)) {
                let jobMap: Map<Attribute, number> = defaultBaseAttributesByJob.get(this.job) as Map<Attribute, number>;
                let raceMap: Map<Attribute, number> = defaultBaseAttributesByRace.get(this.race) as Map<Attribute, number>;

                if (jobMap.has(i as Attribute) && raceMap.has(i as Attribute)) {
                    defaultValue = (jobMap.get(i as Attribute) as number) + (raceMap.get(i as Attribute) as number);
                }
            }

            this.attributes.set(i as Attribute, defaultValue);
        }

        const _this: Set = this;

        this.equipments.forEach(function (value: Equipment): void {
            value.populateAttributes();

            value.attributes.forEach(function (value: number, key: Attribute): void {
                _this.attributes.set(key, (_this.attributes.get(key) as number) + value);
            });

            value.materiaAttributes.forEach(function (value: number, key: Attribute): void {
                _this.attributes.set(key, (_this.attributes.get(key) as number) + value);
            });
        });

        if (this.meal) {
            this.meal.attributeActions.forEach(function (value: Action, key: Attribute): void {
                if (_this.attributes.has(key)) {
                    if (value.isFixed && value.fixedAmount !== -1) {
                        _this.attributes.set(key, (_this.attributes.get(key) as number) + value.fixedAmount);
                    } else if (!value.isFixed && value.rate !== -1 && value.limit !== -1) {
                        let origValue: number = _this.attributes.get(key) as number;
                        let addingValue: number = origValue * (value.rate / 100);
                        addingValue = Math.min(addingValue, value.limit);
                        _this.attributes.set(key, Math.floor(origValue + addingValue));
                    }
                }
            });
        }

        this.hp = Math.floor(3600 * (vitMod.get(this.job) as number) + 21.5 * ((this.attributes.get(Attribute.Vitality) as number) - 292));

        let weaponRatio: number = 0;

        if (this.equipments && this.equipments.has(EquipCategory.Weapon)) {
            let weaponDamage: number = (this.equipments.get(EquipCategory.Weapon) as Equipment).damage;

            if (weaponDamage > 0) {
                weaponRatio = weaponDamage + Math.floor((wpMod.get(this.job) as number) * 292);
            }
        }

        let mainAttributeRatio: number = 0;

        if (this.attributes && this.attributes.has(this.damageAttribute)) {
            mainAttributeRatio = (Math.floor(((this.attributes.get(this.damageAttribute) as number) - 292) * 10000 / (80 * 292)) + 100) / 100;
        }

        let directHitRatio: number = 0;

        if (this.attributes && this.attributes.has(Attribute.DirectHit)) {
            directHitRatio = Math.floor(((this.attributes.get(Attribute.DirectHit) as number) - 364) * 550 / 2170) / 1000 * 0.25 + 1
        }

        let criticalHitRatio: number = 0;

        if (this.attributes && this.attributes.has(Attribute.CriticalHit)) {
            criticalHitRatio = (Math.floor(200 * ((this.attributes.get(Attribute.CriticalHit) as number) - 364) / 2170 + 400) / 1000) * (Math.floor(200 * ((this.attributes.get(Attribute.CriticalHit) as number) - 364) / 2170 + 50) / 1000) + 1;
        }

        let determinationRatio: number = 0;

        if (this.attributes && this.attributes.has(Attribute.Determination)) {
            determinationRatio = Math.floor(130 * ((this.attributes.get(Attribute.Determination) as number) - 292) / 2170) / 1000 + 1;
        }

        let tenacityRatio: number = 1;

        if (([Job.Paladin, Job.Warrior, Job.DarkKnight] as Job[]).indexOf(this.job) !== -1) {
            if (this.attributes && this.attributes.has(Attribute.Tenacity)) {
                tenacityRatio = Math.floor(100 * ((this.attributes.get(Attribute.Tenacity) as number) - 364) / 2170) / 1000 + 1;
            }
        }

        this.damageEstimate = Math.floor(Math.floor(weaponRatio) * mainAttributeRatio) * directHitRatio * criticalHitRatio * determinationRatio * tenacityRatio;
        this.damageReduction = tenacityRatio - 1;

        if (this.attributes && this.attributes.has(this.speedAttribute)) {
            this.globalCooldown = Math.floor(250 * (1 - Math.floor(130 * ((this.attributes.get(this.speedAttribute) as number) - 364) / 2170) / 1000)) / 100;
        }
        
        this.attributeValues = [];
        
        this.attributeDisplays.forEach(function (): void {
            _this.attributeValues.push(0);
        });

        this.attributeDisplays.forEach(function (value: AttributeDisplay, index: number): void {
            _this.attributeValues[index] = _this.attributes.get(value.attribute) as number;
        });

        this.requiredMaterias = [];
        let materiaMap: Map<string, number> = new Map();

        this.equipments.forEach(function (equipment: Equipment): void {
            equipment.materias.forEach(function (materia: Materia): void {
                if (materia.materiaName && materia.materiaName.length > 0) {
                    let materiaCount: number = 1;

                    if (materiaMap.has(materia.materiaName)) {
                        materiaCount += materiaMap.get(materia.materiaName) as number;
                    }

                    materiaMap.set(materia.materiaName, materiaCount);
                }
            });
        });

        materiaMap.forEach(function (value: number, key: string): void {
            _this.requiredMaterias.push(key + ': ' + value + ' 개');
        });
    }
}

export class Action {
    originalObject: any;

    key: number;

    isFixed: boolean;

    rate: number;
    limit: number;
    fixedAmount: number;

    constructor() {
        this.key = -1;

        this.isFixed = false;

        this.rate = -1;
        this.limit = -1;
        this.fixedAmount = -1;
    }

    fromObject(o: any): void {
        this.originalObject = o;

        this.key = o['key'];

        this.rate = o['rate'];
        this.limit = o['limit'];
        this.fixedAmount = o['fixedAmount'];

        this.isFixed = this.fixedAmount !== -1;
    }

    toString(): string {
        return JSON.stringify(this.originalObject);
    }

    fromString(s: string): void {
        this.fromObject(JSON.parse(s));
    }
}

export class Meal {
    originalObject: any;

    key: number;

    nameKo: string;
    nameEn: string;
    iconPath: string;

    jobLevel: number;
    numStarsArray: number[];

    itemLevel: number;

    attributeActions: Map<Attribute, Action>;

    visible: boolean;

    constructor() {
        this.key = -1;

        this.nameKo = '';
        this.nameEn = '';
        this.iconPath = '';

        this.jobLevel = -1;
        this.numStarsArray = [];

        this.itemLevel = -1;

        this.attributeActions = new Map();

        this.visible = false;
    }

    fromObject(o: any): void {
        this.originalObject = o;

        this.key = o['key'];

        this.nameKo = o['nameKo'];
        this.nameEn = o['nameEn'];
        this.iconPath = o['iconPath'];

        this.jobLevel = o['jobLevel'];
        this.numStarsArray = o['numStarsArray'];

        this.itemLevel = o['itemLevel'];

        this.attributeActions = new Map();
        const _this: Meal = this;
        Object.keys(o['attributeActions']).forEach(function (value: string): void {
            let action: Action = new Action();
            action.fromObject(o['attributeActions'][value]);
            _this.attributeActions.set(parseInt(value) as Attribute, action);
        });

        this.visible = true;
    }

    toString(): string {
        return JSON.stringify(this.originalObject);
    }

    fromString(s: string): void {
        this.fromObject(JSON.parse(s));
    }
}

const defaultBaseAttributesByRace: Map<Race, Map<Attribute, number>> = new Map([
    [Race.Hyur1, new Map([
        [Attribute.Strength, 22],
        [Attribute.Dexterity, 19],
        [Attribute.Intelligence, 23],
        [Attribute.Mind, 19],
        [Attribute.Vitality, 20]
    ])],
    [Race.Hyur2, new Map([
        [Attribute.Strength, 23],
        [Attribute.Dexterity, 20],
        [Attribute.Intelligence, 18],
        [Attribute.Mind, 20],
        [Attribute.Vitality, 22]
    ])],

    [Race.Miqote1, new Map([
        [Attribute.Strength, 22],
        [Attribute.Dexterity, 23],
        [Attribute.Intelligence, 19],
        [Attribute.Mind, 19],
        [Attribute.Vitality, 20]])],
    [Race.Miqote2, new Map([
        [Attribute.Strength, 19],
        [Attribute.Dexterity, 22],
        [Attribute.Intelligence, 21],
        [Attribute.Mind, 23],
        [Attribute.Vitality, 18]
    ])],

    [Race.Elezen1, new Map([
        [Attribute.Strength, 20],
        [Attribute.Dexterity, 23],
        [Attribute.Intelligence, 22],
        [Attribute.Mind, 19],
        [Attribute.Vitality, 19]
    ])],
    [Race.Elezen2, new Map([
        [Attribute.Strength, 20],
        [Attribute.Dexterity, 20],
        [Attribute.Intelligence, 23],
        [Attribute.Mind, 21],
        [Attribute.Vitality, 19]
    ])],

    [Race.Roegadyn1, new Map([
        [Attribute.Strength, 22],
        [Attribute.Dexterity, 19],
        [Attribute.Intelligence, 18],
        [Attribute.Mind, 21],
        [Attribute.Vitality, 23]
    ])],
    [Race.Roegadyn2, new Map([
        [Attribute.Strength, 20],
        [Attribute.Dexterity, 18],
        [Attribute.Intelligence, 20],
        [Attribute.Mind, 22],
        [Attribute.Vitality, 23]
    ])],

    [Race.Lalafell1, new Map([
        [Attribute.Strength, 19],
        [Attribute.Dexterity, 23],
        [Attribute.Intelligence, 22],
        [Attribute.Mind, 20],
        [Attribute.Vitality, 19]
    ])],
    [Race.Lalafell2, new Map([
        [Attribute.Strength, 19],
        [Attribute.Dexterity, 21],
        [Attribute.Intelligence, 22],
        [Attribute.Mind, 23],
        [Attribute.Vitality, 18]
    ])],

    [Race.Aura1, new Map([
        [Attribute.Strength, 19],
        [Attribute.Dexterity, 22],
        [Attribute.Intelligence, 20],
        [Attribute.Mind, 23],
        [Attribute.Vitality, 19]
    ])],
    [Race.Aura2, new Map([
        [Attribute.Strength, 23],
        [Attribute.Dexterity, 20],
        [Attribute.Intelligence, 20],
        [Attribute.Mind, 18],
        [Attribute.Vitality, 22]
    ])]
]);

const defaultBaseAttributesByJob: Map<Job, Map<Attribute, number>> = new Map([
    [Job.Paladin, new Map([
        [Attribute.Strength, 272],
        [Attribute.Dexterity, 257],
        [Attribute.Intelligence, 155],
        [Attribute.Mind, 272],
        [Attribute.Vitality, 349]
    ])],
    [Job.Warrior, new Map([
        [Attribute.Strength, 286],
        [Attribute.Dexterity, 257],
        [Attribute.Intelligence, 96],
        [Attribute.Mind, 140],
        [Attribute.Vitality, 349]
    ])],
    [Job.DarkKnight, new Map([
        [Attribute.Strength, 286],
        [Attribute.Dexterity, 257],
        [Attribute.Intelligence, 155],
        [Attribute.Mind, 96],
        [Attribute.Vitality, 349]
    ])],

    [Job.WhiteMage, new Map([
        [Attribute.Strength, 140],
        [Attribute.Dexterity, 286],
        [Attribute.Intelligence, 286],
        [Attribute.Mind, 363],
        [Attribute.Vitality, 272]
    ])],
    [Job.Scholar, new Map([
        [Attribute.Strength, 242],
        [Attribute.Dexterity, 272],
        [Attribute.Intelligence, 334],
        [Attribute.Mind, 363],
        [Attribute.Vitality, 272]
    ])],
    [Job.Astrologian, new Map([
        [Attribute.Strength, 126],
        [Attribute.Dexterity, 272],
        [Attribute.Intelligence, 286],
        [Attribute.Mind, 363],
        [Attribute.Vitality, 272]
    ])],

    [Job.Monk, new Map([
        [Attribute.Strength, 349],
        [Attribute.Dexterity, 286],
        [Attribute.Intelligence, 126],
        [Attribute.Mind, 242],
        [Attribute.Vitality, 272]
    ])],
    [Job.Dragoon, new Map([
        [Attribute.Strength, 363],
        [Attribute.Dexterity, 272],
        [Attribute.Intelligence, 111],
        [Attribute.Mind, 169],
        [Attribute.Vitality, 286]
    ])],
    [Job.Ninja, new Map([
        [Attribute.Strength, 228],
        [Attribute.Dexterity, 349],
        [Attribute.Intelligence, 169],
        [Attribute.Mind, 199],
        [Attribute.Vitality, 272]
    ])],
    [Job.Samurai, new Map([
        [Attribute.Strength, 349],
        [Attribute.Dexterity, 286],
        [Attribute.Intelligence, 126],
        [Attribute.Mind, 242],
        [Attribute.Vitality, 272]
    ])],

    [Job.Bard, new Map([
        [Attribute.Strength, 242],
        [Attribute.Dexterity, 363],
        [Attribute.Intelligence, 228],
        [Attribute.Mind, 213],
        [Attribute.Vitality, 272]
    ])],
    [Job.Machinist, new Map([
        [Attribute.Strength, 228],
        [Attribute.Dexterity, 363],
        [Attribute.Intelligence, 213],
        [Attribute.Mind, 228],
        [Attribute.Vitality, 272]
    ])],

    [Job.BlackMage, new Map([
        [Attribute.Strength, 111],
        [Attribute.Dexterity, 272],
        [Attribute.Intelligence, 363],
        [Attribute.Mind, 199],
        [Attribute.Vitality, 272]
    ])],
    [Job.Summoner, new Map([
        [Attribute.Strength, 242],
        [Attribute.Dexterity, 272],
        [Attribute.Intelligence, 363],
        [Attribute.Mind, 213],
        [Attribute.Vitality, 272]
    ])],
    [Job.RedMage, new Map([
        [Attribute.Strength, 111],
        [Attribute.Dexterity, 272],
        [Attribute.Intelligence, 363],
        [Attribute.Mind, 199],
        [Attribute.Vitality, 272]
    ])],
]);

const defaultSubAttributes: Map<Attribute, number> = new Map([
    [Attribute.SkillSpeed, 364],
    [Attribute.SpellSpeed, 364],

    [Attribute.Tenacity, 364],
    [Attribute.Piety, 292],

    [Attribute.DirectHit, 364],
    [Attribute.CriticalHit, 364],
    [Attribute.Determination, 292]
]);

const wpMod: Map<Job, number> = new Map([
    [Job.Paladin, 0.1],
    [Job.Warrior, 0.105],
    [Job.DarkKnight, 0.105],

    [Job.WhiteMage, 0.115],
    [Job.Scholar, 0.115],
    [Job.Astrologian, 0.115],

    [Job.Monk, 0.11],
    [Job.Dragoon, 0.115],
    [Job.Ninja, 0.11],
    [Job.Samurai, 0.112],

    [Job.Bard, 0.115],
    [Job.Machinist, 0.115],

    [Job.BlackMage, 0.115],
    [Job.Summoner, 0.115],
    [Job.RedMage, 0.115]
]);

const vitMod: Map<Job, number> = new Map([
    [Job.Paladin, 1.2],
    [Job.Warrior, 1.25],
    [Job.DarkKnight, 1.2],

    [Job.WhiteMage, 1.05],
    [Job.Scholar, 1.05],
    [Job.Astrologian, 1.05],

    [Job.Monk, 1.1],
    [Job.Dragoon, 1.15],
    [Job.Ninja, 1.08],
    [Job.Samurai, 1.09],

    [Job.Bard, 1.05],
    [Job.Machinist, 1.05],

    [Job.BlackMage, 1.05],
    [Job.Summoner, 1.05],
    [Job.RedMage, 1.05]
]);

export class Equipment {
    originalObject: any;

    key: number;

    nameKo: string;
    nameEn: string;
    iconPath: string;
    source: string;

    itemLevel: number;
    equipLevel: number;
    itemCategory: ItemCategory;
    materiaSockets: number;
    materias: Materia[];

    damage: number;
    damageName: string;

    attackInterval: number;
    attackIntervalName: string;

    autoAttack: number;
    autoAttackName: string;

    shieldRate: number;
    shieldRateName: string;

    shieldBlockRate: number;
    shieldBlockRateName: string;

    defense: number;
    defenseName: string;

    magicDefense: number;
    magicDefenseName: string;

    attributes: Map<Attribute, number>;
    availableAttributes: Map<Attribute, number>;
    materiaAttributes: Map<Attribute, number>;

    materiaNames: string;

    constructor() {
        this.key = -1;

        this.nameKo = '';
        this.nameEn = '';
        this.iconPath = '';
        this.source = '';

        this.itemLevel = -1;
        this.equipLevel = -1;
        this.itemCategory = ItemCategory.OneHandedArm;
        this.materiaSockets = -1;
        this.materias = [];

        this.damage = -1;
        this.damageName = '';

        this.attackInterval = -1;
        this.attackIntervalName = '';

        this.autoAttack = -1;
        this.autoAttackName = '';

        this.shieldRate = -1;
        this.shieldRateName = '';

        this.shieldBlockRate = -1;
        this.shieldBlockRateName = '';

        this.defense = -1;
        this.defenseName = '';

        this.magicDefense = -1;
        this.magicDefenseName = '';

        this.attributes = new Map();
        this.availableAttributes = new Map();
        this.materiaAttributes = new Map();

        this.materiaNames = '';
    }
    
    fromObject(o: any): void {
        this.originalObject = o;

        this.key = o['key'];

        this.nameKo = o['nameKo'];
        this.nameEn = o['nameEn'];
        this.iconPath = o['iconPath'];
        this.source = o['source'];

        this.itemLevel = o['itemLevel'];
        this.equipLevel = o['equipLevel'];
        this.itemCategory = o['itemCategory'];
        this.materiaSockets = o['materiaSockets'];
        this.materias = [];

        for (let i = 0; i < this.materiaSockets; i++) {
            this.materias.push(new Materia());
        }

        if (this.source === '제작' && this.itemCategory !== ItemCategory.Shield) {
            while (this.materias.length < 5) {
                let materia: Materia = new Materia();
                materia.isForbidden = true;
                this.materias.push(materia);
            }
        }

        this.damage = o['damage'];
        this.damageName = o['damageName'];

        this.attackInterval = o['attackInterval'];
        this.attackIntervalName = o['attackIntervalName'];

        this.autoAttack = o['autoAttack'];
        this.autoAttackName = o['autoAttackName'];

        this.shieldRate = o['shieldRate'];
        this.shieldRateName = o['shieldRateName'];

        this.shieldBlockRate = o['shieldBlockRate'];
        this.shieldBlockRateName = o['shieldBlockRateName'];

        this.defense = o['defense'];
        this.defenseName = o['defenseName'];

        this.magicDefense = o['magicDefense'];
        this.magicDefenseName = o['magicDefenseName'];

        this.attributes = new Map();
        const _this: Equipment = this;
        Object.keys(o['attributes']).forEach(function (value: string): void {
            _this.attributes.set(parseInt(value) as Attribute, parseInt(o['attributes'][value]));
        });

        this.availableAttributes = new Map();
        this.materiaAttributes = new Map();
    }

    toString(): string {
        let o: any = {
            originalObject: this.originalObject,
            materias: []
        };

        this.materias.forEach(function (): void {
            o.materias.push('');
        });

        this.materias.forEach(function (materia: Materia, index: number): void {
            o.materias[index] = materia.toString();
        });

        return JSON.stringify(o);
    }

    fromString(s: string): void {
        let o: any = JSON.parse(s);

        this.fromObject(o['originalObject']);

        const _this: Equipment = this;

        o['materias'].forEach(function (value: string, index: number): void {
            _this.materias[index] = new Materia();
            _this.materias[index].fromString(value);
        });
    }

    populateAttributes(): void {
        this.availableAttributes = new Map();
        this.materiaAttributes = new Map();

        for (let i = 1; i <= maxAttributeIndex; i++) {
            if (typeof this.attributes.get(i as Attribute) === 'undefined') {
                this.attributes.set(i as Attribute, 0);
            }

            this.materiaAttributes.set(i as Attribute, 0);
        }

        let isAccessory: boolean = [
            ItemCategory.Earrings,
            ItemCategory.Necklace,
            ItemCategory.Bracelets,
            ItemCategory.Ring
        ].indexOf(this.itemCategory) !== -1;

        let attributeValues: number[] = [];

        this.attributes.forEach(function (value: number, key: Attribute): void {
            if (isAccessory || key !== Attribute.Vitality) {
                attributeValues.push(value);
            }
        });

        attributeValues.sort(function (a: number, b: number): number {
            return b - a;
        });

        let maxAmount: number = attributeValues[1];
        
        const _this: Equipment = this;

        this.attributes.forEach(function (value: number, key: Attribute): void {
            _this.availableAttributes.set(
                key,
                Math.max(0, maxAmount - value)
            );
        });

        this.materias.forEach(function (value: Materia, index: number): void {
            if (value.isForbidden) {
                if (index - 1 >= 0 && index - 1 < _this.materias.length) {
                    if (!_this.materias[index - 1].isForbidden) {
                        value.highestType = MateriaType.VI;
                    } else {
                        value.highestType = MateriaType.V;
                    }
                }
            } else {
                value.highestType = MateriaType.VI;
            }

            value.populateMateriaData();

            let currentAmount: number = _this.availableAttributes.get(value.attribute) as number;
            _this.availableAttributes.set(value.attribute, currentAmount - value.materiaStrength);

            let currentTotal: number =
                (_this.attributes.get(value.attribute) as number) +
                (_this.materiaAttributes.get(value.attribute) as number);
            if (maxAmount > currentTotal) {
                let newTotal: number = Math.min(maxAmount, currentTotal + value.materiaStrength);
                let newMateriaAttribute: number = newTotal - (_this.attributes.get(value.attribute) as number);
                _this.materiaAttributes.set(value.attribute, newMateriaAttribute);
            }
        });

        this.materiaNames = '';
        
        this.materias.forEach(function (value: Materia): void {
            if (value.materiaName && value.materiaName.length > 0) {
                _this.materiaNames += value.materiaName + ', ';
            }
        });

        if (this.materiaNames && this.materiaNames.length > 2) {
            this.materiaNames = this.materiaNames.substring(0, this.materiaNames.length - 2);
        }
    }
}

export class Materia {
    attribute: Attribute;
    materiaType: MateriaType;
    isForbidden: boolean;
    highestType: MateriaType;

    materiaName: string;
    materiaStrength: number;
    iconPath: string;
    
    constructor() {
        this.attribute = Attribute.Strength;
        this.materiaName = '';
        this.materiaType = MateriaType.None;
        this.highestType = MateriaType.None;

        this.materiaStrength = 0;
        this.isForbidden = false;
        this.iconPath = '';
    }

    toString(): string {
        return JSON.stringify({
            attribute: this.attribute,
            materiaType: this.materiaType,
            isForbidden: this.isForbidden
        });
    }

    fromString(s: string): void {
        let o: any = JSON.parse(s);
        this.attribute = o['attribute'];
        this.materiaType = o['materiaType'];
        this.isForbidden = o['isForbidden'];
    }

    populateMateriaData(): void {
        if (this.materiaType === MateriaType.None) {
            this.iconPath = this.isForbidden ? 'empty-forbidden-socket.png' : 'empty-socket.png';
            this.materiaName = '';
            this.materiaStrength = 0;
            return;
        }

        this.materiaName =
            attributeToMateriaNamePrepend.get(this.attribute) +
            ' ' +
            materiaTypeToMateriaNameAppend.get(this.materiaType);
        
        switch (this.attribute) {
            case Attribute.Strength:
            case Attribute.Dexterity:
            case Attribute.Intelligence:
            case Attribute.Mind:
                switch (this.materiaType) {
                    case MateriaType.I:
                        this.materiaStrength = 1;
                        break;
                    case MateriaType.II:
                        this.materiaStrength = 2;
                        break;
                    case MateriaType.III:
                        this.materiaStrength = 4;
                        break;
                    case MateriaType.IV:
                        this.materiaStrength = 7;
                        break;
                    case MateriaType.V:
                        this.materiaStrength = 15;
                        break;
                    case MateriaType.VI:
                        this.materiaStrength = 25;
                        break;
                }
                break;
            case Attribute.Vitality:
                switch (this.materiaType) {
                    case MateriaType.I:
                        this.materiaStrength = 1;
                        break;
                    case MateriaType.II:
                        this.materiaStrength = 2;
                        break;
                    case MateriaType.III:
                        this.materiaStrength = 4;
                        break;
                    case MateriaType.IV:
                        this.materiaStrength = 8;
                        break;
                    case MateriaType.V:
                        this.materiaStrength = 15;
                        break;
                    case MateriaType.VI:
                        this.materiaStrength = 25;
                        break;
                }
                break;
            case Attribute.SkillSpeed:
            case Attribute.SpellSpeed:
            case Attribute.Tenacity:
            case Attribute.DirectHit:
            case Attribute.CriticalHit:
                switch (this.materiaType) {
                    case MateriaType.I:
                        this.materiaStrength = 2;
                        break;
                    case MateriaType.II:
                        this.materiaStrength = 4;
                        break;
                    case MateriaType.III:
                        this.materiaStrength = 6;
                        break;
                    case MateriaType.IV:
                        this.materiaStrength = 9;
                        break;
                    case MateriaType.V:
                        this.materiaStrength = 12;
                        break;
                    case MateriaType.VI:
                        this.materiaStrength = 40;
                        break;
                }
                break;
            case Attribute.Piety:
                switch (this.materiaType) {
                    case MateriaType.I:
                        this.materiaStrength = 1;
                        break;
                    case MateriaType.II:
                        this.materiaStrength = 2;
                        break;
                    case MateriaType.III:
                        this.materiaStrength = 3;
                        break;
                    case MateriaType.IV:
                        this.materiaStrength = 6;
                        break;
                    case MateriaType.V:
                        this.materiaStrength = 11;
                        break;
                    case MateriaType.VI:
                        this.materiaStrength = 40;
                        break;
                }
                break;
            case Attribute.Determination:
                switch (this.materiaType) {
                    case MateriaType.I:
                        this.materiaStrength = 1;
                        break;
                    case MateriaType.II:
                        this.materiaStrength = 3;
                        break;
                    case MateriaType.III:
                        this.materiaStrength = 4;
                        break;
                    case MateriaType.IV:
                        this.materiaStrength = 6;
                        break;
                    case MateriaType.V:
                        this.materiaStrength = 12;
                        break;
                    case MateriaType.VI:
                        this.materiaStrength = 40;
                        break;
                }
                break;
        }

        switch (this.attribute) {
            case Attribute.Strength:
            case Attribute.Dexterity:
            case Attribute.Intelligence:
            case Attribute.Mind:
            case Attribute.Vitality:
                switch (this.materiaType) {
                    case MateriaType.I:
                        this.iconPath = 'ui/icon/020000/020205.tex.png';
                        break;
                    case MateriaType.II:
                        this.iconPath = 'ui/icon/020000/020206.tex.png';
                        break;
                    case MateriaType.III:
                        this.iconPath = 'ui/icon/020000/020207.tex.png';
                        break;
                    case MateriaType.IV:
                        this.iconPath = 'ui/icon/020000/020208.tex.png';
                        break;
                    case MateriaType.V:
                        this.iconPath = 'ui/icon/020000/020254.tex.png';
                        break;
                    case MateriaType.VI:
                        this.iconPath = 'ui/icon/020000/020263.tex.png';
                        break;
                }
                break;
            case Attribute.SkillSpeed:
            case Attribute.SpellSpeed:
                switch (this.materiaType) {
                    case MateriaType.I:
                        this.iconPath = 'ui/icon/020000/020225.tex.png';
                        break;
                    case MateriaType.II:
                        this.iconPath = 'ui/icon/020000/020226.tex.png';
                        break;
                    case MateriaType.III:
                        this.iconPath = 'ui/icon/020000/020227.tex.png';
                        break;
                    case MateriaType.IV:
                        this.iconPath = 'ui/icon/020000/020228.tex.png';
                        break;
                    case MateriaType.V:
                        this.iconPath = 'ui/icon/020000/020259.tex.png';
                        break;
                    case MateriaType.VI:
                        this.iconPath = 'ui/icon/020000/020265.tex.png';
                        break;
                }
                break;
            case Attribute.Tenacity:
            case Attribute.Piety:
                switch (this.materiaType) {
                    case MateriaType.I:
                        this.iconPath = 'ui/icon/020000/020213.tex.png';
                        break;
                    case MateriaType.II:
                        this.iconPath = 'ui/icon/020000/020214.tex.png';
                        break;
                    case MateriaType.III:
                        this.iconPath = 'ui/icon/020000/020215.tex.png';
                        break;
                    case MateriaType.IV:
                        this.iconPath = 'ui/icon/020000/020216.tex.png';
                        break;
                    case MateriaType.V:
                        this.iconPath = 'ui/icon/020000/020256.tex.png';
                        break;
                    case MateriaType.VI:
                        this.iconPath = 'ui/icon/020000/020262.tex.png';
                        break;
                }
                break;
            case Attribute.DirectHit:
            case Attribute.CriticalHit:
            case Attribute.Determination:
                switch (this.materiaType) {
                    case MateriaType.I:
                        this.iconPath = 'ui/icon/020000/020221.tex.png';
                        break;
                    case MateriaType.II:
                        this.iconPath = 'ui/icon/020000/020222.tex.png';
                        break;
                    case MateriaType.III:
                        this.iconPath = 'ui/icon/020000/020223.tex.png';
                        break;
                    case MateriaType.IV:
                        this.iconPath = 'ui/icon/020000/020224.tex.png';
                        break;
                    case MateriaType.V:
                        this.iconPath = 'ui/icon/020000/020258.tex.png';
                        break;
                    case MateriaType.VI:
                        this.iconPath = 'ui/icon/020000/020267.tex.png';
                        break;
                }
                break;
        }
    }
}

export class EquipmentInfo {
    key: number;

    nameKo: string;
    nameEn: string;
    iconPath: string;
    source: string;

    constructor() {
        this.key = -1;

        this.nameKo = '';
        this.nameEn = '';
        this.iconPath = '';
        this.source = '';
    }
}

export class Table {
    equipCategory: EquipCategory;
    equipCategoryName: string;
    rows: Row[];

    constructor() {
        this.equipCategory = EquipCategory.Weapon;
        this.equipCategoryName = '';
        this.rows = [];
    }
}

export class Row {
    equipment: Equipment;
    selected: boolean;
    attributeValues: number[];
    materiaAttributeValues: number[];
    materiaNames: string;

    constructor() {
        this.equipment = new Equipment();
        this.selected = false;
        this.attributeValues = [];
        this.materiaAttributeValues = [];
        this.materiaNames = '';
    }
}

export class AttributeDisplay {
    attribute: Attribute;
    displayName: string;

    constructor() {
        this.attribute = Attribute.Strength;
        this.displayName = '';
    }
}

export class MealRow {
    meal: Meal;
    selected: boolean;
    attributeValues: string[];

    constructor() {
        this.meal = new Meal();
        this.selected = false;
        this.attributeValues = [];
    }
}

export const attributeToMateriaNamePrepend: Map<Attribute, string> = new Map([
    [Attribute.Strength, '힘의'],
    [Attribute.Dexterity, '민첩성의'],
    [Attribute.Intelligence, '지능의'],
    [Attribute.Mind, '정신력의'],

    [Attribute.SkillSpeed, '신속의'],
    [Attribute.SpellSpeed, '시전의'],

    [Attribute.Vitality, '활력의'],

    [Attribute.Tenacity, '강유의'],
    [Attribute.Piety, '신앙의'],

    [Attribute.DirectHit, '심안의'],
    [Attribute.CriticalHit, '무략의'],
    [Attribute.Determination, '야망의']
]);

export const materiaTypeToMateriaNameAppend: Map<MateriaType, string> = new Map([
    [MateriaType.I, '마테리아'],
    [MateriaType.II, '마테리라'],
    [MateriaType.III, '마테리다'],
    [MateriaType.IV, '마테리가'],
    [MateriaType.V, '마테리쟈'],
    [MateriaType.VI, '하이마테리쟈']
]);

const jobToDamageAttribute: Map<Job, Attribute> = new Map([
    [Job.Paladin, Attribute.Strength],
    [Job.Warrior, Attribute.Strength],
    [Job.DarkKnight, Attribute.Strength],

    [Job.WhiteMage, Attribute.Mind],
    [Job.Scholar, Attribute.Mind],
    [Job.Astrologian, Attribute.Mind],

    [Job.Monk, Attribute.Strength],
    [Job.Dragoon, Attribute.Strength],
    [Job.Ninja, Attribute.Dexterity],
    [Job.Samurai, Attribute.Strength],

    [Job.Bard, Attribute.Dexterity],
    [Job.Machinist, Attribute.Dexterity],

    [Job.BlackMage, Attribute.Intelligence],
    [Job.Summoner, Attribute.Intelligence],
    [Job.RedMage, Attribute.Intelligence]
]);

const jobToSpeedAttribute: Map<Job, Attribute> = new Map([
    [Job.Paladin, Attribute.SkillSpeed],
    [Job.Warrior, Attribute.SkillSpeed],
    [Job.DarkKnight, Attribute.SkillSpeed],

    [Job.WhiteMage, Attribute.SpellSpeed],
    [Job.Scholar, Attribute.SpellSpeed],
    [Job.Astrologian, Attribute.SpellSpeed],

    [Job.Monk, Attribute.SkillSpeed],
    [Job.Dragoon, Attribute.SkillSpeed],
    [Job.Ninja, Attribute.SkillSpeed],
    [Job.Samurai, Attribute.SkillSpeed],

    [Job.Bard, Attribute.SkillSpeed],
    [Job.Machinist, Attribute.SkillSpeed],

    [Job.BlackMage, Attribute.SpellSpeed],
    [Job.Summoner, Attribute.SpellSpeed],
    [Job.RedMage, Attribute.SpellSpeed]
]);

export const jobToDisplayName: Map<Job, string> = new Map([
    [Job.Paladin, '나이트'],
    [Job.Warrior, '전사'],
    [Job.DarkKnight, '암흑기사'],

    [Job.WhiteMage, '백마도사'],
    [Job.Scholar, '학자'],
    [Job.Astrologian, '점성술사'],

    [Job.Monk, '몽크'],
    [Job.Dragoon, '용기사'],
    [Job.Ninja, '닌자'],
    [Job.Samurai, '사무라이'],

    [Job.Bard, '음유시인'],
    [Job.Machinist, '기공사'],

    [Job.BlackMage, '흑마도사'],
    [Job.Summoner, '소환사'],
    [Job.RedMage, '적마도사']
]);

export const jobToIconPath: Map<Job, string> = new Map([
    [Job.Paladin, 'ui/icon/062000/062119.tex.png'],
    [Job.Warrior, 'ui/icon/062000/062121.tex.png'],
    [Job.DarkKnight, 'ui/icon/062000/062132.tex.png'],

    [Job.WhiteMage, 'ui/icon/062000/062124.tex.png'],
    [Job.Scholar, 'ui/icon/062000/062128.tex.png'],
    [Job.Astrologian, 'ui/icon/062000/062133.tex.png'],

    [Job.Monk, 'ui/icon/062000/062120.tex.png'],
    [Job.Dragoon, 'ui/icon/062000/062122.tex.png'],
    [Job.Ninja, 'ui/icon/062000/062130.tex.png'],
    [Job.Samurai, 'ui/icon/062000/062134.tex.png'],

    [Job.Bard, 'ui/icon/062000/062123.tex.png'],
    [Job.Machinist, 'ui/icon/062000/062131.tex.png'],

    [Job.BlackMage, 'ui/icon/062000/062125.tex.png'],
    [Job.Summoner, 'ui/icon/062000/062127.tex.png'],
    [Job.RedMage, 'ui/icon/062000/062135.tex.png']
]);

export const attributeToDisplayName: Map<Attribute, string> = new Map([
    [Attribute.Strength, '힘'],
    [Attribute.Dexterity, '민첩성'],
    [Attribute.Intelligence, '지능'],
    [Attribute.Mind, '정신력'],

    [Attribute.SkillSpeed, '기시'],
    [Attribute.SpellSpeed, '마시'],

    [Attribute.Vitality, '활력'],

    [Attribute.Tenacity, '불굴'],
    [Attribute.Piety, '신앙'],

    [Attribute.DirectHit, '직격'],
    [Attribute.CriticalHit, '극대'],
    [Attribute.Determination, '의지']
]);

export const equipCategoryToDisplayName: Map<EquipCategory, string> = new Map([
    [EquipCategory.Weapon, '무기'],
    [EquipCategory.Shield, '방패'],

    [EquipCategory.Head, '머리'],
    [EquipCategory.Body, '몸통'],
    [EquipCategory.Hands, '손'],
    [EquipCategory.Waist, '허리'],
    [EquipCategory.Legs, '다리'],
    [EquipCategory.Feet, '발'],

    [EquipCategory.Earrings, '귀걸이'],
    [EquipCategory.Necklace, '목걸이'],
    [EquipCategory.Bracelets, '팔찌'],
    [EquipCategory.LeftRing, '왼쪽 반지'],
    [EquipCategory.RightRing, '오른쪽 반지']
]);

export const raceToDisplayName: Map<Race, string> = new Map([
    [Race.Hyur1, '휴런 - 중원 부족'],
    [Race.Hyur2, '휴런 - 고원 부족'],
    [Race.Miqote1, '미코테 - 태양의 추종자'],
    [Race.Miqote2, '미코테 - 달의 수호자'],
    [Race.Elezen1, '엘레젠 - 숲 부족'],
    [Race.Elezen2, '엘레젠 - 황혼 부족'],
    [Race.Roegadyn1, '루가딘 - 바다늑대'],
    [Race.Roegadyn2, '루가딘 - 불꽃지킴이'],
    [Race.Lalafell1, '라라펠 - 평원 부족'],
    [Race.Lalafell2, '라라펠 - 사막 부족'],
    [Race.Aura1, '아우라 - 아우라 렌'],
    [Race.Aura2, '아우라 - 아우라 젤라']
]);