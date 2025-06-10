using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ClientesAPI.API.Configuration
{
    public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    { 
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateOptenApiInfo(description));
            }
        }

        private static OpenApiInfo CreateOptenApiInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Web.API",
                Version = description.ApiVersion.ToString(),

            };

            if (description.IsDeprecated)
            {
                info.Description += " (deprecated)";
            }

            return info;
        }
    }
}
