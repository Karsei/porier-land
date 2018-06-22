using FFXIVTranslator.PorierFFXIV;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PorierLand.Controllers
{
    [Route("api/bis_calculator")]
    public class BisCalculatorController : Controller
    {
        private readonly PorierFFXIVDbContext _context;

        public BisCalculatorController(PorierFFXIVDbContext context)
        {
            _context = context;
        }

        [HttpGet("equipment-info")]
        public object GetEquipmentInfo([FromQuery(Name = "key")] long key)
        {
            try
            {
                Item item = _context.Items.FirstOrDefault(_item => _item.Key == key);

                if (item == null) return null;

                return new EquipmentInfo
                {
                    key = item.Key,
                    nameKo = item.UINameKo,
                    nameEn = item.UINameEn,
                    iconPath = item.IconPath,
                    source = item.Level != null
                        ? getSource((long)item.Level, item.UINameEn)
                        : null
                };
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("equipments")]
        public object GetEquipments([FromQuery(Name = "job")] Job job, [FromQuery(Name = "minLevel")] int minLevel, [FromQuery(Name = "maxLevel")] int maxLevel)
        {
            try
            {
                if (minLevel > maxLevel)
                {
                    minLevel = maxLevel;
                }

                if (maxLevel - minLevel > 50)
                {
                    minLevel = maxLevel - 50;
                }

                if (!jobToString.ContainsKey(job)) return new Equipment[0];

                string jobString = jobToString[job];

                Equipment[] equipments = _context.Items
                    .Where(item => item.Level >= minLevel && item.Level <= maxLevel && item.ItemClassJobs.FirstOrDefault(itemClassJob => itemClassJob.ClassJob.AbbreviationEn == jobString) != null)
                    .Select(item => new Equipment
                    {
                        key = item.Key,
                        nameKo = item.UINameKo,
                        nameEn = item.UINameEn,
                        iconPath = item.IconPath,
                        source = string.Empty,

                        itemLevel = item.Level == null ? 0 : (long)item.Level,
                        equipLevel = item.EquipLevel == null ? 0 : (long)item.EquipLevel,
                        itemCategory = ItemCategory.OneHandedArm,
                        materiaSockets = item.MateriaSocket,

                        damage = 0,
                        damageName = new Job[]
                        {
                        Job.WhiteMage,
                        Job.Scholar,
                        Job.Astrologian,

                        Job.BlackMage,
                        Job.Summoner
                        }.Contains(job) ? "마법 기본 성능" : "물리 기본 성능",

                        attackInterval = 0,
                        attackIntervalName = "공격 주기",

                        autoAttack = 0,
                        autoAttackName = "물리 자동 공격",

                        shieldRate = 0,
                        shieldRateName = "방패 막기 성능",
                        shieldBlockRate = 0,
                        shieldBlockRateName = "방패 막기 발동력",

                        defense = 0,
                        defenseName = "물리 방어력",

                        magicDefense = 0,
                        magicDefenseName = "마법 방어력",

                        attributes = null
                    })
                    .ToArray();

                for (int i = 0; i < equipments.Count(); i++)
                {
                    if (equipments[i].key == null) continue;

                    Item item = _context.Items
                        .Include(_item => _item.ItemBonuses)
                        .Include(_item => _item.UICategory)
                        .FirstOrDefault(_item => _item.Key == equipments[i].key);

                    if (item == null) continue;

                    if (item.Damage != null)
                    {
                        equipments[i].damage = Math.Max(equipments[i].damage, (long)item.Damage);
                    }

                    if (item.DamageHQ != null)
                    {
                        equipments[i].damage = Math.Max(equipments[i].damage, (long)item.DamageHQ);
                    }

                    if (item.MagicDamage != null)
                    {
                        equipments[i].damage = Math.Max(equipments[i].damage, (long)item.MagicDamage);
                    }

                    if (item.MagicDamageHQ != null)
                    {
                        equipments[i].damage = Math.Max(equipments[i].damage, (long)item.MagicDamageHQ);
                    }

                    if (item.AttackInterval != null)
                    {
                        equipments[i].attackInterval = Math.Max(equipments[i].attackInterval, (double)item.AttackInterval);
                    }

                    if (item.AttackIntervalHQ != null)
                    {
                        equipments[i].attackInterval = Math.Max(equipments[i].attackInterval, (double)item.AttackIntervalHQ);
                    }

                    if (item.AutoAttack != null)
                    {
                        equipments[i].autoAttack = Math.Max(equipments[i].autoAttack, (double)item.AutoAttack);
                    }

                    if (item.AutoAttackHQ != null)
                    {
                        equipments[i].autoAttack = Math.Max(equipments[i].autoAttack, (double)item.AutoAttackHQ);
                    }

                    if (item.ShieldRate != null)
                    {
                        equipments[i].shieldRate = Math.Max(equipments[i].shieldRate, (long)item.ShieldRate);
                    }

                    if (item.ShieldRateHQ != null)
                    {
                        equipments[i].shieldRate = Math.Max(equipments[i].shieldRate, (long)item.ShieldRateHQ);
                    }

                    if (item.ShieldBlockRate != null)
                    {
                        equipments[i].shieldBlockRate = Math.Max(equipments[i].shieldBlockRate, (long)item.ShieldBlockRate);
                    }

                    if (item.ShieldBlockRateHQ != null)
                    {
                        equipments[i].shieldBlockRate = Math.Max(equipments[i].shieldBlockRate, (long)item.ShieldBlockRateHQ);
                    }

                    if (item.Defense != null)
                    {
                        equipments[i].defense = Math.Max(equipments[i].defense, (long)item.Defense);
                    }

                    if (item.DefenseHQ != null)
                    {
                        equipments[i].defense = Math.Max(equipments[i].defense, (long)item.DefenseHQ);
                    }

                    if (item.MagicDefense != null)
                    {
                        equipments[i].magicDefense = Math.Max(equipments[i].magicDefense, (long)item.MagicDefense);
                    }

                    if (item.MagicDefenseHQ != null)
                    {
                        equipments[i].magicDefense = Math.Max(equipments[i].magicDefense, (long)item.MagicDefenseHQ);
                    }

                    equipments[i].attributes = new Dictionary<int, long>();

                    foreach (ItemBonus itemBonus in item.ItemBonuses)
                    {
                        ItemBonus _itemBonus = _context.ItemBonuses
                            .Include(__itemBonus => __itemBonus.BaseParam)
                            .FirstOrDefault(__itemBonus => __itemBonus.Key == itemBonus.Key);

                        if (_itemBonus == null) continue;
                        if (_itemBonus.BaseParam == null) continue;

                        if (stringToAttribute.ContainsKey(_itemBonus.BaseParam.NameEn))
                        {
                            Attribute attribute = stringToAttribute[_itemBonus.BaseParam.NameEn];

                            if (equipments[i].attributes.ContainsKey((int)attribute))
                            {
                                equipments[i].attributes[(int)attribute] = Math.Max(equipments[i].attributes[(int)attribute], _itemBonus.Amount);
                            }
                            else
                            {
                                equipments[i].attributes.Add((int)attribute, _itemBonus.Amount);
                            }
                        }
                    }

                    if (stringToItemCategory.ContainsKey(item.UICategory.NameEn))
                    {
                        equipments[i].itemCategory = stringToItemCategory[item.UICategory.NameEn];
                    }

                    equipments[i].source = getSource(equipments[i].itemLevel, equipments[i].nameEn);
                }

                return equipments;
            }
            catch
            {
                return new Equipment[0];
            }
        }

        private string getSource(long itemLevel, string nameEn)
        {
            if (itemLevel >= 310 && itemLevel <= 345)
            {
                for (int i = 0; i < nameToSourceKeywords310.Count(); i++)
                {
                    if (nameEn.Contains(nameToSourceKeywords310[i][0]))
                    {
                        return nameToSourceKeywords310[i][1];
                    }
                }
            }
            else if (itemLevel >= 350 && itemLevel <= 375)
            {
                for (int i = 0; i < nameToSourceKeywords350.Count(); i++)
                {
                    if (nameEn.Contains(nameToSourceKeywords350[i][0]))
                    {
                        return nameToSourceKeywords350[i][1];
                    }
                }

                return "Mendacity 석판";
            }

            return null;
        }

        [HttpPost("bis-sets")]
        public object PostBisSets(BisSetPayload bisSetPayload)
        {
            try
            {
                BisSet bisSet = _context.BisSets.FirstOrDefault(_bisSet => _bisSet.Sets == bisSetPayload.payload);

                if (bisSet != null)
                {
                    return bisSet.Identifier;
                }

                string id = string.Empty;

                do
                {
                    id = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    bisSet = _context.BisSets.FirstOrDefault(_bisSet => _bisSet.Identifier == id);
                }
                while (bisSet != null);

                bisSet = new BisSet
                {
                    Identifier = id,
                    Sets = bisSetPayload.payload
                };

                _context.BisSets.Add(bisSet);
                _context.SaveChanges(true);

                return id;
            }
            catch
            {
                return "";
            }
        }

        [HttpGet("bis-sets")]
        public object GetBisSets([FromQuery(Name = "id")] string id)
        {
            try
            {
                BisSet bisSet = _context.BisSets.FirstOrDefault(_bisSet => _bisSet.Identifier == id);

                if (bisSet == null)
                {
                    return "[]";
                }
                else
                {
                    return bisSet.Sets;
                }
            }
            catch
            {
                return "[]";
            }
        }

        [HttpGet("meals")]
        public object GetMeals([FromQuery(Name = "minLevel")] int minLevel, [FromQuery(Name = "maxLevel")] int maxLevel)
        {
            try
            {
                if (minLevel > maxLevel)
                {
                    minLevel = maxLevel;
                }

                if (maxLevel - minLevel > 100)
                {
                    minLevel = maxLevel - 100;
                }

                Meal[] meals = _context.Items
                    .Include(item => item.ItemActions)
                    .Include(item => item.CraftRecipes)
                    .Where(item => item.UICategory != null
                        && item.UICategory.Key == 46
                        && item.ItemActions != null
                        && item.ItemActions.Count > 0
                        && item.ItemActions.FirstOrDefault(itemAction => itemAction.BaseParam != null && stringToAttribute.Keys.Contains(itemAction.BaseParam.NameEn)) != null
                        && item.CraftRecipes.FirstOrDefault(recipe => recipe.CraftJob.NameEn == "Culinarian") != null
                        && item.Level >= minLevel && item.Level <= maxLevel)
                    .Select(item => new Meal
                    {
                        key = item.Key,

                        nameKo = item.UINameKo,
                        nameEn = item.UINameEn,
                        iconPath = item.IconPath,

                        jobLevel = item.CraftRecipes.FirstOrDefault(recipe => recipe.CraftJob.NameEn == "Culinarian") != null
                            ? item.CraftRecipes.FirstOrDefault(recipe => recipe.CraftJob.NameEn == "Culinarian").LevelView != null
                                ? (long)item.CraftRecipes.FirstOrDefault(recipe => recipe.CraftJob.NameEn == "Culinarian").LevelView
                                : -1
                            : -1,
                        numStarsArray = item.CraftRecipes.FirstOrDefault(recipe => recipe.CraftJob.NameEn == "Culinarian") != null
                            ? item.CraftRecipes.FirstOrDefault(recipe => recipe.CraftJob.NameEn == "Culinarian").LevelDiff != null
                                ? new int[(long)item.CraftRecipes.FirstOrDefault(recipe => recipe.CraftJob.NameEn == "Culinarian").LevelDiff]
                                : new int[0]
                            : new int[0],

                        itemLevel = item.Level != null ? (long)item.Level : -1,

                        attributeActions = null
                    }).ToArray();
                
                for (int i = 0; i < meals.Length; i++)
                {
                    Item item = _context.Items
                        .Include(_item => _item.ItemActions)
                        .Include(_item => _item.CraftRecipes)
                        .FirstOrDefault(_item => _item.Key == meals[i].key);
                    if (item == null) continue;
                    if (item.ItemActions == null || item.ItemActions.Count == 0) continue;

                    meals[i].attributeActions = new Dictionary<int, Action>();

                    foreach (ItemAction itemAction in item.ItemActions)
                    {
                        ItemAction _itemAction = _context.ItemActions
                            .Include(__itemAction => __itemAction.BaseParam)
                            .FirstOrDefault(__itemAction => __itemAction.Key == itemAction.Key);
                        
                        if (_itemAction == null) continue;
                        if (_itemAction.QualityType != QualityType.HQ) continue;
                        if (!stringToAttribute.Keys.Contains(_itemAction.BaseParam.NameEn)) continue;
                        
                        meals[i].attributeActions.Add((int)stringToAttribute[_itemAction.BaseParam.NameEn], new Action
                        {
                            key = _itemAction.Key,

                            rate = _itemAction.Rate,
                            limit = _itemAction.Limit,
                            fixedAmount = _itemAction.FixedAmount,

                            qualityType = _itemAction.QualityType,
                            mealName = _itemAction.Item.UINameEn
                        });
                    }
                }
                
                return meals;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return "[]";
            }
        }

        public class BisSetPayload
        {
            public string payload { get; set; }
        }
        

        private Dictionary<Job, string> jobToString = new Dictionary<Job, string>
        {
            { Job.Paladin, "PLD" },
            { Job.Warrior, "WAR" },
            { Job.DarkKnight, "DRK" },
            { Job.WhiteMage, "WHM" },
            { Job.Scholar, "SCH" },
            { Job.Astrologian, "AST" },
            { Job.Monk, "MNK" },
            { Job.Dragoon, "DRG" },
            { Job.Ninja, "NIN" },
            { Job.Samurai, "SAM" },
            { Job.Bard, "BRD" },
            { Job.Machinist, "MCH" },
            { Job.BlackMage, "BLM" },
            { Job.Summoner, "SMN" },
            { Job.RedMage, "RDM" }
        };
        
        private Dictionary<string, Attribute> stringToAttribute = new Dictionary<string, Attribute>
        {
            { "Strength", Attribute.Strength },
            { "Dexterity", Attribute.Dexterity },
            { "Intelligence", Attribute.Intelligence },
            { "Mind", Attribute.Mind },

            { "Skill Speed", Attribute.SkillSpeed },
            { "Spell Speed", Attribute.SpellSpeed },

            { "Vitality", Attribute.Vitality },

            { "Tenacity", Attribute.Tenacity },
            { "Piety", Attribute.Piety },

            { "Direct Hit Rate", Attribute.DirectHit },
            { "Critical Hit", Attribute.CriticalHit },
            { "Determination", Attribute.Determination }
        };
        
        private string[][] nameToSourceKeywords350 = new string[][]
        {
            new string[] { "Diamond", "시그마 영식" },

            new string[] { "Kai", "Mendacity 석판 보강" },
            new string[] { "Dai-ryumyaku", "Mendacity 석판 보강" },
            
            new string[] { "Byakko's", "극 백호 토벌전" },

            new string[] { "Carborundum", "시그마 일반" },

            new string[] { "Nightsteel", "제작" },
            new string[] { "Black Willow", "제작" },
            new string[] { "Silvergrace", "제작" },
            new string[] { "True Linen", "제작" },
            new string[] { "Slothskin", "제작" }
        };

        private string[][] nameToSourceKeywords310 = new string[][]
        {
            new string[] { "Ultimate Dreadwyrm", "절 바하무트" },

            new string[] { "Genji", "델타 영식" },

            new string[] { "Augmented Lost Allagan", "만물 석판 보강" },

            new string[] { "Shinryu's", "극 신룡 토벌전" },

            new string[] { "Lost Allagan", "만물 석판" },

            new string[] { "Ivalician", "라바나스타 (24인 레이드)" },

            new string[] { "Torreya", "제작" },
            new string[] { "True Griffin", "제작" },
            new string[] { "Indigo Ramie", "제작" },
            new string[] { "Palladium", "제작" },
            new string[] { "Chromite", "제작" },

            new string[] { "Lakshmi's", "극 락슈미 토벌전" },

            new string[] { "Genta", "델타 일반" },

            new string[] { "Susano's", "극 스사노오 토벌전" },

            new string[] { "Skallic", "스칼라 (315 던전)" },

            new string[] { "Ala Mhigan", "진리 석판" }
        };

        private Dictionary<string, ItemCategory> stringToItemCategory = new Dictionary<string, ItemCategory>
        {
            { "Gladiator's Arm", ItemCategory.OneHandedArm },
            { "Marauder's Arm", ItemCategory.TwoHandedArm },
            { "Dark Knight's Arm", ItemCategory.TwoHandedArm },
            { "One-handed Conjurer's Arm", ItemCategory.OneHandedArm },
            { "Two-handed Conjurer's Arm", ItemCategory.TwoHandedArm },
            { "Scholar's Arm", ItemCategory.TwoHandedArm },
            { "Astrologian's Arm", ItemCategory.TwoHandedArm },
            { "Pugilist's Arm", ItemCategory.TwoHandedArm },
            { "Lancer's Arm", ItemCategory.TwoHandedArm },
            { "Rogue's Arm", ItemCategory.TwoHandedArm },
            { "Samurai's Arm", ItemCategory.TwoHandedArm },
            { "Archer's Arm", ItemCategory.TwoHandedArm },
            { "Machinist's Arm", ItemCategory.TwoHandedArm },
            { "One-handed Thaumaturge's Arm", ItemCategory.OneHandedArm },
            { "Two-handed Thaumaturge's Arm", ItemCategory.TwoHandedArm },
            { "Arcanist's Grimoire", ItemCategory.TwoHandedArm },
            { "Red Mage's Arm", ItemCategory.TwoHandedArm },

            { "Shield", ItemCategory.Shield },

            { "Head", ItemCategory.Head },
            { "Body", ItemCategory.Body },
            { "Hands", ItemCategory.Hands },
            { "Waist", ItemCategory.Waist },
            { "Legs", ItemCategory.Legs },
            { "Feet", ItemCategory.Feet },

            { "Earrings", ItemCategory.Earrings },
            { "Necklace", ItemCategory.Necklace },
            { "Bracelets", ItemCategory.Bracelets },
            { "Ring", ItemCategory.Ring }
        };

        public enum Attribute
        {
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

        public enum Job
        {
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

        public enum ItemCategory
        {
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
        
        public class Equipment
        {
            public long? key { get; set; }

            public string nameKo { get; set; }
            public string nameEn { get; set; }
            public string iconPath { get; set; }
            public string source { get; set; }

            public long itemLevel { get; set; }
            public long equipLevel { get; set; }
            public ItemCategory itemCategory { get; set; }
            public int materiaSockets { get; set; }

            public long damage { get; set; }
            public string damageName { get; set; }

            public double attackInterval { get; set; }
            public string attackIntervalName { get; set; }

            public double autoAttack { get; set; }
            public string autoAttackName { get; set; }

            public long shieldRate { get; set; }
            public string shieldRateName { get; set; }

            public long shieldBlockRate { get; set; }
            public string shieldBlockRateName { get; set; }

            public long defense { get; set; }
            public string defenseName { get; set; }

            public long magicDefense { get; set; }
            public string magicDefenseName { get; set; }

            public Dictionary<int, long> attributes { get; set; }
        }

        public class EquipmentInfo
        {
            public long? key { get; set; }

            public string nameKo { get; set; }
            public string nameEn { get; set; }
            public string iconPath { get; set; }
            public string source { get; set; }
        }

        public class Meal
        {
            public long key { get; set; }

            public string nameKo { get; set; }
            public string nameEn { get; set; }
            public string iconPath { get; set; }

            public long jobLevel { get; set; }
            public int[] numStarsArray { get; set; }

            public long itemLevel { get; set; }

            public Dictionary<int, Action> attributeActions { get; set; }
        }

        public class Action
        {
            public long key { get; set; }

            public int rate { get; set; }
            public int limit { get; set; }
            public int fixedAmount { get; set; }

            public QualityType qualityType { get; set; }
            public string mealName { get; set; }
        }
    }
}