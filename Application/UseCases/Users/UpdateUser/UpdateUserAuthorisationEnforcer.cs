using Application.Services;
using Application.Services.Authentication;
using Application.Services.Authorisation;
using CleanArchitecture;
using Domain.Enumerations;

namespace Application.UseCases.Users.UpdateUser
{

    public class UpdateUserAuthorisationEnforcer : IAuthorisationEnforcer<UpdateUserInputPort, IUpdateUserOutputPort>
    {
        #region Fields

        private readonly IAuthenticatedUserProvider _authenticatedUserProvider;
        private readonly IAuthorisationClaimsProvider _authorisationClaimsProvider;

        #endregion

        #region Constructors

        public UpdateUserAuthorisationEnforcer(IAuthenticatedUserProvider authenticatedUserProvider, IAuthorisationClaimsProvider authorisationClaimsProvider)
        {
            this._authenticatedUserProvider = authenticatedUserProvider ?? throw new ArgumentNullException(nameof(authenticatedUserProvider));
            this._authorisationClaimsProvider = authorisationClaimsProvider ?? throw new ArgumentNullException(nameof(authorisationClaimsProvider));
        }

        #endregion

        #region Methods

        async Task<IContinuationResult> IAuthorisationEnforcer<UpdateUserInputPort, IUpdateUserOutputPort>.AuthoriseAsync(
            UpdateUserInputPort inputPort,
            IUpdateUserOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            var _CurrentUser = this._authenticatedUserProvider.GetCurrentlyAuthenticatedUser();

            if (inputPort.Password.HasBeenSet && _CurrentUser.ID != inputPort.UserID)
            {
                await outputPort.PresentUnauthorisedAsync("Passwords can only be changed from User's account.", cancellationToken);
                return new ContinuationResult(ContinuationResultBehavior.Bail);
            }
            if (inputPort.Person != null && _CurrentUser.ID != inputPort.UserID && !await this._authorisationClaimsProvider.DoesUserHaveClaimAsync(AuthorisationClaim.CanAlterOtherUsers))
            {
                await outputPort.PresentUnauthorisedAsync("Passwords can only be changed from User's account or by a Super Admin.", cancellationToken);
                return new ContinuationResult(ContinuationResultBehavior.Bail);
            }

            if (inputPort.Role.HasBeenSet && !await this._authorisationClaimsProvider.DoesUserHaveClaimAsync(AuthorisationClaim.CanAlterUserRoles))
            {
                await outputPort.PresentUnauthorisedAsync(GenericErrorMessageProvider.GetUnauthorisedMessage(AuthorisationClaim.CanAlterUserRoles), cancellationToken);
                return new ContinuationResult(ContinuationResultBehavior.Bail);

            }

            return new ContinuationResult();
        }

        #endregion

    }

}
