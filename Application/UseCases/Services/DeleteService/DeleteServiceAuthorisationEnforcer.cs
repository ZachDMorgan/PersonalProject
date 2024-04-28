using Application.Services;
using Application.Services.Authorisation;
using CleanArchitecture;
using Domain.Enumerations;

namespace Application.UseCases.Services.DeleteService
{

    public class DeleteServiceAuthorisationEnforcer : IAuthorisationEnforcer<DeleteServiceInputPort, IDeleteServiceOutputPort>
    {

        #region Fields

        private readonly IAuthorisationClaimsProvider _authorisationClaimsProvider;

        #endregion

        #region Constructors

        public DeleteServiceAuthorisationEnforcer(IAuthorisationClaimsProvider authorisationClaimsProvider)
            => this._authorisationClaimsProvider = authorisationClaimsProvider ?? throw new ArgumentNullException(nameof(authorisationClaimsProvider));

        #endregion

        #region Methods

        async Task<IContinuationResult> IAuthorisationEnforcer<DeleteServiceInputPort, IDeleteServiceOutputPort>.AuthoriseAsync(
            DeleteServiceInputPort inputPort,
            IDeleteServiceOutputPort outputPort,
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
