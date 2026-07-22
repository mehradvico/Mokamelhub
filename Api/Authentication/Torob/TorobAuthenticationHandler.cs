extern alias TorobBC;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

using TorobEd25519PublicKeyParameters =
    TorobBC::Org.BouncyCastle.Crypto.Parameters.Ed25519PublicKeyParameters;

using TorobEd25519Signer =
    TorobBC::Org.BouncyCastle.Crypto.Signers.Ed25519Signer;

using TorobPublicKeyFactory =
    TorobBC::Org.BouncyCastle.Security.PublicKeyFactory;

namespace Api.Authentication.Torob
{
    public sealed class TorobAuthenticationHandler
        : AuthenticationHandler<TorobAuthenticationOptions>
    {
        private readonly IWebHostEnvironment _environment;

        public TorobAuthenticationHandler(
            IOptionsMonitor<TorobAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IWebHostEnvironment environment
        ) : base(options, logger, encoder)
        {
            _environment = environment;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var tokenVersion = Request
                    .Headers["X-Torob-Token-Version"]
                    .ToString()
                    .Trim();

                if (string.IsNullOrWhiteSpace(tokenVersion))
                {
                    return Task.FromResult(
                        AuthenticateResult.Fail(
                            "torob token version is not provided"
                        )
                    );
                }

                if (!string.Equals(
                        tokenVersion,
                        Options.TokenVersion,
                        StringComparison.Ordinal
                    ))
                {
                    return Task.FromResult(
                        AuthenticateResult.Fail(
                            "invalid torob token version"
                        )
                    );
                }

                var token = Request
                    .Headers["X-Torob-Token"]
                    .ToString()
                    .Trim();

                if (string.IsNullOrWhiteSpace(token))
                {
                    return Task.FromResult(
                        AuthenticateResult.Fail(
                            "torob token is not provided"
                        )
                    );
                }

                if (token.StartsWith(
                        "Bearer ",
                        StringComparison.OrdinalIgnoreCase
                    ))
                {
                    token = token["Bearer ".Length..].Trim();
                }

                var authenticationResult = ValidateTorobToken(token);

                return Task.FromResult(authenticationResult);
            }
            catch (Exception exception)
            {
                Logger.LogWarning(
                    exception,
                    "Torob token validation failed."
                );

                return Task.FromResult(
                    AuthenticateResult.Fail(
                        "invalid torob token"
                    )
                );
            }
        }

        private AuthenticateResult ValidateTorobToken(string token)
        {
            var tokenParts = token.Split('.');

            if (tokenParts.Length != 3)
            {
                return AuthenticateResult.Fail(
                    "invalid torob token format"
                );
            }

            var headerValidationError = ValidateAlgorithm(
                tokenParts[0]
            );

            if (headerValidationError != null)
            {
                return AuthenticateResult.Fail(
                    headerValidationError
                );
            }

            var signatureIsValid = ValidateSignature(
                encodedHeader: tokenParts[0],
                encodedPayload: tokenParts[1],
                encodedSignature: tokenParts[2]
            );

            if (!signatureIsValid)
            {
                return AuthenticateResult.Fail(
                    "invalid torob token signature"
                );
            }

            var tokenHandler = new JwtSecurityTokenHandler
            {
                MapInboundClaims = false
            };

            if (!tokenHandler.CanReadToken(token))
            {
                return AuthenticateResult.Fail(
                    "invalid torob token"
                );
            }

            var jwtToken = tokenHandler.ReadJwtToken(token);

            var lifetimeValidationError = ValidateLifetime(
                jwtToken
            );

            if (lifetimeValidationError != null)
            {
                return AuthenticateResult.Fail(
                    lifetimeValidationError
                );
            }

            var issuerValidationError = ValidateIssuer(
                jwtToken
            );

            if (issuerValidationError != null)
            {
                return AuthenticateResult.Fail(
                    issuerValidationError
                );
            }

            var audienceValidationError = ValidateAudience(
                jwtToken
            );

            if (audienceValidationError != null)
            {
                return AuthenticateResult.Fail(
                    audienceValidationError
                );
            }

            var identity = new ClaimsIdentity(
                claims: jwtToken.Claims,
                authenticationType: Scheme.Name,
                nameType: ClaimTypes.Name,
                roleType: ClaimTypes.Role
            );

            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(
                principal,
                Scheme.Name
            );

            return AuthenticateResult.Success(ticket);
        }

