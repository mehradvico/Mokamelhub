using Microsoft.AspNetCore.Authentication;

namespace Api.Authentication.Torob
{
    public sealed class TorobAuthenticationOptions
        : AuthenticationSchemeOptions
    {
        public string PublicKeyPath { get; set; }

        public string PublicKeyPem { get; set; }

        public string TokenVersion { get; set; } = "1";

        public string[] ValidIssuers { get; set; } = [];

        public string[] ValidAudiences { get; set; } = [];

        public string[] ValidAlgorithms { get; set; } =
        [
            "EdDSA"
        ];

        public int ClockSkewSeconds { get; set; } = 60;
    }
}