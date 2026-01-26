namespace News.Infrastructure.Context
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lk_NewsStatus>()
               .Property(e => e.Id)
               .UseIdentityColumn(seed: 0, increment: 1);
        }
        public DbSet<News.Domain.Models.News> News { get; set; }
        public DbSet<NewsAttachments> NewsAttachments { get; set; }
        public DbSet<NewsComment> NewsComments { get; set; }
        public DbSet<NewsRate> RateNews { get; set; }
        public DbSet<NewsView> Views { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Lk_NewsStatus> lk_NewsStatuses { get; set; }

        public bool AllMigrationsApplied()
        {
            var applied = this.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = this.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        public void Migrate()
        {
            this.Database.Migrate();
        }
    }
}