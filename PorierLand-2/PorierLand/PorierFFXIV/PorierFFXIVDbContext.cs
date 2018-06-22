using Microsoft.EntityFrameworkCore;

namespace FFXIVTranslator.PorierFFXIV
{
    public class PorierFFXIVDbContext : DbContext
    {
        public DbSet<BaseParam> BaseParams { get; set; }
        public DbSet<BeastTribe> BeastTribes { get; set; }
        public DbSet<ClassJob> ClassJobs { get; set; }
        public DbSet<ClassJobCategory> ClassJobCategories { get; set; }
        public DbSet<ClassJobClassJobCategory> ClassJobClassJobCategories { get; set; }
        public DbSet<Crystal> Crystals { get; set; }
        public DbSet<ENpc> ENpcs { get; set; }
        public DbSet<ENpcPlaceName> ENpcPlaceNames { get; set; }
        public DbSet<ENpcShop> ENpcShops { get; set; }
        public DbSet<Gathering> Gatherings { get; set; }
        public DbSet<GatheringPoint> GatheringPoints { get; set; }
        public DbSet<GatheringSubCategory> GatheringSubCategories { get; set; }
        public DbSet<GatheringType> GatheringTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemBonus> ItemBonuses { get; set; }
        public DbSet<ItemAction> ItemActions { get; set; }
        public DbSet<ItemClassJob> ItemClassJobs { get; set; }
        public DbSet<ItemENpc> ItemENpcs { get; set; }
        public DbSet<ItemShop> ItemShops { get; set; }
        public DbSet<ItemUICategory> ItemUICategories { get; set; }
        public DbSet<ItemUIKind> ItemUIKinds { get; set; }
        public DbSet<PlaceName> PlaceNames { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeElement> RecipeElements { get; set; }
        public DbSet<RecipeItem> RecipeItems { get; set; }
        public DbSet<SecretRecipeBook> SecretRecipeBooks { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<BisSet> BisSets { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=PorierFFXIV;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasOne(recipe => recipe.CraftItem)
                .WithMany(item => item.CraftRecipes);

            modelBuilder.Entity<Recipe>()
                .HasOne(recipe => recipe.NeedEquipItem)
                .WithMany(item => item.NeedEquipRecipes);
        }
    }
}