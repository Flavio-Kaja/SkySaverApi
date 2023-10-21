namespace SkySaver.Domain.Streaks.Features;

using SkySaver.Domain.Streaks.Services;
using SkySaver.Domain.Streaks;
using SkySaver.Domain.Streaks.Dtos;
using SkySaver.Domain.Streaks.Models;
using SkySaver.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class AddStreak
{
    public sealed class Command : IRequest<StreakDto>
    {
        public readonly StreakForCreationDto StreakToAdd;

        public Command(StreakForCreationDto streakToAdd)
        {
            StreakToAdd = streakToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, StreakDto>
    {
        private readonly IStreakRepository _streakRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IStreakRepository streakRepository, IUnitOfWork unitOfWork)
        {
            _streakRepository = streakRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StreakDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var streakToAdd = request.StreakToAdd.ToStreakForCreation();
            var streak = Streak.Create(streakToAdd);

            await _streakRepository.Add(streak, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return streak.ToStreakDto();
        }
    }
}