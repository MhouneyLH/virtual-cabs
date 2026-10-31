using GleisGrain.Common.Abstractions;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace GleisGrain.Common.Ids;

/// <summary>Documents strongly typed ids as plain strings (uuid for Guid-based ids) instead of objects with a Value property.</summary>
internal sealed class StronglyTypedIdSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        var idInterface = context.JsonTypeInfo.Type
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IStronglyTypedId<,>));

        if (idInterface is null)
        {
            return Task.CompletedTask;
        }

        schema.Type = JsonSchemaType.String;
        schema.Format = idInterface.GenericTypeArguments[1] == typeof(Guid) ? "uuid" : null;
        schema.Properties?.Clear();
        schema.Required?.Clear();

        return Task.CompletedTask;
    }
}
