namespace SkySaver.Domain.Streaks.Features;

using SkySaver.Domain.Streaks.Services;
using SkySaver.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteStreak
{
    public sealed class Command : IRequest
    {
        public readonly Guid Id;

        public Command(Guid id)
        {
            Id = id;
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
            var recordToDelete = await _streakRepository.GetById(request.Id, cancellationToken: cancellationToken);
            _streakRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}