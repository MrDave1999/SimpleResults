namespace SimpleResults.Example.NativeAOT;

[JsonSourceGenerationOptions(
    WriteIndented = true, 
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(ListedResult<User>))]
public partial class CustomContext : JsonSerializerContext { }
