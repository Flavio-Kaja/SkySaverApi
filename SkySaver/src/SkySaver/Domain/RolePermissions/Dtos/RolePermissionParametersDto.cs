namespace SkySaver.Domain.RolePermissions.Dtos;

using FluentValidation;

public sealed class RolePermissionParametersDto
{
    public string Role { get; set; }
}

public class RolePermissionParametersValidator : AbstractValidator<RolePermissionParametersDto>
{
    public RolePermissionParametersValidator()
    {
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .Length(2, 50).WithMessage("Role must be between 2 and 50 characters.");
    }
}
