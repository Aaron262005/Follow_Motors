namespace FollowMotors.Infrastructure.Security;

public class JwtSettings
{
    public const string SeccionConfiguracion = "Jwt";

    public string ClaveSecreta { get; set; } = string.Empty;
    public string Emisor { get; set; } = "FollowMotors.WebApi";
    public string Audiencia { get; set; } = "FollowMotors.Frontend";
    public int MinutosExpiracion { get; set; } = 120;
}
