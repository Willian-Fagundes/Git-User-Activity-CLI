using System.Text.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace WebApi;
public record class Repository(string Name, string Description,
    [property: JsonPropertyName("html_url")] Uri GitHubHomeUrl,
    Uri Homepage, int Watchers, [property: JsonPropertyName("pushed_at")] DateTime LastPushUtc)
    {
        public DateTime LastPush => LastPushUtc.ToLocalTime();
    }
