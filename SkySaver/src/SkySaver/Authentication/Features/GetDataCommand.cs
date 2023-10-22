using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using SkySaver.Authentication.Models;
using SkySaver.Authentication.Services;
using SkySaver.Domain.Roles;
using SkySaver.Domain.Users;
using SkySaver.Domain.Users.Services;
using SkySaver.Domain.Streaks.Services;
using SkySaver.Domain.Flights.Services;
using SkySaver.Domain.Flights.Mappings;
using SkySaver.Domain.PurchasableGoods.Services;
using SkySaver.Domain.PurchasableGoods.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace SkySaver.Authentication.Features
{
    /// <summary>
    /// User login command.
    /// </summary>
    public class UserLoginCommand : IRequest<LoginResponseModel>
    {
        public readonly UserLoginModel userLoginModel;
        public UserLoginCommand(UserLoginModel userLoginModel)
        {
            this.userLoginModel = userLoginModel;
        }
    }

    /// <summary>
    /// Command handler
    /// </summary>
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, LoginResponseModel>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<UserLoginCommandHandler> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IFlightRepository flightRepository;
        private readonly IStreakRepository _streakRepository;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPurchasableGoodRepository _purchasableGoodRepository;
        private readonly IUserRepository _userRepository;

        public UserLoginCommandHandler(IAuthenticationService authenticationService,
            ILogger<UserLoginCommandHandler> logger, IUserRepository userRepository, UserManager<User> userManager, RoleManager<Role> roleManager, IStreakRepository streakRepository, IFlightRepository flightRepository, IPurchasableGoodRepository purchasableGoodRepository)
        {
            _authenticationService = authenticationService;
            _logger = logger;
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;

            _streakRepository = streakRepository;
            this.flightRepository = flightRepository;
            _purchasableGoodRepository = purchasableGoodRepository;
        }

        public async Task<LoginResponseModel> Handle(UserLoginCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.Query().Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Email == command.userLoginModel.Email, cancellationToken: cancellationToken);
                if (user is null)
                    throw new NotFoundException();

                var passwordCheckResult = await _userManager.CheckPasswordAsync(user, command.userLoginModel.Password);

                if (!passwordCheckResult)
                    throw new NotFoundException("Invalid username or password.");
                var streaks = await _streakRepository.Query().FirstOrDefaultAsync(s => s.UserID == user.Id);
                IQueryable<Domain.Flights.Flight> flights1 = flightRepository.Query().Where(c => c.UserID == user.Id);
                List<Domain.Flights.Dtos.FlightDto> flights = flights1.ToFlightDtoQueryable().ToList();
                IQueryable<Domain.PurchasableGoods.PurchasableGood> purchasableGoods = _purchasableGoodRepository.Query().Where(u => u.PointsCost < 10000);
                var unlockedProducts = purchasableGoods.ToPurchasableGoodDtoQueryable().ToList();
                IQueryable<Domain.PurchasableGoods.PurchasableGood> locked = _purchasableGoodRepository.Query().Where(u => u.PointsCost >= 10000);
                List<Domain.PurchasableGoods.Dtos.PurchasableGoodDto> lockedProducts = locked.ToPurchasableGoodDtoQueryable().ToList();
                var token = await _authenticationService.AuthenticateAsync(user, cancellationToken);
                return new LoginResponseModel(token, user.Id, streaks?.StreakLevel ?? "None", user.SkyPoints, flights, user.FullName, unlockedProducts, lockedProducts);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while logging in.");
                throw;
            }
        }

    }
}