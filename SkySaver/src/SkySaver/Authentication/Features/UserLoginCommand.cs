﻿using MediatR;
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
    public class GetDataCommand : IRequest<GetDataResponseModel>
    {
        public readonly Guid UserId;
        public GetDataCommand(Guid userId)
        {
            this.UserId = userId;
        }
    }

    /// <summary>
    /// Command handler
    /// </summary>
    public class GetDataCommandHandler : IRequestHandler<GetDataCommand, GetDataResponseModel>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<GetDataCommandHandler> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IFlightRepository flightRepository;
        private readonly IStreakRepository _streakRepository;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPurchasableGoodRepository _purchasableGoodRepository;
        private readonly IUserRepository _userRepository;

        public GetDataCommandHandler(IAuthenticationService authenticationService,
            ILogger<GetDataCommandHandler> logger, IUserRepository userRepository, UserManager<User> userManager, RoleManager<Role> roleManager, IStreakRepository streakRepository, IFlightRepository flightRepository, IPurchasableGoodRepository purchasableGoodRepository)
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

        public async Task<GetDataResponseModel> Handle(GetDataCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.Query().Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken: cancellationToken);
                if (user is null)
                    throw new NotFoundException();

                var streaks = await _streakRepository.Query().FirstOrDefaultAsync(s => s.UserID == user.Id);
                IQueryable<Domain.Flights.Flight> flights1 = flightRepository.Query().Where(c => c.UserID == user.Id);
                List<Domain.Flights.Dtos.FlightDto> flights = flights1.ToFlightDtoQueryable().ToList();
                IQueryable<Domain.PurchasableGoods.PurchasableGood> purchasableGoods = _purchasableGoodRepository.Query().Where(u => u.PointsCost < 10000);
                var unlockedProducts = purchasableGoods.ToPurchasableGoodDtoQueryable().ToList();
                IQueryable<Domain.PurchasableGoods.PurchasableGood> locked = _purchasableGoodRepository.Query().Where(u => u.PointsCost >= 10000);
                List<Domain.PurchasableGoods.Dtos.PurchasableGoodDto> lockedProducts = locked.ToPurchasableGoodDtoQueryable().ToList();
                var token = await _authenticationService.AuthenticateAsync(user, cancellationToken);
                return new GetDataResponseModel(user.Id, streaks?.StreakLevel ?? "None", user.SkyPoints, flights, user.FullName, unlockedProducts, lockedProducts);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while logging in.");
                throw;
            }
        }

    }
    public class GetDataResponseModel
    {
        public GetDataResponseModel(Guid userId, string streak, int skyPoints, List<Domain.Flights.Dtos.FlightDto> flights, string name,
        List<Domain.PurchasableGoods.Dtos.PurchasableGoodDto> unlockedGoods, List<Domain.PurchasableGoods.Dtos.PurchasableGoodDto> lockedGoods)
        {
            UserId = userId;
            Streak = streak;
            this.SkyPoints = skyPoints;
            UnlockedGoods = unlockedGoods;
            LockedGoods = lockedGoods;
            this.Flights = flights;
        }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int SkyPoints { get; set; }
        public string Streak { get; set; }
        public List<Domain.Flights.Dtos.FlightDto> Flights { get; set; }
        public List<Domain.PurchasableGoods.Dtos.PurchasableGoodDto> UnlockedGoods { get; set; }
        public List<Domain.PurchasableGoods.Dtos.PurchasableGoodDto> LockedGoods { get; set; }
    }
}