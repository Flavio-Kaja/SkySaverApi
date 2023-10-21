namespace SkySaver.Domain.ScavengerHunts.Features;

using SkySaver.Domain.ScavengerHunts.Services;
using SkySaver.Domain.ScavengerHunts;
using SkySaver.Domain.ScavengerHunts.Dtos;
using SkySaver.Domain.ScavengerHunts.Models;
using SkySaver.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class AddScavengerHunt
{
    public sealed class Command : IRequest<ScavengerHuntDto>
    {
        public readonly ScavengerHuntForCreationDto ScavengerHuntToAdd;

        public Command(ScavengerHuntForCreationDto scavengerHuntToAdd)
        {
            ScavengerHuntToAdd = scavengerHuntToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, ScavengerHuntDto>
    {
        private readonly IScavengerHuntRepository _scavengerHuntRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IScavengerHuntRepository scavengerHuntRepository, IUnitOfWork unitOfWork)
        {
            _scavengerHuntRepository = scavengerHuntRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ScavengerHuntDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var scavengerHuntToAdd = request.ScavengerHuntToAdd.ToScavengerHuntForCreation();
            var scavengerHunt = ScavengerHunt.Create(scavengerHuntToAdd);

            await _scavengerHuntRepository.Add(scavengerHunt, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return scavengerHunt.ToScavengerHuntDto();
        }
    }
}