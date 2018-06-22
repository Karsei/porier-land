using FFXIVTranslator.PorierFFXIV;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PorierLand.Controllers
{
    [Route("api/craft_calculator")]
    public class CraftCalculatorController : Controller
    {
        private readonly PorierFFXIVDbContext _context;

        public CraftCalculatorController(PorierFFXIVDbContext context)
        {
            _context = context;
        }

        [HttpGet("items")]
        public object GetItems([FromQuery(Name = "name")] string name, [FromQuery(Name = "recipe_key")] long? recipeKey, [FromQuery(Name = "get_crystals")] bool? getCrystals)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    return GetItemsByName(name);
                }
                else if (recipeKey != null && getCrystals != null)
                {
                    return GetItemsByRecipeKey((long)recipeKey, (bool)getCrystals);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        
        private ItemSearchResults GetItemsByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new ItemSearchResults
                {
                    results = new List<ItemSearchResult>()
                };
            }

            return new ItemSearchResults
            {
                results = from item in _context.Items
                          where item.UINameKo.ToLower().Contains(name.ToLower()) || item.UINameEn.ToLower().Contains(name.ToLower())
                          select new ItemSearchResult
                          {
                              title = item.UINameKo,
                              price = item.Key.ToString(),
                              description = item.UINameEn
                          }
            };
        }

        private Item GetItem(long key, int requiredAmount)
        {
            Item item = _context.Items
                .Where(_item => key == _item.Key)
                .Select(_item => new Item
                {
                    key = _item.Key,
                    nameKo = _item.UINameKo,
                    nameEn = _item.UINameEn,
                    iconPath = _item.IconPath,
                    requiredAmount = requiredAmount,
                    recipes = _item.CraftRecipes.Select(recipe => new Recipe
                    {
                        key = recipe.Key,
                        level = recipe.LevelView,
                        totalCrafted = recipe.CraftNum,
                        numStarsArray = new int[(int)recipe.LevelDiff],
                        craftingJob = new Job
                        {
                            key = recipe.CraftJob.Key,
                            nameKo = recipe.CraftJob.NameKo,
                            nameEn = recipe.CraftJob.NameEn,
                            iconPath = "/assets/ui/icon/062000/" + ((long)((recipe.CraftJob.Key) + 62100L)).ToString("D6") + ".tex.png"
                        }
                    }).ToArray(),
                    gatherings = _item.Gatherings.Select(gathering => new Gathering
                    {
                        key = gathering.Key,
                        gatheringJob = new Job
                        {
                            key = gathering.GatheringJob.Key,
                            nameKo = gathering.GatheringJob.NameKo,
                            nameEn = gathering.GatheringJob.NameEn,
                            iconPath = "/assets/ui/icon/062000/" + ((long)(gathering.GatheringJob.Key + 62100L)).ToString("D6") + ".tex.png"
                        },
                        numStarsArray = new int[(int)gathering.LevelDiff],
                        isHidden = gathering.IsHidden,
                        gatheringSubCategory =
                            gathering.GatheringSubCategory != null ?
                            new GatheringSubCategory
                            {
                                key = gathering.GatheringSubCategory.Key,
                                nameKo = gathering.GatheringSubCategory.NameKo,
                                nameEn = gathering.GatheringSubCategory.NameEn
                            } : null,
                        gatheringPoints = gathering.GatheringPoints
                            .Where(gatheringPoint => gatheringPoint.Level != null)
                            .Select(gatheringPoint => new GatheringPoint
                            {
                                key = gatheringPoint.Key,
                                level = gatheringPoint.Level,
                                placeKey = gatheringPoint.PlaceName.Key,
                                placeNameKo = gatheringPoint.PlaceName.SGLKo,
                                placeNameEn = gatheringPoint.PlaceName.SGLEn,
                                children = new GatheringPoint[0]
                            }).ToArray()
                    }).ToArray(),
                    shops = _item.ItemShops.Select(itemShop => itemShop.Shop)
                    .Select(shop => new Shop
                    {
                        key = shop.Key,
                        nameKo = shop.NameKo,
                        nameEn = shop.NameEn,
                        beastTribe = shop.BeastTribe != null ? new BeastTribe
                        {
                            key = shop.BeastTribe.Key,
                            nameKo = shop.BeastTribe.SGLKo,
                            nameEn = shop.BeastTribe.SGLEn
                        } : null,
                        quest = shop.Quest != null ? new Quest
                        {
                            key = shop.Quest.Key,
                            nameKo = shop.Quest.NameKo,
                            nameEn = shop.Quest.NameEn
                        } : null,
                        npcs = shop.ENpcShops.Select(eNpcShop => eNpcShop.ENpc)
                        .Select(eNpc => new Npc
                        {
                            key = eNpc.Key,
                            nameKo = eNpc.SGLKo,
                            nameEn = eNpc.SGLEn
                        }).ToArray()
                    }).ToArray()
                }).FirstOrDefault();

            if (item != null)
            {
                List<Gathering> gatherings = new List<Gathering>();

                foreach (Gathering gathering in item.gatherings)
                {
                    Gathering candidate = gatherings.Where(_gathering =>
                        _gathering.gatheringJob.key == gathering.gatheringJob.key &&
                        _gathering.numStarsArray.Length == gathering.numStarsArray.Length &&
                        _gathering.isHidden == gathering.isHidden &&
                        (_gathering.gatheringSubCategory == null ?
                            gathering.gatheringSubCategory == null :
                            _gathering.gatheringSubCategory.key == gathering.gatheringSubCategory.key)).FirstOrDefault();
                    
                    if (candidate != null)
                    {
                        candidate.gatheringPoints = candidate.gatheringPoints.Concat(gathering.gatheringPoints).ToArray();
                    }
                    else
                    {
                        gatherings.Add(gathering);
                    }
                }

                item.gatherings = gatherings.ToArray();

                foreach (Gathering gathering in item.gatherings)
                {
                    while (gathering.gatheringPoints.Any(gatheringPoint => _context.GatheringPoints.Any(_gatheringPoint => _gatheringPoint.Key == gatheringPoint.key && _gatheringPoint.Parent != null)))
                    {
                        List<GatheringPoint> newGatheringPoints = new List<GatheringPoint>();

                        foreach (GatheringPoint gatheringPoint in gathering.gatheringPoints)
                        {
                            GatheringPoint existingGatheringPoint = FindGatheringPointRecursively(newGatheringPoints, gatheringPoint.placeKey);

                            if (existingGatheringPoint != null)
                            {
                                if (existingGatheringPoint.level != null && gatheringPoint.level != null)
                                {
                                    existingGatheringPoint.level = Math.Min((long)existingGatheringPoint.level, (long)gatheringPoint.level);
                                }

                                existingGatheringPoint.children = existingGatheringPoint.children.Concat(gatheringPoint.children).ToArray();

                                continue;
                            }

                            FFXIVTranslator.PorierFFXIV.GatheringPoint parentGatheringPoint = _context.GatheringPoints.Where(_gatheringPoint => _gatheringPoint.Key == gatheringPoint.key)
                                .Select(_gatheringPoint => _gatheringPoint.Parent)
                                .FirstOrDefault();

                            if (parentGatheringPoint == null)
                            {
                                newGatheringPoints.Add(gatheringPoint);
                            }
                            else
                            {
                                GatheringPoint newGatheringPoint = new GatheringPoint
                                {
                                    key = parentGatheringPoint.Key,
                                    level = parentGatheringPoint.Level,
                                    children = new GatheringPoint[1]
                                    {
                                        gatheringPoint
                                    }
                                };

                                PlaceName placeName = _context.GatheringPoints.Where(_gatheringPoint => _gatheringPoint.Key == parentGatheringPoint.Key)
                                    .Select(_gatheringPoint => _gatheringPoint.PlaceName)
                                    .FirstOrDefault();

                                if (placeName != null)
                                {
                                    newGatheringPoint.placeKey = placeName.Key;
                                    newGatheringPoint.placeNameKo = placeName.SGLKo;
                                    newGatheringPoint.placeNameEn = placeName.SGLEn;
                                }

                                GatheringPoint candidate = FindGatheringPointRecursively(newGatheringPoints, newGatheringPoint.placeKey);

                                if (candidate != null)
                                {
                                    if (candidate.level != null && newGatheringPoint.level != null)
                                    {
                                        candidate.level = Math.Min((long)candidate.level, (long)newGatheringPoint.level);
                                    }

                                    candidate.children = candidate.children.Concat(newGatheringPoint.children).ToArray();
                                }
                                else
                                {
                                    newGatheringPoints.Add(newGatheringPoint);
                                }
                            }
                        }

                        gathering.gatheringPoints = newGatheringPoints.ToArray();
                    }
                }

                foreach (Gathering gathering in item.gatherings)
                {
                    MergeNullPlaces(gathering.gatheringPoints);
                }
                
                foreach (Shop shop in item.shops)
                {
                    foreach (Npc npc in shop.npcs)
                    {
                        ENpcPlaceName eNpcPlaceName = _context.ENpcs
                            .Where(eNpc => eNpc.Key == npc.key)
                            .SelectMany(eNpc => eNpc.ENpcPlaceNames)
                            .Where(_eNpcPlaceName => _eNpcPlaceName.PlaceName != null)
                            .FirstOrDefault();

                        if (eNpcPlaceName != null)
                        {
                            npc.x = eNpcPlaceName.X;
                            npc.y = eNpcPlaceName.Y;
                            npc.place = _context.ENpcPlaceNames
                                .Where(_eNpcPlaceName => _eNpcPlaceName.Key == eNpcPlaceName.Key)
                                .Select(_eNpcPlaceName => _eNpcPlaceName.PlaceName)
                                .Select(placeName => new Place
                                {
                                    key = placeName.Key,
                                    nameKo = placeName.SGLKo,
                                    nameEn = placeName.SGLEn
                                })
                                .FirstOrDefault();

                            Place currentPlace = npc.place;

                            while (true)
                            {
                                PlaceName region = _context.PlaceNames.Where(placeName => placeName.Key == currentPlace.key).Select(placeName => placeName.Region).FirstOrDefault();

                                if (region == null) break;

                                currentPlace.parent = new Place
                                {
                                    key = region.Key,
                                    nameKo = region.SGLKo,
                                    nameEn = region.SGLEn
                                };

                                currentPlace = currentPlace.parent;
                            }
                        }
                    }
                }
            }

            return item;
        }

        private GatheringPoint FindGatheringPointRecursively(IEnumerable<GatheringPoint> gatheringPoints, long? placeKey)
        {
            if (placeKey == null) return null;

            foreach (GatheringPoint gatheringPoint in gatheringPoints)
            {
                if (gatheringPoint.children.Length > 0)
                {
                    GatheringPoint candidate = FindGatheringPointRecursively(gatheringPoint.children, placeKey);

                    if (candidate != null) return candidate;
                }

                if (gatheringPoint.placeKey == placeKey) return gatheringPoint;
            }

            return null;
        }

        private void MergeNullPlaces(IEnumerable<GatheringPoint> gatheringPoints)
        {
            foreach (GatheringPoint gatheringPoint in gatheringPoints)
            {
                GatheringPoint[] nullPoints = gatheringPoint.children.Where(child => child.placeKey == null).ToArray();

                foreach (GatheringPoint nullPoint in nullPoints)
                {
                    nullPoint.placeNameKo = "???";
                    nullPoint.placeNameEn = "???";
                }

                if (nullPoints.Length > 1)
                {
                    GatheringPoint nullPoint = nullPoints[0];

                    for (int i = 1; i < nullPoints.Length; i++)
                    {
                        nullPoint.level = Math.Min((long)nullPoint.level, (long)nullPoints[i].level);
                        nullPoint.children = nullPoint.children.Concat(nullPoints[i].children).ToArray();
                    }

                    List<GatheringPoint> newList = gatheringPoint.children.Where(child => child.placeKey != null).ToList();
                    newList.Add(nullPoint);
                    gatheringPoint.children = newList.ToArray();
                }

                MergeNullPlaces(gatheringPoint.children);
            }
        }

        [HttpGet("item")]
        public Item GetItemByKey([FromQuery(Name = "key")] long key)
        {
            try
            {
                return GetItem(key, 0);
            }
            catch
            {
                return null;
            }
        }
        
        private Item[] GetItemsByRecipeKey(long recipeKey, bool getCrystals)
        {
            FFXIVTranslator.PorierFFXIV.Recipe recipe = _context.Recipes
                .Where(_recipe => _recipe.Key.Equals(recipeKey))
                .FirstOrDefault();

            if (recipe == null) return new Item[0];

            IEnumerable<Tuple<long, int>> tuples = null;

            if (getCrystals)
            {
                tuples = _context.RecipeItems
                    .Where(recipeItem => recipeItem.Recipe.Key == recipeKey)
                    .Select(recipeItem => new Tuple<long, int>(recipeItem.Item.Key, (int)recipeItem.Amount))
                    .Concat(_context.Crystals
                        .Where(crystal => crystal.Recipe.Key == recipeKey)
                        .Select(crystal => new Tuple<long, int>(crystal.Item.Key, (int)crystal.Amount)));
            }
            else
            {
                tuples = _context.RecipeItems
                    .Where(recipeItem => recipeItem.Recipe.Key == recipeKey)
                    .Select(recipeItem => new Tuple<long, int>(recipeItem.Item.Key, (int)recipeItem.Amount));
            }

            if (tuples == null) return new Item[0];

            List<Item> requiredItems = new List<Item>();

            foreach (Tuple<long, int> tuple in tuples)
            {
                requiredItems.Add(GetItem(tuple.Item1, tuple.Item2));
            }

            return requiredItems.ToArray();
        }

        public class ItemSearchResults
        {
            public IEnumerable<ItemSearchResult> results { get; set; }
        }

        public class ItemSearchResult
        {
            public string title { get; set; }
            public string price { get; set; }
            public string description { get; set; }
        }

        public class Item
        {
            public long? key { get; set; }
            public string nameKo { get; set; }
            public string nameEn { get; set; }
            public string iconPath { get; set; }
            public int requiredAmount { get; set; }

            public Recipe[] recipes { get; set; }
            public Gathering[] gatherings { get; set; }
            public Shop[] shops { get; set; }
        }

        public class Recipe
        {
            public long? key { get; set; }
            public long? level { get; set; }
            public long? totalCrafted { get; set; }
            public int[] numStarsArray { get; set; }
            public Job craftingJob { get; set; }
        }

        public class Job
        {
            public long key { get; set; }
            public string nameKo { get; set; }
            public string nameEn { get; set; }
            public string iconPath { get; set; }
        }

        public class Gathering
        {
            public long? key { get; set; }
            public Job gatheringJob { get; set; }
            public int[] numStarsArray { get; set; }
            public bool isHidden { get; set; }
            public GatheringSubCategory gatheringSubCategory { get; set; }

            public GatheringPoint[] gatheringPoints { get; set; }
        }

        public class GatheringSubCategory
        {
            public long? key { get; set; }
            public string nameKo { get; set; }
            public string nameEn { get; set; }
        }

        public class GatheringPoint
        {
            public long? key { get; set; }
            public long? level { get; set; }
            public long? placeKey { get; set; }
            public string placeNameKo { get; set; }
            public string placeNameEn { get; set; }

            public GatheringPoint[] children { get; set; }
        }

        public class Shop
        {
            public long? key { get; set; }
            public string nameKo { get; set; }
            public string nameEn { get; set; }

            public BeastTribe beastTribe { get; set; }
            public Quest quest { get; set; }

            public Npc[] npcs { get; set; }
        }

        public class BeastTribe
        {
            public long? key { get; set; }
            public string nameKo { get; set; }
            public string nameEn { get; set; }
        }

        public class Quest
        {
            public long? key { get; set; }
            public string nameKo { get; set; }
            public string nameEn { get; set; }
        }

        public class Npc
        {
            public long? key { get; set; }
            public string nameKo { get; set; }
            public string nameEn { get; set; }
            public double x { get; set; }
            public double y { get; set; }
            public Place place { get; set; }
        }

        public class Place
        {
            public long? key { get; set; }
            public string nameKo { get; set; }
            public string nameEn { get; set; }
            public Place parent;
        }
    }
}