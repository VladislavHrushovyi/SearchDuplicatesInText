using SearchDuplicatesText.DataRepositories.InMemoryContext.Repositories.ProgressRepository;
using SearchDuplicatesText.DataRepositories.PostgreSqlContext.Repositories;
using SearchDuplicatesText.Services.FileService;
using SearchDuplicatesText.Services.MakeDataForMethodsService;
using SearchDuplicatesText.Services.PlagiarismCheckService;
using SearchDuplicatesText.Services.PlagiarismCheckService.ExpMethod;
using SearchDuplicatesText.Services.PlagiarismCheckService.NgramMethod;
using SearchDuplicatesText.Services.PlagiarismCheckService.ShingleMethod;

namespace SearchDuplicatesText.ServiceProviders;

public static class ServiceProviderExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<UploadFileService>();
        services.AddScoped<TextClearing>();
        services.AddScoped<WordNormalizer>();
        services.AddScoped<ConvertTextToDataMethod>();
        services.AddScoped<FileRepository>();
        services.AddScoped<FileHandler>();
        services.AddScoped<PlagiarismCheckService>();
        services.AddScoped<ShingleMethod>();
        services.AddScoped<NgramMethod>();
        services.AddScoped<ExpMethod>();
        services.AddScoped<IProgressRepository, ProgressRepository>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}