using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FFXIVTranslator.LibraEorzea
{
    public partial class LibraEorzeaDbContext : DbContext
    {
        public virtual DbSet<Achievement> Achievement { get; set; }
        public virtual DbSet<AchievementCategory> AchievementCategory { get; set; }
        public virtual DbSet<AchievementKind> AchievementKind { get; set; }
        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<BaseParam> BaseParam { get; set; }
        public virtual DbSet<BeastTribe> BeastTribe { get; set; }
        public virtual DbSet<BnpcName> BnpcName { get; set; }
        public virtual DbSet<BnpcNamePlaceName> BnpcNamePlaceName { get; set; }
        public virtual DbSet<ClassJob> ClassJob { get; set; }
        public virtual DbSet<ClassJobCategory> ClassJobCategory { get; set; }
        public virtual DbSet<ClassJobClassJobCategory> ClassJobClassJobCategory { get; set; }
        public virtual DbSet<Colosseum> Colosseum { get; set; }
        public virtual DbSet<ContentRoulette> ContentRoulette { get; set; }
        public virtual DbSet<ContentType> ContentType { get; set; }
        public virtual DbSet<CraftType> CraftType { get; set; }
        public virtual DbSet<Emote> Emote { get; set; }
        public virtual DbSet<EnpcResident> EnpcResident { get; set; }
        public virtual DbSet<EnpcResidentPlaceName> EnpcResidentPlaceName { get; set; }
        public virtual DbSet<EnpcResidentQuest> EnpcResidentQuest { get; set; }
        public virtual DbSet<ExVersion> ExVersion { get; set; }
        public virtual DbSet<Fchierarchy> Fchierarchy { get; set; }
        public virtual DbSet<Fcrank> Fcrank { get; set; }
        public virtual DbSet<Fcreputation> Fcreputation { get; set; }
        public virtual DbSet<Frontline> Frontline { get; set; }
        public virtual DbSet<Frontline01> Frontline01 { get; set; }
        public virtual DbSet<Frontline02> Frontline02 { get; set; }
        public virtual DbSet<Gathering> Gathering { get; set; }
        public virtual DbSet<GatheringSubCategory> GatheringSubCategory { get; set; }
        public virtual DbSet<GatheringType> GatheringType { get; set; }
        public virtual DbSet<GcrankGridaniaFemaleText> GcrankGridaniaFemaleText { get; set; }
        public virtual DbSet<GcrankGridaniaMaleText> GcrankGridaniaMaleText { get; set; }
        public virtual DbSet<GcrankLimsaFemaleText> GcrankLimsaFemaleText { get; set; }
        public virtual DbSet<GcrankLimsaMaleText> GcrankLimsaMaleText { get; set; }
        public virtual DbSet<GcrankUldahFemaleText> GcrankUldahFemaleText { get; set; }
        public virtual DbSet<GcrankUldahMaleText> GcrankUldahMaleText { get; set; }
        public virtual DbSet<GeneralAction> GeneralAction { get; set; }
        public virtual DbSet<GrandCompany> GrandCompany { get; set; }
        public virtual DbSet<GuardianDeity> GuardianDeity { get; set; }
        public virtual DbSet<GuildOrder> GuildOrder { get; set; }
        public virtual DbSet<InstanceContent> InstanceContent { get; set; }
        public virtual DbSet<InstanceContentType> InstanceContentType { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemCategory> ItemCategory { get; set; }
        public virtual DbSet<ItemClassJob> ItemClassJob { get; set; }
        public virtual DbSet<ItemSeries> ItemSeries { get; set; }
        public virtual DbSet<ItemSpecialBonus> ItemSpecialBonus { get; set; }
        public virtual DbSet<ItemUicategory> ItemUicategory { get; set; }
        public virtual DbSet<ItemUikind> ItemUikind { get; set; }
        public virtual DbSet<JournalCategory> JournalCategory { get; set; }
        public virtual DbSet<JournalGenre> JournalGenre { get; set; }
        public virtual DbSet<JournalSection> JournalSection { get; set; }
        public virtual DbSet<LodestoneSystemDefine> LodestoneSystemDefine { get; set; }
        public virtual DbSet<NotebookDivision> NotebookDivision { get; set; }
        public virtual DbSet<PlaceName> PlaceName { get; set; }
        public virtual DbSet<Purify> Purify { get; set; }
        public virtual DbSet<Quest> Quest { get; set; }
        public virtual DbSet<QuestClassJob> QuestClassJob { get; set; }
        public virtual DbSet<QuestRewardOther> QuestRewardOther { get; set; }
        public virtual DbSet<QuestWebEx> QuestWebEx { get; set; }
        public virtual DbSet<QuestWebType> QuestWebType { get; set; }
        public virtual DbSet<Race> Race { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<RecipeElement> RecipeElement { get; set; }
        public virtual DbSet<SecretRecipeBook> SecretRecipeBook { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Title> Title { get; set; }
        public virtual DbSet<Tomestones> Tomestones { get; set; }
        public virtual DbSet<Town> Town { get; set; }
        public virtual DbSet<Trait> Trait { get; set; }

        // Unable to generate entity type for table 'app_data'. Please see the warning messages.
        private string location;

        public LibraEorzeaDbContext(string location)
        {
            this.location = location;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=" + location);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Category)
                    .HasName("Achievement_Category_index");

                entity.HasIndex(e => e.Key)
                    .HasName("Achievement_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Category).HasColumnType("integer unsigned");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.HelpDe)
                    .HasColumnName("Help_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HelpEn)
                    .HasColumnName("Help_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HelpFr)
                    .HasColumnName("Help_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HelpJa)
                    .HasColumnName("Help_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Icon).HasColumnType("integer unsigned");

                entity.Property(e => e.IndexDe).HasColumnName("Index_de");

                entity.Property(e => e.IndexEn).HasColumnName("Index_en");

                entity.Property(e => e.IndexFr).HasColumnName("Index_fr");

                entity.Property(e => e.IndexJa).HasColumnName("Index_ja");

                entity.Property(e => e.Item).HasColumnType("integer unsigned");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.Point).HasColumnType("integer unsigned");

                entity.Property(e => e.Priority).HasColumnType("integer unsigned");

                entity.Property(e => e.Title).HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<AchievementCategory>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Key)
                    .HasName("AchievementCategory_Key_index");

                entity.HasIndex(e => e.Kind)
                    .HasName("AchievementCategory_Kind_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Kind).HasColumnType("integer unsigned");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<AchievementKind>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Key)
                    .HasName("AchievementKind_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Action>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.HelpWebDe)
                    .HasColumnName("HelpWeb_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HelpWebEn)
                    .HasColumnName("HelpWeb_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HelpWebFr)
                    .HasColumnName("HelpWeb_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HelpWebJa)
                    .HasColumnName("HelpWeb_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Icon).HasColumnType("integer unsigned");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<BaseParam>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<BeastTribe>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.ReputationValueDe)
                    .HasColumnName("ReputationValue_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ReputationValueEn)
                    .HasColumnName("ReputationValue_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ReputationValueFr)
                    .HasColumnName("ReputationValue_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ReputationValueJa)
                    .HasColumnName("ReputationValue_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglDe)
                    .HasColumnName("SGL_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglEn)
                    .HasColumnName("SGL_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglFr)
                    .HasColumnName("SGL_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglJa)
                    .HasColumnName("SGL_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<BnpcName>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("BNpcName");

                entity.HasIndex(e => e.Key)
                    .HasName("BnpcName_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.IndexDe).HasColumnName("Index_de");

                entity.Property(e => e.IndexEn).HasColumnName("Index_en");

                entity.Property(e => e.IndexFr).HasColumnName("Index_fr");

                entity.Property(e => e.IndexJa).HasColumnName("Index_ja");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.SglDe)
                    .HasColumnName("SGL_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglEn)
                    .HasColumnName("SGL_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglFr)
                    .HasColumnName("SGL_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglJa)
                    .HasColumnName("SGL_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<BnpcNamePlaceName>(entity =>
            {
                entity.HasKey(e => new { e.BnpcNameKey, e.PlaceNameKey, e.Region });

                entity.ToTable("BNpcName_PlaceName");

                entity.HasIndex(e => e.BnpcNameKey)
                    .HasName("BnpcName_PlaceName_BnpcName_Key_index");

                entity.HasIndex(e => e.PlaceNameKey)
                    .HasName("BnpcName_PlaceName_PlaceName_Key_index");

                entity.HasIndex(e => e.Region)
                    .HasName("BnpcName_PlaceName_region_index");

                entity.Property(e => e.BnpcNameKey).HasColumnName("BNpcName_Key");

                entity.Property(e => e.PlaceNameKey).HasColumnName("PlaceName_Key");

                entity.Property(e => e.Region).HasColumnName("region");
            });

            modelBuilder.Entity<ClassJob>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Key)
                    .HasName("ClassJob_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer nusigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.AbbreviationDe)
                    .HasColumnName("Abbreviation_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.AbbreviationEn)
                    .HasColumnName("Abbreviation_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.AbbreviationFr)
                    .HasColumnName("Abbreviation_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.AbbreviationJa)
                    .HasColumnName("Abbreviation_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Filebase)
                    .HasColumnName("filebase")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsJob).HasColumnType("integer nunsigned");

                entity.Property(e => e.Label).HasColumnType("varchar(255)");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Uipriority)
                    .HasColumnName("UIPriority")
                    .HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<ClassJobCategory>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Classjob)
                    .HasColumnName("classjob")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ClassJobClassJobCategory>(entity =>
            {
                entity.HasKey(e => new { e.ClassJobKey, e.ClassJobCategoryKey });

                entity.ToTable("ClassJob_ClassJobCategory");

                entity.Property(e => e.ClassJobKey)
                    .HasColumnName("ClassJob_Key")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.ClassJobCategoryKey)
                    .HasColumnName("ClassJobCategory_Key")
                    .HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<Colosseum>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.LosePoint).HasColumnType("integer unsigned");

                entity.Property(e => e.LoseToken).HasColumnType("integer unsigned");

                entity.Property(e => e.WinPoint).HasColumnType("integer unsigned");

                entity.Property(e => e.WinToken).HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<ContentRoulette>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ContentType>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Key)
                    .HasName("ContentType_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<CraftType>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.ClassJob)
                    .HasName("CraftType_ClassJob_index");

                entity.HasIndex(e => e.Key)
                    .HasName("CraftType_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassJob).HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<Emote>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Icon).HasColumnType("integer unsigned");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<EnpcResident>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("ENpcResident");

                entity.HasIndex(e => e.Area)
                    .HasName("EnpcResident_Area_index");

                entity.HasIndex(e => e.HasQuest)
                    .HasName("ENpcResident_has_quest_index");

                entity.HasIndex(e => e.HasShop)
                    .HasName("ENpcResident_has_shop_index");

                entity.HasIndex(e => e.Key)
                    .HasName("EnpcResident_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.HasConditionShop)
                    .HasColumnName("has_condition_shop")
                    .HasColumnType("boolean");

                entity.Property(e => e.HasQuest)
                    .HasColumnName("has_quest")
                    .HasColumnType("boolean");

                entity.Property(e => e.HasShop)
                    .HasColumnName("has_shop")
                    .HasColumnType("boolean");

                entity.Property(e => e.IndexDe).HasColumnName("Index_de");

                entity.Property(e => e.IndexEn).HasColumnName("Index_en");

                entity.Property(e => e.IndexFr).HasColumnName("Index_fr");

                entity.Property(e => e.IndexJa).HasColumnName("Index_ja");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.SglDe)
                    .HasColumnName("SGL_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglEn)
                    .HasColumnName("SGL_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglFr)
                    .HasColumnName("SGL_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglJa)
                    .HasColumnName("SGL_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<EnpcResidentPlaceName>(entity =>
            {
                entity.HasKey(e => new { e.EnpcResidentKey, e.PlaceNameKey, e.Region });

                entity.ToTable("ENpcResident_PlaceName");

                entity.HasIndex(e => e.EnpcResidentKey)
                    .HasName("ENpcResident_PlaceName_ENpcResident_Key_index");

                entity.HasIndex(e => e.PlaceNameKey)
                    .HasName("ENpcResident_PlaceName_PlaceName_Key_index");

                entity.HasIndex(e => e.Region)
                    .HasName("ENpcResident_PlaceName_region_index");

                entity.Property(e => e.EnpcResidentKey).HasColumnName("ENpcResident_Key");

                entity.Property(e => e.PlaceNameKey).HasColumnName("PlaceName_Key");

                entity.Property(e => e.Region).HasColumnName("region");
            });

            modelBuilder.Entity<EnpcResidentQuest>(entity =>
            {
                entity.HasKey(e => new { e.EnpcResidentKey, e.QuestKey });

                entity.ToTable("ENpcResident_Quest");

                entity.Property(e => e.EnpcResidentKey).HasColumnName("ENpcResident_Key");

                entity.Property(e => e.QuestKey).HasColumnName("Quest_Key");
            });

            modelBuilder.Entity<ExVersion>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.TextDe)
                    .HasColumnName("Text_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextEn)
                    .HasColumnName("Text_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextFr)
                    .HasColumnName("Text_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextJa)
                    .HasColumnName("Text_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Fchierarchy>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("FCHierarchy");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Fcrank>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("FCRank");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.CurrentPoint).HasColumnType("integer unsigned");

                entity.Property(e => e.NextPoint).HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<Fcreputation>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("FCReputation");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Color).HasColumnType("integer unsigned");

                entity.Property(e => e.CurrentPoint).HasColumnType("integer unsigned");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.NextPoint).HasColumnType("integer unsigned");

                entity.Property(e => e.TextDe)
                    .HasColumnName("Text_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextEn)
                    .HasColumnName("Text_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextFr)
                    .HasColumnName("Text_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextJa)
                    .HasColumnName("Text_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Frontline>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.WinPvPexp)
                    .HasColumnName("WinPvPExp")
                    .HasColumnType("text unsigned");

                entity.Property(e => e.WinPvPtoken)
                    .HasColumnName("WinPvPToken")
                    .HasColumnType("text unsigned");
            });

            modelBuilder.Entity<Frontline01>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.WinPvPexp)
                    .HasColumnName("WinPvPExp")
                    .HasColumnType("text unsigned");

                entity.Property(e => e.WinPvPtoken)
                    .HasColumnName("WinPvPToken")
                    .HasColumnType("text unsigned");
            });

            modelBuilder.Entity<Frontline02>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.WinPvPexp)
                    .HasColumnName("WinPvPExp")
                    .HasColumnType("text unsigned");

                entity.Property(e => e.WinPvPtoken)
                    .HasColumnName("WinPvPToken")
                    .HasColumnType("text unsigned");
            });

            modelBuilder.Entity<Gathering>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.GatheringType)
                    .HasName("Gathering_GatheringType_index");

                entity.HasIndex(e => e.Item)
                    .HasName("Gathering_Item_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.GatheringItemNo).HasColumnType("integer unsigned");

                entity.Property(e => e.GatheringNotebookList).HasColumnType("integer unsigned");

                entity.Property(e => e.GatheringSubCategory).HasColumnType("integer unsigned");

                entity.Property(e => e.GatheringType).HasColumnType("integer unsigned");

                entity.Property(e => e.IndexDe).HasColumnName("Index_de");

                entity.Property(e => e.IndexEn).HasColumnName("Index_en");

                entity.Property(e => e.IndexFr).HasColumnName("Index_fr");

                entity.Property(e => e.IndexJa).HasColumnName("Index_ja");

                entity.Property(e => e.IsHidden)
                    .HasColumnName("is_hidden")
                    .HasColumnType("boolean");

                entity.Property(e => e.Item).HasColumnType("integer unsigned");

                entity.Property(e => e.Level).HasColumnType("integer unsigned");

                entity.Property(e => e.LevelDiff)
                    .HasColumnName("levelDiff")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.LevelView)
                    .HasColumnName("levelView")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.Path).HasColumnName("path");
            });

            modelBuilder.Entity<GatheringSubCategory>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Division).HasColumnType("integer unsigned");

                entity.Property(e => e.GatheringType).HasColumnType("integer unsigned");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<GatheringType>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Key)
                    .HasName("GatheringType_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.TextDe)
                    .HasColumnName("Text_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextEn)
                    .HasColumnName("Text_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextFr)
                    .HasColumnName("Text_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextJa)
                    .HasColumnName("Text_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<GcrankGridaniaFemaleText>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("GCRankGridaniaFemaleText");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.SglDe)
                    .HasColumnName("SGL_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglEn)
                    .HasColumnName("SGL_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglFr)
                    .HasColumnName("SGL_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglJa)
                    .HasColumnName("SGL_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<GcrankGridaniaMaleText>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("GCRankGridaniaMaleText");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.SglDe)
                    .HasColumnName("SGL_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglEn)
                    .HasColumnName("SGL_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglFr)
                    .HasColumnName("SGL_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglJa)
                    .HasColumnName("SGL_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<GcrankLimsaFemaleText>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("GCRankLimsaFemaleText");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.SglDe)
                    .HasColumnName("SGL_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglEn)
                    .HasColumnName("SGL_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglFr)
                    .HasColumnName("SGL_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglJa)
                    .HasColumnName("SGL_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<GcrankLimsaMaleText>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("GCRankLimsaMaleText");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.SglDe)
                    .HasColumnName("SGL_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglEn)
                    .HasColumnName("SGL_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglFr)
                    .HasColumnName("SGL_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglJa)
                    .HasColumnName("SGL_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<GcrankUldahFemaleText>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("GCRankUldahFemaleText");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.SglDe)
                    .HasColumnName("SGL_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglEn)
                    .HasColumnName("SGL_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglFr)
                    .HasColumnName("SGL_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglJa)
                    .HasColumnName("SGL_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<GcrankUldahMaleText>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("GCRankUldahMaleText");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.SglDe)
                    .HasColumnName("SGL_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglEn)
                    .HasColumnName("SGL_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglFr)
                    .HasColumnName("SGL_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglJa)
                    .HasColumnName("SGL_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<GeneralAction>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Icon).HasColumnType("integer unsigned");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<GrandCompany>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.SglDe)
                    .HasColumnName("SGL_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglEn)
                    .HasColumnName("SGL_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglFr)
                    .HasColumnName("SGL_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglJa)
                    .HasColumnName("SGL_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<GuardianDeity>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<GuildOrder>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Exp00).HasColumnType("integer unsigned");

                entity.Property(e => e.Exp01).HasColumnType("integer unsigned");

                entity.Property(e => e.Gil00).HasColumnType("integer unsigned");

                entity.Property(e => e.Gil01).HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<InstanceContent>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.ContentType)
                    .HasName("InstanceContent_ContentType_index");

                entity.HasIndex(e => e.Type)
                    .HasName("InstanceContent_Type_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.AcceptClassJobCategory).HasColumnType("integer unsigned");

                entity.Property(e => e.Alliance).HasColumnType("boolean");

                entity.Property(e => e.Area).HasColumnType("integer unsigned");

                entity.Property(e => e.AttackerCount).HasColumnType("integer unsigned");

                entity.Property(e => e.Colosseum).HasColumnType("integer unsigned");

                entity.Property(e => e.ContentType).HasColumnType("integer unsigned");

                entity.Property(e => e.ContentsDe)
                    .HasColumnName("Contents_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ContentsEn)
                    .HasColumnName("Contents_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ContentsFr)
                    .HasColumnName("Contents_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ContentsJa)
                    .HasColumnName("Contents_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.DescriptionDe)
                    .HasColumnName("Description_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DescriptionEn)
                    .HasColumnName("Description_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DescriptionFr)
                    .HasColumnName("Description_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DescriptionJa)
                    .HasColumnName("Description_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DifferentiateDps)
                    .HasColumnName("DifferentiateDPS")
                    .HasColumnType("boolean");

                entity.Property(e => e.EnableItemLimit).HasColumnType("boolean");

                entity.Property(e => e.EnableLootMode).HasColumnType("boolean");

                entity.Property(e => e.EnableUsingItem).HasColumnType("boolean");

                entity.Property(e => e.EntryPartyMemberNum).HasColumnType("integer unsigned");

                entity.Property(e => e.ExVersion).HasColumnType("integer unsigned");

                entity.Property(e => e.FinderPartyCondition).HasColumnType("integer unsigned");

                entity.Property(e => e.ForceCount).HasColumnType("integer unsigned");

                entity.Property(e => e.FreeRole).HasColumnType("boolean");

                entity.Property(e => e.Halfway).HasColumnType("boolean");

                entity.Property(e => e.HealerCount).HasColumnType("integer unsigned");

                entity.Property(e => e.IndexDe).HasColumnName("Index_de");

                entity.Property(e => e.IndexEn).HasColumnName("Index_en");

                entity.Property(e => e.IndexFr).HasColumnName("Index_fr");

                entity.Property(e => e.IndexJa).HasColumnName("Index_ja");

                entity.Property(e => e.IsFeast)
                    .HasColumnName("is_feast")
                    .HasColumnType("boolean");

                entity.Property(e => e.IsKoeruAnnihilation)
                    .HasColumnName("is_koeru_annihilation")
                    .HasColumnType("boolean");

                entity.Property(e => e.IsKoeruUsually)
                    .HasColumnName("is_koeru_usually")
                    .HasColumnType("boolean");

                entity.Property(e => e.ItemLevel).HasColumnType("integer unsigned");

                entity.Property(e => e.ItemLevelMax).HasColumnType("integer unsigned");

                entity.Property(e => e.LevelMax).HasColumnType("integer unsigned");

                entity.Property(e => e.LevelMin).HasColumnType("integer unsigned");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.PartyCount).HasColumnType("integer unsigned");

                entity.Property(e => e.PartyMemberCount).HasColumnType("integer unsigned");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.RandomContentType).HasColumnType("integer unsigned");

                entity.Property(e => e.RangeCount).HasColumnType("integer unsigned");

                entity.Property(e => e.RateChange).HasColumnType("boolean");

                entity.Property(e => e.RateMatch).HasColumnType("boolean");

                entity.Property(e => e.SmallParty).HasColumnType("boolean");

                entity.Property(e => e.Sortkey).HasColumnType("integer unsigned");

                entity.Property(e => e.TankCount).HasColumnType("integer unsigned");

                entity.Property(e => e.Time).HasColumnType("integer unsigned");

                entity.Property(e => e.Type).HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<InstanceContentType>(entity =>
            {
                entity.HasKey(e => e.Type);

                entity.HasIndex(e => e.Type)
                    .HasName("InstanceContentType_Type_index");

                entity.Property(e => e.Type)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Sortkey).HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.EquipLevel)
                    .HasName("Item_EquipLevel_index");

                entity.HasIndex(e => e.Key)
                    .HasName("Item_Key_index");

                entity.HasIndex(e => e.Legacy)
                    .HasName("Item_Legacy_index");

                entity.HasIndex(e => e.Level)
                    .HasName("Item_Level_index");

                entity.HasIndex(e => e.Uicategory)
                    .HasName("Item_UICategory_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.AttackInterval).HasColumnType("integer unsigned");

                entity.Property(e => e.AttackIntervalHq)
                    .HasColumnName("AttackInterval_hq")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.AutoAttack).HasColumnType("integer unsigned");

                entity.Property(e => e.AutoAttackHq)
                    .HasColumnName("AutoAttack_hq")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.Category).HasColumnType("integer unsigned");

                entity.Property(e => e.Classjob)
                    .HasColumnName("classjob")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Damage).HasColumnType("integer unsigned");

                entity.Property(e => e.DamageHq)
                    .HasColumnName("Damage_hq")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.Defense).HasColumnType("integer unsigned");

                entity.Property(e => e.DefenseHq)
                    .HasColumnName("Defense_hq")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.EquipLevel).HasColumnType("integer unsigned");

                entity.Property(e => e.HelpDe)
                    .HasColumnName("Help_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HelpEn)
                    .HasColumnName("Help_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HelpFr)
                    .HasColumnName("Help_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.HelpJa)
                    .HasColumnName("Help_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Hq)
                    .HasColumnName("HQ")
                    .HasColumnType("boolean");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IconHq)
                    .HasColumnName("icon_hq")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IndexDe).HasColumnName("Index_de");

                entity.Property(e => e.IndexEn).HasColumnName("Index_en");

                entity.Property(e => e.IndexFr).HasColumnName("Index_fr");

                entity.Property(e => e.IndexJa).HasColumnName("Index_ja");

                entity.Property(e => e.Legacy)
                    .HasColumnName("legacy")
                    .HasColumnType("boolean");

                entity.Property(e => e.Level).HasColumnType("integer unsigned");

                entity.Property(e => e.MagicDamage).HasColumnType("integer unsigned");

                entity.Property(e => e.MagicDamageHq)
                    .HasColumnName("MagicDamage_hq")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.MagicDefense).HasColumnType("integer unsigned");

                entity.Property(e => e.MagicDefenseHq)
                    .HasColumnName("MagicDefense_hq")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.MateriaProhibition).HasColumnType("boolean unsigned");

                entity.Property(e => e.MirageItem).HasColumnType("integer unsigned");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.Price).HasColumnType("integer unsigned");

                entity.Property(e => e.PriceMin).HasColumnType("integer unsigned");

                entity.Property(e => e.Purify).HasColumnType("integer unsigned");

                entity.Property(e => e.Rarity).HasColumnType("integer unsigned");

                entity.Property(e => e.Salvage).HasColumnType("integer unsigned");

                entity.Property(e => e.SearchCategory).HasColumnType("integer unsigned");

                entity.Property(e => e.Series).HasColumnType("integer unsigned");

                entity.Property(e => e.ShieldBlockRate).HasColumnType("integer unsigned");

                entity.Property(e => e.ShieldBlockRateHq)
                    .HasColumnName("ShieldBlockRate_hq")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.ShieldRate).HasColumnType("integer unsigned");

                entity.Property(e => e.ShieldRateHq)
                    .HasColumnName("ShieldRate_hq")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.Slot).HasColumnType("integer unsigned");

                entity.Property(e => e.SpecialBonus).HasColumnType("integer unsigned");

                entity.Property(e => e.SpecialBonusArg).HasColumnType("integer unsigned");

                entity.Property(e => e.Uicategory)
                    .HasColumnName("UICategory")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.UinameDe)
                    .HasColumnName("UIName_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UinameEn)
                    .HasColumnName("UIName_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UinameFr)
                    .HasColumnName("UIName_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UinameJa)
                    .HasColumnName("UIName_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ItemClassJob>(entity =>
            {
                entity.HasKey(e => new { e.ItemKey, e.ClassJobKey });

                entity.ToTable("Item_ClassJob");

                entity.Property(e => e.ItemKey).HasColumnName("Item_Key");

                entity.Property(e => e.ClassJobKey).HasColumnName("ClassJob_Key");
            });

            modelBuilder.Entity<ItemSeries>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ItemSpecialBonus>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ItemUicategory>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("ItemUICategory");

                entity.HasIndex(e => e.Key)
                    .HasName("ItemUICategory_Key_index");

                entity.HasIndex(e => e.Kind)
                    .HasName("ItemUICategory_Kind_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Kind).HasColumnType("integer unsigned");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Priority).HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<ItemUikind>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("ItemUIKind");

                entity.HasIndex(e => e.Key)
                    .HasName("ItemUIKind_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<JournalCategory>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Key)
                    .HasName("JournalCategory_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Label)
                    .HasColumnName("label")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Section).HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<JournalGenre>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Category)
                    .HasName("JournalGenre_Category_index");

                entity.HasIndex(e => e.Key)
                    .HasName("JournalGenre_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Category).HasColumnType("integer unsigned");

                entity.Property(e => e.Label)
                    .HasColumnName("label")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<JournalSection>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<LodestoneSystemDefine>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.DefineName).HasColumnType("varchar(255)");

                entity.Property(e => e.DefineValue).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<NotebookDivision>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Key)
                    .HasName("NotebookDivision_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<PlaceName>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Key)
                    .HasName("PlaceName_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Region)
                    .HasColumnName("region")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.SglDe)
                    .HasColumnName("SGL_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglEn)
                    .HasColumnName("SGL_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglFr)
                    .HasColumnName("SGL_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.SglJa)
                    .HasColumnName("SGL_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Purify>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Class).HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<Quest>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.Area)
                    .HasName("Quest_Area_index");

                entity.HasIndex(e => e.Genre)
                    .HasName("Quest_Genre_index");

                entity.HasIndex(e => e.Key)
                    .HasName("Quest_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.AllaganTomestoneCondition).HasColumnType("integer unsigned");

                entity.Property(e => e.Area).HasColumnType("integer unsigned");

                entity.Property(e => e.BeastReputationValueNum).HasColumnType("integer unsigned");

                entity.Property(e => e.BeastTribe).HasColumnType("integer unsigned");

                entity.Property(e => e.ClassJob).HasColumnType("integer unsigned");

                entity.Property(e => e.ClassJob2).HasColumnType("integer unsigned");

                entity.Property(e => e.ClassLevel).HasColumnType("integer unsigned");

                entity.Property(e => e.ClassLevel2).HasColumnType("integer unsigned");

                entity.Property(e => e.ClassLevelUpperLimit).HasColumnType("integer unsigned");

                entity.Property(e => e.Client).HasColumnType("integer unsigned");

                entity.Property(e => e.CompanyPointNum).HasColumnType("integer unsigned");

                entity.Property(e => e.CompanyPointType).HasColumnType("integer unsigned");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.ExpBonus).HasColumnType("integer unsigned");

                entity.Property(e => e.Genre).HasColumnType("integer unsigned");

                entity.Property(e => e.Gil).HasColumnType("integer unsigned");

                entity.Property(e => e.Header).HasColumnType("integer unsigned");

                entity.Property(e => e.IndexDe).HasColumnName("Index_de");

                entity.Property(e => e.IndexEn).HasColumnName("Index_en");

                entity.Property(e => e.IndexFr).HasColumnName("Index_fr");

                entity.Property(e => e.IndexJa).HasColumnName("Index_ja");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.QuestLevelOffset).HasColumnType("integer unsigned");

                entity.Property(e => e.Sort).HasColumnType("integer unsigned");

                entity.Property(e => e.WebType).HasColumnType("integer unsigned");
            });

            modelBuilder.Entity<QuestClassJob>(entity =>
            {
                entity.HasKey(e => new { e.QuestKey, e.ClassJobKey });

                entity.ToTable("Quest_ClassJob");

                entity.Property(e => e.QuestKey).HasColumnName("Quest_Key");

                entity.Property(e => e.ClassJobKey).HasColumnName("ClassJob_Key");
            });

            modelBuilder.Entity<QuestRewardOther>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<QuestWebEx>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<QuestWebType>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.TextDe)
                    .HasColumnName("Text_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextEn)
                    .HasColumnName("Text_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextFr)
                    .HasColumnName("Text_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextJa)
                    .HasColumnName("Text_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFemaleDe)
                    .HasColumnName("NameFemale_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFemaleEn)
                    .HasColumnName("NameFemale_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFemaleFr)
                    .HasColumnName("NameFemale_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFemaleJa)
                    .HasColumnName("NameFemale_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.HasIndex(e => e.CraftItemId)
                    .HasName("Recipe_CraftItemId_index");

                entity.HasIndex(e => e.CraftType)
                    .HasName("Recipe_CraftType_index");

                entity.HasIndex(e => e.Key)
                    .HasName("Recipe_Key_index");

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.CanAutoCraft).HasColumnType("integer unsigned");

                entity.Property(e => e.CanHq).HasColumnType("integer unsigned");

                entity.Property(e => e.CanMasterpiece).HasColumnType("integer unsigned");

                entity.Property(e => e.CraftItemId).HasColumnType("integer unsigned");

                entity.Property(e => e.CraftNum).HasColumnType("integer unsigned");

                entity.Property(e => e.CraftType).HasColumnType("integer unsigned");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.Element).HasColumnType("integer unsigned");

                entity.Property(e => e.IndexDe).HasColumnName("Index_de");

                entity.Property(e => e.IndexEn).HasColumnName("Index_en");

                entity.Property(e => e.IndexFr).HasColumnName("Index_fr");

                entity.Property(e => e.IndexJa).HasColumnName("Index_ja");

                entity.Property(e => e.Level).HasColumnType("integer unsigned");

                entity.Property(e => e.LevelDiff)
                    .HasColumnName("levelDiff")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.LevelView)
                    .HasColumnName("levelView")
                    .HasColumnType("integer unsigned");

                entity.Property(e => e.Meister).HasColumnType("integer unsigned");

                entity.Property(e => e.NeedAutoControl).HasColumnType("integer unsigned");

                entity.Property(e => e.NeedAutoCraftmanship).HasColumnType("integer unsigned");

                entity.Property(e => e.NeedControl).HasColumnType("integer unsigned");

                entity.Property(e => e.NeedCraftmanship).HasColumnType("integer unsigned");

                entity.Property(e => e.NeedEquipItem).HasColumnType("integer unsigned");

                entity.Property(e => e.NeedSecretRecipeBook).HasColumnType("integer unsigned");

                entity.Property(e => e.NeedStatus).HasColumnType("integer unsigned");

                entity.Property(e => e.Number).HasColumnType("integer unsigned");

                entity.Property(e => e.Path).HasColumnName("path");
            });

            modelBuilder.Entity<RecipeElement>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<SecretRecipeBook>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.TextDe)
                    .HasColumnName("Text_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextEn)
                    .HasColumnName("Text_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextFr)
                    .HasColumnName("Text_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.TextJa)
                    .HasColumnName("Text_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.BeastTribe).HasColumnType("integer unsigned");

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.FemaleDe)
                    .HasColumnName("Female_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FemaleEn)
                    .HasColumnName("Female_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FemaleFr)
                    .HasColumnName("Female_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FemaleJa)
                    .HasColumnName("Female_ja")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.FrontDe)
                    .HasColumnName("Front_de")
                    .HasColumnType("boolean");

                entity.Property(e => e.FrontEn)
                    .HasColumnName("Front_en")
                    .HasColumnType("boolean");

                entity.Property(e => e.FrontFr)
                    .HasColumnName("Front_fr")
                    .HasColumnType("boolean");

                entity.Property(e => e.FrontJa)
                    .HasColumnName("Front_ja")
                    .HasColumnType("boolean");

                entity.Property(e => e.MaleDe)
                    .HasColumnName("Male_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MaleEn)
                    .HasColumnName("Male_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MaleFr)
                    .HasColumnName("Male_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.MaleJa)
                    .HasColumnName("Male_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Tomestones>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.Path).HasColumnName("path");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Trait>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasColumnType("integer unsigned")
                    .ValueGeneratedNever();

                entity.Property(e => e.NameDe)
                    .HasColumnName("Name_de")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameEn)
                    .HasColumnName("Name_en")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameFr)
                    .HasColumnName("Name_fr")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NameJa)
                    .HasColumnName("Name_ja")
                    .HasColumnType("varchar(255)");
            });
        }
    }
}