        private string? ValidateAlgorithm(string encodedHeader)
        {
            byte[] headerBytes;

            try
            {
                headerBytes = Base64UrlEncoder.DecodeBytes(
                    encodedHeader
                );
            }
            catch
            {
                return "invalid torob token header";
            }

            using var headerDocument = JsonDocument.Parse(
                headerBytes
            );

            var header = headerDocument.RootElement;

            if (!header.TryGetProperty(
                    "alg",
                    out var algorithmElement
                ))
            {
                return "token algorithm is not provided";
            }

            var algorithm = algorithmElement.GetString();

            var allowedAlgorithms =
                Options.ValidAlgorithms is { Length: > 0 }
                    ? Options.ValidAlgorithms
                    : ["EdDSA"];

            if (string.IsNullOrWhiteSpace(algorithm))
            {
                return "token algorithm is not provided";
            }

            if (!allowedAlgorithms.Contains(
                    algorithm,
                    StringComparer.Ordinal
                ))
            {
                return "invalid torob token algorithm";
            }

            if (!string.Equals(
                    algorithm,
                    "EdDSA",
                    StringComparison.Ordinal
                ))
            {
                return "unsupported torob token algorithm";
            }

            if (!header.TryGetProperty(
                    "v",
                    out var versionElement
                ))
            {
                return "torob token header version is not provided";
            }

            string? headerVersion;

            if (versionElement.ValueKind == JsonValueKind.Number)
            {
                headerVersion = versionElement
                    .GetInt32()
                    .ToString(CultureInfo.InvariantCulture);
            }
            else if (versionElement.ValueKind == JsonValueKind.String)
            {
                headerVersion = versionElement.GetString();
            }
            else
            {
                return "invalid torob token header version";
            }

            if (!string.Equals(
                    headerVersion,
                    Options.TokenVersion,
                    StringComparison.Ordinal
                ))
            {
                return "invalid torob token header version";
            }

            return null;
        }
        private bool ValidateSignature(
            string encodedHeader,
            string encodedPayload,
            string encodedSignature
        )
        {
            byte[] signature;

            try
            {
                signature = Base64UrlEncoder.DecodeBytes(
                    encodedSignature
                );
            }
            catch
            {
                return false;
            }

            var signingInput = Encoding.ASCII.GetBytes(
                $"{encodedHeader}.{encodedPayload}"
            );

            var publicKey = GetEd25519PublicKey();

            var signer = new TorobEd25519Signer();

            signer.Init(
                forSigning: false,
                parameters: publicKey
            );

            signer.BlockUpdate(
                signingInput,
                0,
                signingInput.Length
            );

            return signer.VerifySignature(signature);
        }

        private string? ValidateLifetime(
            JwtSecurityToken jwtToken
        )
        {
            var now = DateTimeOffset.UtcNow;

            var clockSkew = TimeSpan.FromSeconds(
                Math.Max(
                    0,
                    Options.ClockSkewSeconds
                )
            );

            var expirationUnixTime = ReadUnixTimeClaim(
                jwtToken.Payload,
                "exp"
            );

            if (!expirationUnixTime.HasValue)
            {
                return "torob token expiration is not provided";
            }

            DateTimeOffset expiration;

            try
            {
                expiration = DateTimeOffset.FromUnixTimeSeconds(
                    expirationUnixTime.Value
                );
            }
            catch
            {
                return "invalid torob token expiration";
            }

            if (expiration < now.Subtract(clockSkew))
            {
                return "torob token has expired";
            }

            var notBeforeUnixTime = ReadUnixTimeClaim(
                jwtToken.Payload,
                "nbf"
            );

            if (notBeforeUnixTime.HasValue)
            {
                DateTimeOffset notBefore;

                try
                {
                    notBefore = DateTimeOffset.FromUnixTimeSeconds(
                        notBeforeUnixTime.Value
                    );
                }
                catch
                {
                    return "invalid torob token not-before value";
                }

                if (notBefore > now.Add(clockSkew))
                {
                    return "torob token is not active yet";
                }
            }

            return null;
        }

        private string? ValidateIssuer(
            JwtSecurityToken jwtToken
        )
        {
            if (Options.ValidIssuers == null ||
                Options.ValidIssuers.Length == 0)
            {
                return null;
            }

            var issuerIsValid = Options.ValidIssuers.Contains(
                jwtToken.Issuer,
                StringComparer.Ordinal
            );

            return issuerIsValid
                ? null
                : "invalid torob token issuer";
        }

