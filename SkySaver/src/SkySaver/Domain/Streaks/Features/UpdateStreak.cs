namespace SkySaver.Domain.Streaks.Features;

using SkySaver.Domain.Streaks;
using SkySaver.Domain.Streaks.Dtos;
using SkySaver.Domain.Streaks.Services;
using SkySaver.Services;
using SkySaver.Domain.Streaks.Models;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class UpdateStreak
{
    public sealed class Command : IRequest
    {
        public readonly Guid Id;
        public readonly StreakForUpdateDto UpdatedStreakData;

        public Command(Guid id, StreakForUpdateDto updatedStreakData)
        {
            Id = id;
            UpdatedStreakData = updatedStreakData;
        }
    }

    public sealed class Handler : IRequestHandler<Command>
    {
        private readonly IStreakRepository _streakRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IStreakRepository streakRepository, IUnitOfWork unitOfWork)
        {
            _streakRepository = streakRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var streakToUpdate = await _streakRepository.GetById(request.Id, cancellationToken: cancellationToken);
            var streakToAdd = request.UpdatedStreakData.ToStreakForUpdate();
            streakToUpdate.Update(streakToAdd);

            _streakRepository.Update(streakToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}