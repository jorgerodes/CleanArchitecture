using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitecture.Api.Documentation;

public sealed class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var documentation in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(documentation.GroupName, CreateDocummentation(documentation));
        }
    }

    private static OpenApiInfo CreateDocummentation(ApiVersionDescription apiVersionDescription) {
        var info = new OpenApiInfo
        {
            Title = $"Clean Architecture API v{apiVersionDescription.ApiVersion}",
            Version = apiVersionDescription.ApiVersion.ToString()
        };

        if (apiVersionDescription.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
           
    }
}