namespace BaGet.Core.Configuration
{
    public sealed class NugetAuthenticationOptions
    {
        public NugetCredentials[] Credentials { get; set; }

        public ApiKey[] ApiKeys { get; set; }
    }
}
