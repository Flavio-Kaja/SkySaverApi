namespace SkySaver.Domain.Flights.Features;

using SkySaver.Domain.Flights.Services;
using SkySaver.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteFlight
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
        private readonly IFlightRepository _flightRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IFlightRepository flightRepository, IUnitOfWork unitOfWork)
        {
            _flightRepository = flightRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _flightRepository.GetById(request.Id, cancellationToken: cancellationToken);
            _flightRepository.Remove(recordToDelete);
            await _unitOfWork.CommitChanges(cancellationToken);
        }
    }
}