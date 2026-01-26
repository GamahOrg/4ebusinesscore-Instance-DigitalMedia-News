namespace News.API.Extension
{
    public static class ValidationExtension
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            // Add Validation 
            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PostNewsCommand>());
        }
    }
}
