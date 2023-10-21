namespace SkySaver.Domain.ScavengerHunts.Features;

using SkySaver.Domain.ScavengerHunts;
using SkySaver.Domain.ScavengerHunts.Dtos;
using SkySaver.Domain.ScavengerHunts.Services;
using SkySaver.Services;
using SkySaver.Domain.ScavengerHunts.Models;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class UpdateScavengerHunt
{
    public sealed class Command : IRequest
    {
        public readonly Guid Id;
        public readonly ScavengerHuntForUpdateDto UpdatedScavengerHuntData;

        public Command(Guid id, ScavengerHuntForUpdateDto updatedScavengerHuntData)
        {
            Id = id;
            UpdatedScavengerHuntData = updatedScavengerHuntData;
        }
    }

    public sealed class Handler : IRequestHandler<Command>
    {
        private readonly IScavengerHuntRepository _scavengerHuntRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IScavengerHuntRepository scavengerHuntRepository, IUnitOfWork unitOfWork)
        {
            _scavengerHuntRepository = scavengerHuntRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var scavengerHuntToUpdate = await _scavengerHuntRepository.GetById(request.Id, cancellationToken: cancellationToken);
            var scavengerHuntToAdd = request.UpdatedScavengerHuntData.ToScavengerHuntForUpdate();
            scavengerHuntToUpdate.Update(scavengerHuntToAdd);

            _scavengerHuntRepository.Update(scavengerHuntToUpdate);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}