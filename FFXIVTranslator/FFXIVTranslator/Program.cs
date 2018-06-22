using FFXIVTranslator.CSV;
using FFXIVTranslator.LibraEorzea;
using FFXIVTranslator.PorierFFXIV;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFXIVTranslator
{
    class Program
    {
        static LibraEorzeaDbContext libraEorzeaDbContext;
        static PorierFFXIVDbContext porierFFXIVDbContext;
        static Dictionary<long, string> ItemUiKindDictionary;
        static Dictionary<string, string> GatheringJobDictionary;
        
        static void WriteLog(string title, ref int count, int total)
        {
            Console.Write("\r{0}: {1}% ({2} / {3})", title, (int)Math.Ceiling(((float)(count + 1) / total) * 100), count + 1, total);
            count++;
        }

        static void PopulateBaseParam()
        {
            Console.WriteLine("Populating BaseParam...");
            IEnumerable<PorierFFXIV.BaseParam> baseParams =
                from baseParam in libraEorzeaDbContext.BaseParam
                join baseParamRow in BaseParamRow.GetRows()
                on baseParam.Key equals baseParamRow.Key
                select new PorierFFXIV.BaseParam
                {
                    Key = baseParam.Key,
                    NameKo = baseParamRow.Name,
                    NameJa = baseParam.NameJa,
                    NameEn = baseParam.NameEn,
                    NameFr = baseParam.NameFr,
                    NameDe = baseParam.NameDe
                };
            int count = 0;
            int total = baseParams.Count();
            foreach (PorierFFXIV.BaseParam baseParam in baseParams)
            {
                WriteLog("BaseParam", ref count, total);
                porierFFXIVDbContext.BaseParams.Add(baseParam);
            }
            Console.WriteLine("\nSaving BaseParam...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateBeastTribe()
        {
            Console.WriteLine("Populating BeastTribe...");
            IEnumerable<PorierFFXIV.BeastTribe> beastTribes =
                from beastTribe in libraEorzeaDbContext.BeastTribe
                join beastTribeRow in BeastTribeRow.GetRows()
                on beastTribe.Key equals beastTribeRow.Key
                select new PorierFFXIV.BeastTribe
                {
                    Key = beastTribe.Key,
                    SGLKo = beastTribeRow.Name,
                    SGLJa = beastTribe.SglJa,
                    SGLEn = beastTribe.SglEn,
                    SGLFr = beastTribe.SglFr,
                    SGLDe = beastTribe.SglDe
                };
            int count = 0;
            int total = beastTribes.Count();
            foreach (PorierFFXIV.BeastTribe beastTribe in beastTribes)
            {
                WriteLog("BeastTribe", ref count, total);
                porierFFXIVDbContext.BeastTribes.Add(beastTribe);
            }
            Console.WriteLine("\nSaving BeastTribe...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateClassJob()
        {
            Console.WriteLine("Populating ClassJob...");
            IEnumerable<PorierFFXIV.ClassJob> classJobs =
                from classJob in libraEorzeaDbContext.ClassJob
                join classJobRow in ClassJobRow.GetRows()
                on classJob.Key equals classJobRow.Key
                select new PorierFFXIV.ClassJob
                {
                    Key = classJob.Key,
                    Label = classJob.Label,
                    NameKo = classJobRow.Name,
                    NameJa = classJob.NameJa,
                    NameEn = classJob.NameEn,
                    NameFr = classJob.NameFr,
                    NameDe = classJob.NameDe,
                    AbbreviationKo = classJobRow.Abbreviation,
                    AbbreviationJa = classJob.AbbreviationJa,
                    AbbreviationEn = classJob.AbbreviationEn,
                    AbbreviationFr = classJob.AbbreviationFr,
                    AbbreviationDe = classJob.AbbreviationDe,
                    IsJob = classJob.IsJob.Equals(1L)
                };
            int count = 0;
            int total = classJobs.Count();
            foreach (PorierFFXIV.ClassJob classJob in classJobs)
            {
                WriteLog("ClassJob", ref count, total);
                porierFFXIVDbContext.ClassJobs.Add(classJob);
            }
            Console.WriteLine("\nSaving ClassJob...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateClassJobCategory()
        {
            Console.WriteLine("Populating ClassJobCategory...");
            IEnumerable<PorierFFXIV.ClassJobCategory> classJobCategories =
                from classJobCategory in libraEorzeaDbContext.ClassJobCategory
                join classJobCategoryRow in ClassJobCategoryRow.GetRows()
                on classJobCategory.Key equals classJobCategoryRow.Key
                select new PorierFFXIV.ClassJobCategory
                {
                    Key = classJobCategory.Key,
                    NameKo = classJobCategoryRow.Name,
                    NameJa = classJobCategory.NameJa,
                    NameEn = classJobCategory.NameEn,
                    NameFr = classJobCategory.NameFr,
                    NameDe = classJobCategory.NameDe
                };
            int count = 0;
            int total = classJobCategories.Count();
            foreach (PorierFFXIV.ClassJobCategory classJobCategory in classJobCategories)
            {
                WriteLog("ClassJobCategory", ref count, total);
                porierFFXIVDbContext.ClassJobCategories.Add(classJobCategory);
            }
            Console.WriteLine("\nSaving ClassJobCategory...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateClassJobClassJobCategory()
        {
            Console.WriteLine("Populating ClassJobClassJobCategory...");
            int count = 0;
            int total = libraEorzeaDbContext.ClassJobClassJobCategory.Count();
            foreach (LibraEorzea.ClassJobClassJobCategory classJobClassJobCategory in libraEorzeaDbContext.ClassJobClassJobCategory)
            {
                WriteLog("ClassJobClassJobCategory", ref count, total);

                PorierFFXIV.ClassJob classJob = porierFFXIVDbContext.ClassJobs.Find(classJobClassJobCategory.ClassJobKey);
                PorierFFXIV.ClassJobCategory classJobCategory = porierFFXIVDbContext.ClassJobCategories.Find(classJobClassJobCategory.ClassJobCategoryKey);
                
                if (classJob != null && classJobCategory != null)
                {
                    porierFFXIVDbContext.ClassJobClassJobCategories.Add(new PorierFFXIV.ClassJobClassJobCategory
                    {
                        ClassJob = classJob,
                        ClassJobCategory = classJobCategory
                    });
                }
            }
            Console.WriteLine("\nSaving ClassJobClassJobCategory...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateENpc()
        {
            Console.WriteLine("Populating ENpc...");
            IEnumerable<ENpc> eNpcs =
                from eNpc in libraEorzeaDbContext.EnpcResident
                join eNpcRow in ENpcRow.GetRows()
                on eNpc.Key equals eNpcRow.Key
                select new ENpc
                {
                    Key = eNpc.Key,
                    SGLKo = eNpcRow.Name,
                    SGLJa = eNpc.SglJa,
                    SGLEn = eNpc.SglEn,
                    SGLFr = eNpc.SglFr,
                    SGLDe = eNpc.SglDe,
                    HasShop = eNpc.HasShop.Equals("1"),
                    HasConditionShop = eNpc.HasConditionShop.Equals("1"),
                    Data = eNpc.Data,
                    Path = eNpc.Path
                };
            int count = 0;
            int total = eNpcs.Count();
            foreach (ENpc eNpc in eNpcs)
            {
                WriteLog("ENpc", ref count, total);
                porierFFXIVDbContext.ENpcs.Add(eNpc);
            }
            Console.WriteLine("\nSaving ENpc...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateGatheringType()
        {
            Console.WriteLine("Populating GatheringType...");
            IEnumerable<PorierFFXIV.GatheringType> gatheringTypes =
                from gatheringType in libraEorzeaDbContext.GatheringType
                join gatheringTypeRow in GatheringTypeRow.GetRows()
                on gatheringType.Key equals gatheringTypeRow.Key
                select new PorierFFXIV.GatheringType
                {
                    Key = gatheringType.Key,
                    TextKo = gatheringTypeRow.Name,
                    TextJa = gatheringType.TextJa,
                    TextEn = gatheringType.TextEn,
                    TextFr = gatheringType.TextFr,
                    TextDe = gatheringType.TextDe
                };
            int count = 0;
            int total = gatheringTypes.Count();
            foreach (PorierFFXIV.GatheringType gatheringType in gatheringTypes)
            {
                WriteLog("GatheringType", ref count, total);
                porierFFXIVDbContext.GatheringTypes.Add(gatheringType);
            }
            Console.WriteLine("\nSaving GatheringType...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateGatheringSubCategory()
        {
            Console.WriteLine("Populating GatheringSubCategory...");
            var joinedGatheringSubCategories =
                from gatheringSubCategory in libraEorzeaDbContext.GatheringSubCategory
                join gatheringSubCategoryRow in GatheringSubCategoryRow.GetRows()
                on gatheringSubCategory.Key equals gatheringSubCategoryRow.Key
                select new
                {
                    GatheringSubCategory = gatheringSubCategory,
                    GatheringSubCategoryRow = gatheringSubCategoryRow
                };
            int count = 0;
            int total = joinedGatheringSubCategories.Count();
            foreach (var joinedGatheringSubCategory in joinedGatheringSubCategories)
            {
                WriteLog("GatheringSubCategory", ref count, total);

                PorierFFXIV.GatheringSubCategory gatheringSubCategory = new PorierFFXIV.GatheringSubCategory
                {
                    Key = joinedGatheringSubCategory.GatheringSubCategory.Key,
                    NameKo = joinedGatheringSubCategory.GatheringSubCategoryRow.Name,
                    NameJa = joinedGatheringSubCategory.GatheringSubCategory.NameJa,
                    NameEn = joinedGatheringSubCategory.GatheringSubCategory.NameEn,
                    NameFr = joinedGatheringSubCategory.GatheringSubCategory.NameFr,
                    NameDe = joinedGatheringSubCategory.GatheringSubCategory.NameDe
                };

                if (joinedGatheringSubCategory.GatheringSubCategory.GatheringType != null)
                {
                    gatheringSubCategory.GatheringType = porierFFXIVDbContext.GatheringTypes.Find(joinedGatheringSubCategory.GatheringSubCategory.GatheringType);
                }

                porierFFXIVDbContext.GatheringSubCategories.Add(gatheringSubCategory);
            }
            Console.WriteLine("\nSaving GatheringSubCategory...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateItemUIKind()
        {
            Console.WriteLine("Populating ItemUIKind...");
            int count = 0;
            int total = libraEorzeaDbContext.ItemUikind.Count();
            foreach (ItemUikind itemUiKind in libraEorzeaDbContext.ItemUikind)
            {
                WriteLog("ItemUIKind", ref count, total);

                ItemUIKind _itemUiKind = new ItemUIKind
                {
                    Key = itemUiKind.Key,
                    NameJa = itemUiKind.NameJa,
                    NameEn = itemUiKind.NameEn,
                    NameFr = itemUiKind.NameFr,
                    NameDe = itemUiKind.NameDe
                };
                
                if (ItemUiKindDictionary.TryGetValue(itemUiKind.Key, out string NameKo))
                {
                    _itemUiKind.NameKo = NameKo;
                }

                porierFFXIVDbContext.ItemUIKinds.Add(_itemUiKind);
            }
            Console.WriteLine("\nSaving ItemUIKind...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateItemUICategory()
        {
            Console.WriteLine("Populating ItemUICategory...");
            var joinedItemUiCategories =
                from itemUiCategory in libraEorzeaDbContext.ItemUicategory
                join itemUiCategoryRow in ItemUICategoryRow.GetRows()
                on itemUiCategory.Key equals itemUiCategoryRow.Key
                select new
                {
                    ItemUICategory = itemUiCategory,
                    ItemUICategoryRow = itemUiCategoryRow
                };
            int count = 0;
            int total = joinedItemUiCategories.Count();
            foreach (var joinedItemUiCategory in joinedItemUiCategories)
            {
                WriteLog("ItemUICategory", ref count, total);

                ItemUICategory itemUiCategory = new ItemUICategory
                {
                    Key = joinedItemUiCategory.ItemUICategory.Key,
                    NameKo = joinedItemUiCategory.ItemUICategoryRow.Name,
                    NameJa = joinedItemUiCategory.ItemUICategory.NameJa,
                    NameEn = joinedItemUiCategory.ItemUICategory.NameEn,
                    NameFr = joinedItemUiCategory.ItemUICategory.NameFr,
                    NameDe = joinedItemUiCategory.ItemUICategory.NameDe
                };

                if (joinedItemUiCategory.ItemUICategory.Kind != null)
                {
                    itemUiCategory.Kind = porierFFXIVDbContext.ItemUIKinds.Find(joinedItemUiCategory.ItemUICategory.Kind);
                }

                porierFFXIVDbContext.ItemUICategories.Add(itemUiCategory);
            }
            Console.WriteLine("\nSaving ItemUICategory...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateItem()
        {
            Console.WriteLine("Populating Item...");

            var joinedItems =
                from item in libraEorzeaDbContext.Item
                join itemRow in ItemRow.GetRows()
                on item.Key equals itemRow.Key into itemRows
                from itemRow in itemRows.DefaultIfEmpty()
                join globalRow in ItemRow.GetGlobalRows()
                on item.Key equals globalRow.Key into globalRows
                from globalRow in globalRows.DefaultIfEmpty()
                select new
                {
                    Item = item,
                    ItemRow = itemRow,
                    GlobalRow = globalRow
                };
            int count = 0;
            int total = joinedItems.Count();
            foreach (var joinedItem in joinedItems)
            {
                WriteLog("Item", ref count, total);

                PorierFFXIV.Item item = new PorierFFXIV.Item
                {
                    Key = joinedItem.Item.Key,
                    UINameKo = joinedItem.ItemRow == null ? "" : joinedItem.ItemRow.Name,
                    UINameJa = joinedItem.Item.UinameJa,
                    UINameEn = joinedItem.Item.UinameEn,
                    UINameFr = joinedItem.Item.UinameFr,
                    UINameDe = joinedItem.Item.UinameDe,
                    HelpKo = joinedItem.ItemRow == null ? "" : joinedItem.ItemRow.Description,
                    HelpJa = joinedItem.Item.HelpJa,
                    HelpEn = joinedItem.Item.HelpEn,
                    HelpFr = joinedItem.Item.HelpFr,
                    HelpDe = joinedItem.Item.HelpDe,
                    Level = joinedItem.Item.Level,
                    EquipLevel = joinedItem.Item.EquipLevel,
                    Rarity = joinedItem.Item.Rarity,
                    HQ = joinedItem.Item.Hq.Equals("1"),
                    Damage = joinedItem.Item.Damage,
                    DamageHQ = joinedItem.Item.DamageHq,
                    MagicDamage = joinedItem.Item.MagicDamage,
                    MagicDamageHQ = joinedItem.Item.MagicDamageHq,
                    Defense = joinedItem.Item.Defense,
                    DefenseHQ = joinedItem.Item.DefenseHq,
                    MagicDefense = joinedItem.Item.MagicDefense,
                    MagicDefenseHQ = joinedItem.Item.MagicDefenseHq,
                    ShieldRate = joinedItem.Item.ShieldRate,
                    ShieldRateHQ = joinedItem.Item.ShieldRateHq,
                    ShieldBlockRate = joinedItem.Item.ShieldBlockRate,
                    ShieldBlockRateHQ = joinedItem.Item.ShieldBlockRateHq,
                    AttackInterval = joinedItem.Item.AttackInterval,
                    AttackIntervalHQ = joinedItem.Item.AttackIntervalHq,
                    AutoAttack = joinedItem.Item.AutoAttack,
                    AutoAttackHQ = joinedItem.Item.AutoAttackHq,
                    Price = joinedItem.Item.Price,
                    Data = joinedItem.Item.Data,
                    Path = joinedItem.Item.Path
                };

                /*
                if (joinedItem.ItemRow != null && joinedItem.ItemRow.ItemNumber != null &&
                    Int64.TryParse(joinedItem.ItemRow.ItemNumber, out long itemNumber))
                {
                    if (itemNumber == 0)
                    {
                        item.IconPath = "ui/icon/000000/000009.tex.png";
                    }
                    else
                    {
                        string baseNumber = (itemNumber - (itemNumber % 1000L)).ToString("D6");
                        item.IconPath = "ui/icon/" + baseNumber + "/" + itemNumber.ToString("D6") + ".tex.png";
                    }
                }
                */

                if (joinedItem.GlobalRow != null && joinedItem.GlobalRow.ItemNumber != null &&
                    Int64.TryParse(joinedItem.GlobalRow.ItemNumber, out long itemNumber))
                {
                    if (itemNumber == 0)
                    {
                        item.IconPath = "ui/icon/000000/000009.tex.png";
                    }
                    else
                    {
                        string baseNumber = (itemNumber - (itemNumber % 1000L)).ToString("D6");
                        item.IconPath = "ui/icon/" + baseNumber + "/" + itemNumber.ToString("D6") + ".tex.png";
                    }
                }

                if (joinedItem.Item.Uicategory != null)
                {
                    item.UICategory = porierFFXIVDbContext.ItemUICategories.Find(joinedItem.Item.Uicategory);
                }

                JObject data = JObject.Parse(Encoding.ASCII.GetString(item.Data));

                if (data["CondClassJob"] != null)
                {
                    if (Int64.TryParse((string)data["CondClassJob"], out long classJobCategoryKey))
                    {
                        item.ClassJobCategory = porierFFXIVDbContext.ClassJobCategories.Find(classJobCategoryKey);
                    }
                }

                item.MateriaSocket = 0;

                if (data["MateriaSocket"] != null)
                {
                    if (Int32.TryParse((string)data["MateriaSocket"], out int materiaSocket))
                    {
                        item.MateriaSocket = materiaSocket;
                    }
                }

                porierFFXIVDbContext.Items.Add(item);
            }
            Console.WriteLine("\nSaving Item...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateGathering()
        {
            Console.WriteLine("Populating Gathering...");
            int count = 0;
            int total = libraEorzeaDbContext.Gathering.Count();
            foreach (LibraEorzea.Gathering gathering in libraEorzeaDbContext.Gathering)
            {
                WriteLog("Gathering", ref count, total);

                PorierFFXIV.Gathering _gathering = new PorierFFXIV.Gathering
                {
                    Key = gathering.Key,
                    Level = gathering.Level,
                    LevelView = gathering.LevelView,
                    LevelDiff = gathering.LevelDiff,
                    IsHidden = gathering.IsHidden.Equals("1"),
                    Data = gathering.Data,
                    Path = gathering.Path
                };

                if (gathering.Item != null)
                {
                    _gathering.Item = porierFFXIVDbContext.Items.Find(gathering.Item);
                }

                if (gathering.GatheringType != null)
                {
                    _gathering.GatheringType = porierFFXIVDbContext.GatheringTypes.Find(gathering.GatheringType);
                    
                    if (GatheringJobDictionary.ContainsKey(_gathering.GatheringType.TextKo))
                    {
                        _gathering.GatheringJob = porierFFXIVDbContext.ClassJobs.FirstOrDefault(classJob => classJob.NameKo.Equals(GatheringJobDictionary[_gathering.GatheringType.TextKo]));
                    }
                }

                if (gathering.GatheringSubCategory != null)
                {
                    _gathering.GatheringSubCategory = porierFFXIVDbContext.GatheringSubCategories.Find(gathering.GatheringSubCategory);
                }

                porierFFXIVDbContext.Gatherings.Add(_gathering);
            }
            Console.WriteLine("\nSaving Gathering...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateItemBonus()
        {
            Console.WriteLine("Populating ItemBonus...");
            int count = 0;
            int total = porierFFXIVDbContext.Items.Count();
            foreach (PorierFFXIV.Item item in porierFFXIVDbContext.Items)
            {
                WriteLog("ItemBonus", ref count, total);

                JObject data = JObject.Parse(Encoding.ASCII.GetString(item.Data));

                List<Tuple<JArray, QualityType>> bonusTuples = new List<Tuple<JArray, QualityType>>();

                if (data["bonus_hq"] != null)
                {
                    bonusTuples.Add(new Tuple<JArray, QualityType>((JArray)data["bonus_hq"], QualityType.HQ));
                }

                if (data["bonus"] != null)
                {
                    bonusTuples.Add(new Tuple<JArray, QualityType>((JArray)data["bonus"], QualityType.NQ));
                }

                foreach (Tuple<JArray, QualityType> bonusTuple in bonusTuples)
                {
                    foreach (JObject bonusObject in bonusTuple.Item1)
                    {
                        foreach (JProperty bonusProperty in bonusObject.Children())
                        {
                            if (Int64.TryParse(bonusProperty.Name, out long baseParamKey) &&
                                Int64.TryParse(bonusProperty.Value.ToString(), out long amount))
                            {
                                PorierFFXIV.BaseParam baseParam = porierFFXIVDbContext.BaseParams.Find(baseParamKey);

                                if (baseParam != null)
                                {
                                    porierFFXIVDbContext.ItemBonuses.Add(new ItemBonus
                                    {
                                        Item = item,
                                        BaseParam = baseParam,
                                        QualityType = bonusTuple.Item2,
                                        Amount = amount
                                    });
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("\nSaving ItemBonus...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateItemAction()
        {
            Console.WriteLine("Populating ItemAction...");
            int count = 0;
            int total = porierFFXIVDbContext.Items.Count();
            foreach (PorierFFXIV.Item item in porierFFXIVDbContext.Items)
            {
                WriteLog("ItemAction", ref count, total);

                JObject data = JObject.Parse(Encoding.ASCII.GetString(item.Data));

                List<Tuple<JArray, QualityType>> actionTuples = new List<Tuple<JArray, QualityType>>();

                if (data["action_hq"] != null)
                {
                    actionTuples.Add(new Tuple<JArray, QualityType>((JArray)data["action_hq"], QualityType.HQ));
                }

                if (data["action"] != null)
                {
                    actionTuples.Add(new Tuple<JArray, QualityType>((JArray)data["action"], QualityType.NQ));
                }

                foreach (Tuple<JArray, QualityType> actionTuple in actionTuples)
                {
                    foreach (JObject actionObject in actionTuple.Item1)
                    {
                        foreach (JProperty actionProperty in actionObject.Children())
                        {
                            if (Int64.TryParse(actionProperty.Name, out long baseParamKey))
                            {
                                PorierFFXIV.BaseParam baseParam = porierFFXIVDbContext.BaseParams.Find(baseParamKey);

                                if (baseParam != null)
                                {
                                    if (actionProperty.Value.Type == JTokenType.Object)
                                    {
                                        JObject obj = (JObject)actionProperty.Value;

                                        if (obj["rate"] != null && obj["limit"] != null)
                                        {
                                            if (Int32.TryParse(obj["rate"].ToString(), out int rate) &&
                                                Int32.TryParse(obj["limit"].ToString(), out int limit))
                                            {
                                                porierFFXIVDbContext.ItemActions.Add(new ItemAction
                                                {
                                                    Item = item,
                                                    BaseParam = baseParam,
                                                    QualityType = actionTuple.Item2,
                                                    Rate = rate,
                                                    Limit = limit,
                                                    FixedAmount = -1
                                                });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (Int32.TryParse(actionProperty.Value.ToString(), out int fixedAmount))
                                        {
                                            porierFFXIVDbContext.ItemActions.Add(new ItemAction
                                            {
                                                Item = item,
                                                BaseParam = baseParam,
                                                QualityType = actionTuple.Item2,
                                                Rate = -1,
                                                Limit = -1,
                                                FixedAmount = fixedAmount
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("\nSaving ItemAction...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateItemClassJob()
        {
            Console.WriteLine("Populating ItemClassJob...");
            int count = 0;
            int total = libraEorzeaDbContext.ItemClassJob.Count();
            foreach (LibraEorzea.ItemClassJob itemClassJob in libraEorzeaDbContext.ItemClassJob)
            {
                WriteLog("ItemClassJob", ref count, total);

                PorierFFXIV.Item item = porierFFXIVDbContext.Items.Find(itemClassJob.ItemKey);
                PorierFFXIV.ClassJob classJob = porierFFXIVDbContext.ClassJobs.Find(itemClassJob.ClassJobKey);

                if (item != null && classJob != null)
                {
                    porierFFXIVDbContext.ItemClassJobs.Add(new PorierFFXIV.ItemClassJob
                    {
                        Item = item,
                        ClassJob = classJob
                    });
                }
            }
            Console.WriteLine("\nSaving ItemClassJob...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateItemENpc()
        {
            Console.WriteLine("Populating ItemENpc...");
            int count = 0;
            int total = porierFFXIVDbContext.Items.Count();
            foreach (PorierFFXIV.Item item in porierFFXIVDbContext.Items)
            {
                WriteLog("Item ENpc", ref count, total);

                JObject data = JObject.Parse(Encoding.ASCII.GetString(item.Data));

                if (data["shopnpc"] != null)
                {
                    foreach (string eNpcKeyString in data["shopnpc"])
                    {
                        if (Int64.TryParse(eNpcKeyString, out long eNpcKey))
                        {
                            ENpc eNpc = porierFFXIVDbContext.ENpcs.Find(eNpcKey);

                            if (eNpc != null)
                            {
                                porierFFXIVDbContext.ItemENpcs.Add(new ItemENpc
                                {
                                    Item = item,
                                    ENpc = eNpc
                                });
                            }
                        }
                    }
                }
            }
            Console.WriteLine("\nSaving ItemENpc...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulatePlaceName()
        {
            Console.WriteLine("Populating PlaceName...");
            var joinedPlaceNames =
                from placeName in libraEorzeaDbContext.PlaceName
                join placeNameRow in PlaceNameRow.GetRows()
                on placeName.Key equals placeNameRow.Key
                select new
                {
                    PlaceName = placeName,
                    PlaceNameRow = placeNameRow
                };
            int count = 0;
            int total = joinedPlaceNames.Count() * 2;
            foreach (var joinedPlaceName in joinedPlaceNames)
            {
                WriteLog("PlaceName", ref count, total);

                porierFFXIVDbContext.PlaceNames.Add(new PorierFFXIV.PlaceName
                {
                    Key = joinedPlaceName.PlaceName.Key,
                    SGLKo = joinedPlaceName.PlaceNameRow.Name,
                    SGLJa = joinedPlaceName.PlaceName.SglJa,
                    SGLEn = joinedPlaceName.PlaceName.SglEn,
                    SGLFr = joinedPlaceName.PlaceName.SglFr,
                    SGLDe = joinedPlaceName.PlaceName.SglDe
                });
            }

            foreach (var joinedPlaceName in joinedPlaceNames)
            {
                WriteLog("Place Name", ref count, total);

                if (joinedPlaceName.PlaceName.Region != null)
                {
                    PorierFFXIV.PlaceName parentPlaceName = porierFFXIVDbContext.PlaceNames.Find(joinedPlaceName.PlaceName.Key);
                    PorierFFXIV.PlaceName childPlaceName = porierFFXIVDbContext.PlaceNames.Find(joinedPlaceName.PlaceName.Region);

                    if (parentPlaceName != null && childPlaceName != null)
                    {
                        parentPlaceName.Region = childPlaceName;
                    }
                }
            }
            Console.WriteLine("\nSaving PlaceName...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateENpcPlaceName()
        {
            Console.WriteLine("Populating ENpcPlaceName...");
            int count = 0;
            int total = porierFFXIVDbContext.ENpcs.Count();
            foreach (ENpc eNpc in porierFFXIVDbContext.ENpcs)
            {
                WriteLog("ENpcPlaceName", ref count, total);

                JObject data = JObject.Parse(Encoding.ASCII.GetString(eNpc.Data));

                if (data["coordinate"] != null)
                {
                    foreach (JProperty coordinateProperty in data["coordinate"].Children())
                    {
                        if (Int64.TryParse(coordinateProperty.Name, out long placeNameKey))
                        {
                            PorierFFXIV.PlaceName placeName = porierFFXIVDbContext.PlaceNames.Find(placeNameKey);

                            if (placeName != null)
                            {
                                foreach (JArray coordinateArray in coordinateProperty.Value)
                                {
                                    if (coordinateArray.ToArray().Length == 2)
                                    {
                                        if (Double.TryParse(coordinateArray.ElementAt(0).ToString(), out double x) &&
                                            Double.TryParse(coordinateArray.ElementAt(1).ToString(), out double y))
                                        {
                                            porierFFXIVDbContext.ENpcPlaceNames.Add(new ENpcPlaceName
                                            {
                                                ENpc = eNpc,
                                                PlaceName = placeName,
                                                X = x,
                                                Y = y
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("\nSaving ENpcPlaceName...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateGatheringPoint()
        {
            Console.WriteLine("Populating GatheringPoint...");
            int count = 0;
            int total = porierFFXIVDbContext.Gatherings.Count();
            foreach (PorierFFXIV.Gathering gathering in porierFFXIVDbContext.Gatherings)
            {
                WriteLog("GatheringPoint", ref count, total);

                JObject data = JObject.Parse(Encoding.ASCII.GetString(gathering.Data));

                if (data["GatheringPoints"] != null)
                {
                    foreach (JObject jObject in data["GatheringPoints"])
                    {
                        ParseGatheringPoint(jObject, gathering, null);
                    }
                }
            }
            Console.WriteLine("\nSaving GatheringPoint...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void ParseGatheringPoint(JObject jObject, PorierFFXIV.Gathering gathering, GatheringPoint parent)
        {
            GatheringPoint gatheringPoint = new GatheringPoint
            {
                Gathering = gathering
            };

            if (jObject["key"] != null &&
                Int64.TryParse(jObject["key"].ToString(), out long placeNameKey))
            {
                PorierFFXIV.PlaceName placeName = porierFFXIVDbContext.PlaceNames.Find(placeNameKey);

                if (placeName != null)
                {
                    gatheringPoint.PlaceName = placeName;
                }
            }

            if (jObject["level"] != null &&
                Int64.TryParse(jObject["level"].ToString(), out long level))
            {
                gatheringPoint.Level = level;
            }

            if (parent != null)
            {
                gatheringPoint.Parent = parent;
            }

            porierFFXIVDbContext.GatheringPoints.Add(gatheringPoint);

            if (jObject["children"] != null)
            {
                foreach (JObject _jObject in jObject["children"])
                {
                    ParseGatheringPoint(_jObject, gathering, gatheringPoint);
                }
            }
        }

        static void PopulateQuest()
        {
            Console.WriteLine("Populating Quest...");
            IEnumerable<PorierFFXIV.Quest> quests =
                from quest in libraEorzeaDbContext.Quest
                join questRow in QuestRow.GetRows()
                on quest.Key equals questRow.Key
                select new PorierFFXIV.Quest
                {
                    Key = quest.Key,
                    NameKo = questRow.Name,
                    NameJa = quest.NameJa,
                    NameEn = quest.NameEn,
                    NameFr = quest.NameFr,
                    NameDe = quest.NameDe
                };
            int count = 0;
            int total = quests.Count();
            foreach (PorierFFXIV.Quest quest in quests)
            {
                WriteLog("Quest", ref count, total);
                porierFFXIVDbContext.Quests.Add(quest);
            }
            Console.WriteLine("\nSaving Quest...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateRecipeElement()
        {
            Console.WriteLine("Populating RecipeElement...");
            IEnumerable<PorierFFXIV.RecipeElement> recipeElements =
                from recipeElement in libraEorzeaDbContext.RecipeElement
                join recipeElementRow in RecipeElementRow.GetRows()
                on recipeElement.Key equals recipeElementRow.Key
                select new PorierFFXIV.RecipeElement
                {
                    Key = recipeElement.Key,
                    NameKo = recipeElementRow.Name,
                    NameJa = recipeElement.NameJa,
                    NameEn = recipeElement.NameEn,
                    NameFr = recipeElement.NameFr,
                    NameDe = recipeElement.NameDe
                };
            int count = 0;
            int total = recipeElements.Count();
            foreach (PorierFFXIV.RecipeElement recipeElement in recipeElements)
            {
                WriteLog("RecipeElement", ref count, total);
                porierFFXIVDbContext.RecipeElements.Add(recipeElement);
            }
            Console.WriteLine("\nSaving RecipeElement...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateSecretRecipeBook()
        {
            Console.WriteLine("Populating SecretRecipeBook...");
            IEnumerable<PorierFFXIV.SecretRecipeBook> secretRecipeBooks =
                from secretRecipeBook in libraEorzeaDbContext.SecretRecipeBook
                join secretRecipeBookRow in SecretRecipeBookRow.GetRows()
                on secretRecipeBook.Key equals secretRecipeBookRow.Key
                select new PorierFFXIV.SecretRecipeBook
                {
                    Key = secretRecipeBook.Key,
                    TextKo = secretRecipeBookRow.Name,
                    TextJa = secretRecipeBook.TextJa,
                    TextEn = secretRecipeBook.TextEn,
                    TextFr = secretRecipeBook.TextFr,
                    TextDe = secretRecipeBook.TextDe
                };
            int count = 0;
            int total = secretRecipeBooks.Count();
            foreach (PorierFFXIV.SecretRecipeBook secretRecipeBook in secretRecipeBooks)
            {
                WriteLog("SecretRecipeBook", ref count, total);
                porierFFXIVDbContext.SecretRecipeBooks.Add(secretRecipeBook);
            }
            Console.WriteLine("\nSaving SecretRecipeBook...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateShop()
        {
            Console.WriteLine("Populating Shop...");
            var joinedShops =
                from shop in libraEorzeaDbContext.Shop
                join shopRow in ShopRow.GetRows()
                on shop.Key equals shopRow.Key
                select new
                {
                    Shop = shop,
                    ShopRow = shopRow
                };
            int count = 0;
            int total = joinedShops.Count();
            foreach (var joinedShop in joinedShops)
            {
                WriteLog("Shop", ref count, total);

                PorierFFXIV.Shop shop = new PorierFFXIV.Shop
                {
                    Key = joinedShop.Shop.Key,
                    NameKo = joinedShop.ShopRow.Name,
                    NameJa = joinedShop.Shop.NameJa,
                    NameEn = joinedShop.Shop.NameEn,
                    NameFr = joinedShop.Shop.NameFr,
                    NameDe = joinedShop.Shop.NameDe
                };

                if (joinedShop.Shop.BeastTribe != null)
                {
                    shop.BeastTribe = porierFFXIVDbContext.BeastTribes.Find(joinedShop.Shop.BeastTribe);
                }

                if (joinedShop.Shop.Quest != null)
                {
                    shop.Quest = porierFFXIVDbContext.Quests.Find(joinedShop.Shop.Quest);
                }

                porierFFXIVDbContext.Shops.Add(shop);
            }
            Console.WriteLine("\nSaving Shop...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateENpcShopAndItemShop()
        {
            Console.WriteLine("Populating ENpcShop and ItemShop...");
            int count = 0;
            int total = porierFFXIVDbContext.ENpcs.Count();
            foreach (ENpc eNpc in porierFFXIVDbContext.ENpcs)
            {
                WriteLog("ENpcShop and ItemShop", ref count, total);

                JObject data = JObject.Parse(Encoding.ASCII.GetString(eNpc.Data));

                if (data["shop"] != null)
                {
                    foreach (JObject shopObject in data["shop"])
                    {
                        foreach (JProperty shopProperty in shopObject.Children())
                        {
                            if (Int64.TryParse(shopProperty.Name, out long shopKey))
                            {
                                PorierFFXIV.Shop shop = porierFFXIVDbContext.Shops.Find(shopKey);

                                if (shop == null)
                                {
                                    shop = new PorierFFXIV.Shop
                                    {
                                        Key = shopKey,
                                        NameKo = "아이템 거래",
                                        NameJa = "アイテムの購入",
                                        NameEn = "Purchase Items",
                                        NameFr = "Objets",
                                        NameDe = "Waren"
                                    };
                                    porierFFXIVDbContext.Add(shop);
                                }
                                
                                porierFFXIVDbContext.ENpcShops.Add(new ENpcShop
                                {
                                    ENpc = eNpc,
                                    Shop = shop
                                });

                                foreach (JObject itemObject in shopProperty.Value)
                                {
                                    foreach (JProperty itemProperty in itemObject.Children())
                                    {
                                        if (Int64.TryParse(itemProperty.Name, out long itemKey))
                                        {
                                            PorierFFXIV.Item item = porierFFXIVDbContext.Items.Find(itemKey);

                                            if (item != null)
                                            {
                                                porierFFXIVDbContext.ItemShops.Add(new ItemShop
                                                {
                                                    Item = item,
                                                    Shop = shop
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("\nSaving ENpcShop and ItemShop...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateStatus()
        {
            Console.WriteLine("Populating Status...");
            IEnumerable<PorierFFXIV.Status> statuses =
                from status in libraEorzeaDbContext.Status
                join statusRow in StatusRow.GetRows()
                on status.Key equals statusRow.Key
                select new PorierFFXIV.Status
                {
                    Key = status.Key,
                    NameKo = statusRow.Name,
                    NameJa = status.NameJa,
                    NameEn = status.NameEn,
                    NameFr = status.NameFr,
                    NameDe = status.NameDe
                };
            int count = 0;
            int total = statuses.Count();
            foreach (PorierFFXIV.Status status in statuses)
            {
                WriteLog("Status", ref count, total);
                porierFFXIVDbContext.Statuses.Add(status);
            }
            Console.WriteLine("\nSaving Status...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateRecipe()
        {
            Console.WriteLine("Populating Recipe...");
            int count = 0;
            int total = libraEorzeaDbContext.Recipe.Count();
            foreach (LibraEorzea.Recipe recipe in libraEorzeaDbContext.Recipe)
            {
                WriteLog("Recipe", ref count, total);

                PorierFFXIV.Recipe _recipe = new PorierFFXIV.Recipe
                {
                    Key = recipe.Key,
                    CanAutoCraft = recipe.CanAutoCraft == 1L,
                    CanHQ = recipe.CanHq == 1L,
                    CanMasterpiece = recipe.CanMasterpiece == 1L,
                    CraftNum = recipe.CraftNum,
                    Level = recipe.Level,
                    LevelView = recipe.LevelView,
                    LevelDiff = recipe.LevelDiff,
                    NeedCraftmanship = recipe.NeedCraftmanship,
                    NeedControl = recipe.NeedControl,
                    NeedAutoCraftmanship = recipe.NeedAutoCraftmanship,
                    NeedAutoControl = recipe.NeedAutoControl,
                    Data = recipe.Data,
                    Path = recipe.Path
                };

                if (recipe.CraftItemId != null)
                {
                    _recipe.CraftItem = porierFFXIVDbContext.Items.Find(recipe.CraftItemId);
                }

                if (recipe.CraftType != null)
                {
                    long? classJobKey = libraEorzeaDbContext.CraftType.Find(recipe.CraftType).ClassJob;

                    if (classJobKey != null)
                    {
                        _recipe.CraftJob = porierFFXIVDbContext.ClassJobs.Find(classJobKey);
                    }
                }

                if (recipe.Element != null)
                {
                    _recipe.Element = porierFFXIVDbContext.RecipeElements.Find(recipe.Element);
                }

                if (recipe.NeedStatus != null)
                {
                    _recipe.NeedStatus = porierFFXIVDbContext.Statuses.Find(recipe.NeedStatus);
                }

                if (recipe.NeedEquipItem != null)
                {
                    _recipe.NeedEquipItem = porierFFXIVDbContext.Items.Find(recipe.NeedEquipItem);
                }

                if (recipe.NeedSecretRecipeBook != null)
                {
                    _recipe.NeedSecretRecipeBook = porierFFXIVDbContext.SecretRecipeBooks.Find(recipe.NeedSecretRecipeBook);
                }

                JObject data = JObject.Parse(Encoding.ASCII.GetString(_recipe.Data));

                if (data["quality_max"] != null &&
                    Int64.TryParse(data["quality_max"].ToString(), out long qualityMax))
                {
                    _recipe.QualityMax = qualityMax;
                }

                if (data["work_max"] != null &&
                    Int64.TryParse(data["work_max"].ToString(), out long workMax))
                {
                    _recipe.WorkMax = workMax;
                }

                porierFFXIVDbContext.Recipes.Add(_recipe);
            }
            Console.WriteLine("\nSaving Recipe...\n");
            porierFFXIVDbContext.SaveChanges(true);
        }

        static void PopulateCrystalAndRecipeItem()
        {
            Console.WriteLine("Populating Crystal and RecipeItem...");
            int count = 0;
            int total = porierFFXIVDbContext.Recipes.Count();
            foreach (PorierFFXIV.Recipe recipe in porierFFXIVDbContext.Recipes)
            {
                WriteLog("Crystal and RecipeItem", ref count, total);

                JObject data = JObject.Parse(Encoding.ASCII.GetString(recipe.Data));

                if (data["Crystal"] != null)
                {
                    foreach (JObject crystalObject in data["Crystal"])
                    {
                        foreach (JProperty crystalProperty in crystalObject.Children())
                        {
                            if (Int64.TryParse(crystalProperty.Name, out long itemKey) &&
                                Int64.TryParse(crystalProperty.Value.ToString(), out long amount))
                            {
                                PorierFFXIV.Item item = porierFFXIVDbContext.Items.Find(itemKey);

                                if (item != null)
                                {
                                    porierFFXIVDbContext.Crystals.Add(new Crystal
                                    {
                                        Item = item,
                                        Recipe = recipe,
                                        Amount = amount
                                    });
                                }
                            }
                        }
                    }
                }

                if (data["Item"] != null)
                {
                    foreach (JObject itemObject in data["Item"])
                    {
                        foreach (JProperty itemProperty in itemObject.Children())
                        {
                            if (Int64.TryParse(itemProperty.Name, out long itemKey) &&
                                Int64.TryParse(itemProperty.Value.ToString(), out long amount))
                            {
                                PorierFFXIV.Item item = porierFFXIVDbContext.Items.Find(itemKey);

                                if (item != null)
                                {
                                    porierFFXIVDbContext.RecipeItems.Add(new RecipeItem
                                    {
                                        Item = item,
                                        Recipe = recipe,
                                        Amount = amount
                                    });
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("\nSaving Crystal and RecipeItem...");
            porierFFXIVDbContext.SaveChanges(true);
        }
        
        static void Main(string[] args)
        {
            /*
             * 1. REMEMBER TO UPDATE SQLITE AND EXD FILE LOCATIONS!
             * 2. REMEMBER TO UPDATE PORIERFFXIV CONNECTION STRING!
             * 3. REMEMBER TO UPDATE ITEMUIKIND AND GATHERINGJOB KOREAN DICTIONARY IF NECESSARY!
             */
            string libraEorzeaLocation = @"\Downloads\app_data.sqlite";
            CsvConfiguration.EXD_LOCATION = @"\Downloads\exd";
            ItemUiKindDictionary = new Dictionary<long, string>()
            {
                { 1, "무기" },
                { 2, "도구" },
                { 3, "방어구" },
                { 4, "장신구" },
                { 5, "약품 및 요리" },
                { 6, "재료" },
                { 7, "기타" }
            };
            GatheringJobDictionary = new Dictionary<string, string>()
            {
                { "광물 캐기", "광부" },
                { "암석 캐기", "광부" },
                { "나무 베기", "원예가" },
                { "풀 베기", "원예가" }
            };
            
            using (libraEorzeaDbContext = new LibraEorzeaDbContext(libraEorzeaLocation))
            using (porierFFXIVDbContext = new PorierFFXIVDbContext())
            {
                PopulateBaseParam();

                PopulateBeastTribe();

                PopulateClassJob();
                
                PopulateClassJobCategory();
                
                PopulateClassJobClassJobCategory();
                
                PopulateENpc();
                
                PopulateGatheringType();
                
                PopulateGatheringSubCategory();
                
                PopulateItemUIKind();
                
                PopulateItemUICategory();
                
                PopulateItem();
                
                PopulateGathering();
                
                PopulateItemBonus();

                PopulateItemAction();

                PopulateItemClassJob();

                PopulateItemENpc();

                PopulatePlaceName();

                PopulateENpcPlaceName();

                PopulateGatheringPoint();

                PopulateQuest();

                PopulateRecipeElement();

                PopulateSecretRecipeBook();

                PopulateShop();

                PopulateENpcShopAndItemShop();

                PopulateStatus();

                PopulateRecipe();

                PopulateCrystalAndRecipeItem();
            }

            Console.WriteLine("DONE");

            Console.ReadLine();
        }
    }
}
