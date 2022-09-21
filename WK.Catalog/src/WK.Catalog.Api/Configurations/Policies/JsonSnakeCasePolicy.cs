using WK.Catalog.Api.Extensions.String;
using System.Text.Json;

namespace WK.Catalog.Api.Configurations.Policies;

public class JsonSnakeCasePolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
        => name.ToSnakeCase();
}
