using News.Domain.Models;
using System.Text;

namespace News.Infrastructure.Context
{
    public class ContextSeed
    {
        private static DBContext _dbContext;
        private static IServiceProvider _serviceProvider;

        public static void Seed(DBContext appDbContext, IServiceProvider serviceProvider)
        {
            _dbContext = appDbContext;
            _dbContext.Database.EnsureCreated();
            _serviceProvider = serviceProvider;
            SeedLk_NewsStatus();
            SeedSectors();
            SeedAuthors();
            SeedNews();
        }

        private static void SeedNews()
        {
            if (!_dbContext.News.Any())
            {
                string SeedingNews = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "News.json", Encoding.UTF8);
                var AllNews = JsonConvert.DeserializeObject<List<NewsModel>>(SeedingNews);

                if (AllNews != null)
                {
                    foreach (var item in AllNews)
                    {
                        item.CreatedOn = DateTime.Now;
                        item.UpdatedOn = DateTime.Now;
                        item.CreatedBy = Guid.NewGuid();
                        item.UpdatedBy = null;
                        _dbContext.News.Add(item);
                        _dbContext.SaveChanges();
                    }
                }
            }
        }
        private static void SeedSectors()
        {
            if (!_dbContext.Sectors.Any())
            {
                string SeedingSectors = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "Sectors.json", Encoding.UTF8);
                var AllSectors = JsonConvert.DeserializeObject<List<Sector>>(SeedingSectors);

                if (AllSectors != null)
                {
                    foreach (var item in AllSectors)
                    {
                        item.CreatedOn = DateTime.Now;
                        item.UpdatedOn = DateTime.Now;
                        item.CreatedBy = Guid.NewGuid();
                        item.UpdatedBy = null;
                        _dbContext.Sectors.Add(item);
                        _dbContext.SaveChanges();
                    }
                }
            }
        }
        private static void SeedAuthors()
        {
            if (!_dbContext.Authors.Any())
            {
                string SeedingAuthors = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "Author.json", Encoding.UTF8);
                var AllAuthors = JsonConvert.DeserializeObject<List<Author>>(SeedingAuthors);

                if (AllAuthors != null)
                {
                    foreach (var item in AllAuthors)
                    {
                        item.CreatedOn = DateTime.Now;
                        item.UpdatedOn = DateTime.Now;
                        item.CreatedBy = Guid.NewGuid();
                        item.UpdatedBy = null;
                        _dbContext.Authors.Add(item);
                        _dbContext.SaveChanges();
                    }
                }
            }
        }

        private static void SeedLk_NewsStatus()
        {
            string Lk_NewsStatusJson = File.ReadAllText(@"SeedData" + Path.DirectorySeparatorChar + "LK_NewsStatus.json", Encoding.UTF8);
            var lk_NewsStatuses = JsonConvert.DeserializeObject<List<Lk_NewsStatus>>(Lk_NewsStatusJson);

            if (lk_NewsStatuses != null && lk_NewsStatuses.Any())
            {
                _dbContext.Database.OpenConnection();
                try
                {
                    if (!_dbContext.lk_NewsStatuses.Any())
                    {
                        foreach (var lk_NewsStatuse in lk_NewsStatuses)
                        {
                            lk_NewsStatuse.CreatedOn = DateTime.Now;
                        }

                        _dbContext.lk_NewsStatuses.AddRange(lk_NewsStatuses);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        var existingNewsStatuses = _dbContext.lk_NewsStatuses.ToList();
                        var newlk_NewsStatuses = lk_NewsStatuses
                            .Where(ns => !existingNewsStatuses.Any(ep => ep.NameEn == ns.NameEn))
                            .ToList();

                        if (newlk_NewsStatuses.Any())
                        {
                            foreach (var SocialMedia in newlk_NewsStatuses)
                            {
                                SocialMedia.CreatedOn = DateTime.Now;
                            }

                            _dbContext.lk_NewsStatuses.AddRange(newlk_NewsStatuses);
                            _dbContext.SaveChanges();
                        }
                    }
                }
                finally
                {
                    _dbContext.Database.CloseConnection();
                }
            }

        }

    }
}
