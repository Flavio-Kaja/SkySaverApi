namespace SkySaver.Domain.Streaks.Features;

using SkySaver.Domain.Streaks.Dtos;
using SkySaver.Domain.Streaks.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class GetStreak
{
    public sealed class Query : IRequest<StreakDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, StreakDto>
    {
        private readonly IStreakRepository _streakRepository;

        public Handler(IStreakRepository streakRepository)
        {
            _streakRepository = streakRepository;
        }

        public async Task<StreakDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _streakRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return result.ToStreakDto();
        }
    }
}