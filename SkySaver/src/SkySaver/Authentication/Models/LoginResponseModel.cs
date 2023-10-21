using System.IdentityModel.Tokens.Jwt;

namespace SkySaver.Authentication.Models
{
    /// <summary>
    /// Login response model
    /// </summary>
    public class LoginResponseModel
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="token">Security token</param>
        /// <param name="expiration">The date the <see cref="Token"/> expires</param>
        public LoginResponseModel(JwtSecurityToken token = default)
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token);
        }

        public LoginResponseModel(JwtSecurityToken token, Guid userId)
        {
            UserId = userId;
            Token = new JwtSecurityTokenHandler().WriteToken(token);
        }
        /// <summary>
        /// Gets the security token
        /// </summary>
        public string Token { get; }
        public Guid UserId { get; set; }

    }
}
