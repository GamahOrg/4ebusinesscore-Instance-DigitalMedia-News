namespace News.Infrastructure.ModelsConfigurations
{
	public class NewsConfig:IEntityTypeConfiguration<NewsModel>
	{
		public void Configure(EntityTypeBuilder<NewsModel> builder)
		{
			builder.HasMany(a => a.NewsRates)
					.WithOne(a => a.News)
					.HasForeignKey(a => a.NewsId);

			builder.HasMany(a => a.NewsAttachments)
					.WithOne(a => a.News)
					.HasForeignKey(a => a.NewsId);

			builder.HasMany(a => a.Views)
					.WithOne(a => a.News)
					.HasForeignKey(a => a.NewsId);

			builder.HasMany(a => a.NewsComments)
					.WithOne(a => a.News)
					.HasForeignKey(a => a.NewsId);

			builder.Property(a => a.IsArchive)
					.HasDefaultValue(false);
			builder.Property(a => a.IsMainNews)
					.HasDefaultValue(false);
		}
	}
}
