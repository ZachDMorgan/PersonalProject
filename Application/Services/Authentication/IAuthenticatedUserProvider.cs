using Domain.Entities;

namespace Application.Services.Authentication
{

    public interface IAuthenticatedUserProvider
    {

        #region Methods

        User GetCurrentlyAuthenticatedUser();

        #endregion

    }

}