        private string? ValidateAudience(
            JwtSecurityToken jwtToken
        )
        {
            var requestHost = Request.Host.Value?.Trim();

            if (string.IsNullOrWhiteSpace(requestHost))
            {
                return "request host is not available";
            }

            var tokenAudiences = jwtToken.Audiences
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToList();

            if (tokenAudiences.Count == 0)
            {
                return "torob token audience is not provided";
            }

            var matchesRequestHost = tokenAudiences.Any(
                audience => string.Equals(
                    audience,
                    requestHost,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (!matchesRequestHost)
            {
                return "invalid torob token audience";
            }

            if (Options.ValidAudiences is { Length: > 0 })
            {
                var matchesConfiguredAudience = tokenAudiences.Any(
                    audience => Options.ValidAudiences.Contains(
                        audience,
                        StringComparer.OrdinalIgnoreCase
                    )
                );

                if (!matchesConfiguredAudience)
                {
                    return "invalid configured torob token audience";
                }
            }

            return null;
        }

        private static long? ReadUnixTimeClaim(
            JwtPayload payload,
            string claimName
        )
        {
            if (!payload.TryGetValue(
                    claimName,
                    out var rawValue
                ) ||
                rawValue == null)
            {
                return null;
            }

            if (rawValue is long longValue)
            {
                return longValue;
            }

            if (rawValue is int intValue)
            {
                return intValue;
            }

            if (rawValue is JsonElement jsonElement &&
                jsonElement.ValueKind ==
                JsonValueKind.Number &&
                jsonElement.TryGetInt64(
                    out var jsonValue
                ))
            {
                return jsonValue;
            }

            var stringValue = Convert.ToString(
                rawValue,
                CultureInfo.InvariantCulture
            );

            if (long.TryParse(
                    stringValue,
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out var parsedValue
                ))
            {
                return parsedValue;
            }

            return null;
        }

        private TorobEd25519PublicKeyParameters
            GetEd25519PublicKey()
        {
            var publicKeyPem = GetPublicKeyPem();

            var base64Value = publicKeyPem
                .Replace(
                    "-----BEGIN PUBLIC KEY-----",
                    string.Empty,
                    StringComparison.Ordinal
                )
                .Replace(
                    "-----END PUBLIC KEY-----",
                    string.Empty,
                    StringComparison.Ordinal
                )
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty)
                .Replace(" ", string.Empty)
                .Trim();

            if (string.IsNullOrWhiteSpace(base64Value))
            {
                throw new InvalidOperationException(
                    "Torob public key content is empty."
                );
            }

            byte[] derBytes;

            try
            {
                derBytes = Convert.FromBase64String(
                    base64Value
                );
            }
            catch (FormatException exception)
            {
                throw new InvalidOperationException(
                    "Torob public key is not a valid PEM public key.",
                    exception
                );
            }

            var key = TorobPublicKeyFactory.CreateKey(
                derBytes
            ) as TorobEd25519PublicKeyParameters;

            if (key == null)
            {
                throw new InvalidOperationException(
                    "Torob public key is not an Ed25519 public key."
                );
            }

            return key;
        }

        private string GetPublicKeyPem()
        {
            if (!string.IsNullOrWhiteSpace(
                    Options.PublicKeyPem
                ))
            {
                return Options.PublicKeyPem
                    .TrimStart('\uFEFF')
                    .Trim();
            }

            if (string.IsNullOrWhiteSpace(
                    Options.PublicKeyPath
                ))
            {
                throw new InvalidOperationException(
                    "Torob public key is not configured."
                );
            }

            var publicKeyPath = Options.PublicKeyPath;

            if (!Path.IsPathRooted(publicKeyPath))
            {
                publicKeyPath = Path.Combine(
                    _environment.ContentRootPath,
                    publicKeyPath
                );
            }

            if (!File.Exists(publicKeyPath))
            {
                throw new FileNotFoundException(
                    "Torob public key file was not found.",
                    publicKeyPath
                );
            }

            return File.ReadAllText(publicKeyPath)
                .TrimStart('\uFEFF')
                .Trim();
        }

        protected override async Task HandleChallengeAsync(
            AuthenticationProperties properties
        )
        {
            if (Response.HasStarted)
            {
                return;
            }

            Response.StatusCode =
                StatusCodes.Status401Unauthorized;

            Response.ContentType =
                "application/json; charset=utf-8";

            var responseBody = JsonSerializer.Serialize(
                new
                {
                    error = "unauthorized"
                }
            );

            await Response.WriteAsync(responseBody);
        }
    }
}
