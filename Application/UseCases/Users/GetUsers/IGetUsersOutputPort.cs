using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Users.GetUsers
{

    public interface IGetUsersOutputPort : IOutputPort
    {

        #region Methods

        Task PresentUnauthenticatedAsync(CancellationToken cancellationToken);

        Task PresentUsersAsync(IQueryable<UserDto> users, CancellationToken cancellationToken);

        #endregion

    }

}
