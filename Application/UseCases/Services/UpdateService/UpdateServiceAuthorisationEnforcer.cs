using Application.Services;
using Application.Services.Authorisation;
using CleanArchitecture;
using Domain.Enumerations;

namespace Application.UseCases.Services.UpdateService
{

    public class UpdateServiceAuthorisationEnforcer : IAuthorisationEnforcer<UpdateServiceInputPort, IUpdateServiceOutputPort>
    {

        #region Fields

        private readonly IAuthorisationClaimsProvider _authorisationClaimsProvider;

        #endregion

        #region Constructors

        public UpdateServiceAuthorisationEnforcer(IAuthorisationClaimsProvider authorisationClaimsProvider)
            => this._authorisationClaimsProvider = authorisationClaimsProvider ?? throw new ArgumentNullException(nameof(authorisationClaimsProvider));

        #endregion

        #region Methods

        async Task<IContinuationResult> IAuthorisationEnforcer<UpdateServiceInputPort, IUpdateServiceOutputPort>.AuthoriseAsync(
            UpdateServiceInputPort inputPort,
            IUpdateServiceOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            if (await this._authorisationClaimsProvider.DoesUserHaveClaimAsync(AuthorisationClaim.CanAlterServices))
                return new ContinuationResult();
            await outputPort.PresentUnauthorisedAsync(GenericErrorMessageProvider.GetUnauthorisedMessage(AuthorisationClaim.CanAlterServices), cancellationToken);
            return new ContinuationResult(ContinuationResultBehavior.Bail);
        }

        #endregion

    }

}
