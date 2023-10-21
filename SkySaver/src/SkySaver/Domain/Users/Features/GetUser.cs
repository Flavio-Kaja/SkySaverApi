namespace SkySaver.Domain.Users.Features;

using SkySaver.Domain.Users.Dtos;
using SkySaver.Domain.Users.Services;
using SharedKernel.Exceptions;
using Mappings;
using MediatR;

public static class GetUser
{
    public sealed class Query : IRequest<UserDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return result.ToUserDto();
        }
    }
}