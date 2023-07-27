using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OmmelSamvirke.API.IntegrationTests.Common;

public static class SerializerSettings
{
    public static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };
}
