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

        public LoginResponseModel(JwtSecurityToken token, Guid userId, string streak, int skyPoints, List<Domain.Flights.Dtos.FlightDto> flights, string name,
        List<Domain.PurchasableGoods.Dtos.PurchasableGoodDto> unlockedGoods, List<Domain.PurchasableGoods.Dtos.PurchasableGoodDto> lockedGoods)
        {
            UserId = userId;
            Streak = streak;
            Name = name;
            this.SkyPoints = skyPoints;
            UnlockedGoods = unlockedGoods;
            LockedGoods = lockedGoods;
            this.Flights = flights;
            Token = new JwtSecurityTokenHandler().WriteToken(token);
        }
        /// <summary>
        /// Gets the security token
        /// </summary>
        public string Token { get; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int SkyPoints { get; set; }
        public string Streak { get; set; }
        public List<Domain.Flights.Dtos.FlightDto> Flights { get; set; }
        public List<Domain.PurchasableGoods.Dtos.PurchasableGoodDto> UnlockedGoods { get; set; }
        public List<Domain.PurchasableGoods.Dtos.PurchasableGoodDto> LockedGoods { get; set; }
    }
}
