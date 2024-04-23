using Application.Dtos;
using Application.Services.Validation;
using CleanArchitecture;

namespace Application.UseCases.Users.UpdateUser
{

    public interface IUpdateUserOutputPort : IOutputPort
    {

        #region Methods

        Task PresentCannotRemoveLastSuperAdminAsync(CancellationToken cancellationToken);

        Task PresentInvalidInputAsync(ICollection<ValidationError> validationErrors, CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        Task PresentUpdatedUserAsync(UserDto user, CancellationToken cancellationToken);

        Task PresentUserDoesNotExistAsync(Guid invalidID, CancellationToken cancellationToken);

        #endregion

    }

}
