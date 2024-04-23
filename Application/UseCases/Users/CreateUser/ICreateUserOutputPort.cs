using Application.Dtos;
using Application.Services.Validation;
using CleanArchitecture;

namespace Application.UseCases.Users.CreateUser
{

    public interface ICreateUserOutputPort : IOutputPort
    {

        #region Methods

        Task PresentInvalidInputAsync(ICollection<ValidationError> validationErrors, CancellationToken cancellationToken);

        Task PresentUserAsync(UserDto user, CancellationToken cancellationToken);

        Task PresentUsernameNotUniqueAsync(CancellationToken cancellationToken);

        Task PresentPractitionerDoesNotExistAsync(Guid invalidID, CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
