using Domain.Enumerations;

namespace Application.Services.Authorisation
{

    public interface IAuthorisationClaimsProvider
    {

        #region Methods

        Task<bool> DoesUserHaveClaimAsync(AuthorisationClaim targetClaim);

        Task<bool> DoesUserHaveClaimsAsync(IEnumerable<AuthorisationClaim> targetClaims);

        #endregion

    }

}
