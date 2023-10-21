namespace SkySaver.Domain.ScavengerHunts.Features;

using SkySaver.Domain.ScavengerHunts.Dtos;
using SkySaver.Domain.ScavengerHunts.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class GetScavengerHunt
{
    public sealed class Query : IRequest<ScavengerHuntDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, ScavengerHuntDto>
    {
        private readonly IScavengerHuntRepository _scavengerHuntRepository;

        public Handler(IScavengerHuntRepository scavengerHuntRepository)
        {
            _scavengerHuntRepository = scavengerHuntRepository;
        }

        public async Task<ScavengerHuntDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _scavengerHuntRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return result.ToScavengerHuntDto();
        }
    }
}