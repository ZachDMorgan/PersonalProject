using Application.Services;
using Application.Services.Authorisation;
using CleanArchitecture;
using Domain.Enumerations;

namespace Application.UseCases.Practitioners.DeletePractitioner
{

    public class DeletePractitionerAuthorisationEnforcer : IAuthorisationEnforcer<DeletePractitionerInputPort, IDeletePractitionerOutputPort>
    {

        #region Fields

        private readonly IAuthorisationClaimsProvider _authorisationClaimsProvider;

        #endregion

        #region Constructors

        public DeletePractitionerAuthorisationEnforcer(IAuthorisationClaimsProvider authorisationClaimsProvider)
            => this._authorisationClaimsProvider = authorisationClaimsProvider ?? throw new ArgumentNullException(nameof(authorisationClaimsProvider));

        #endregion

        #region Methods

        async Task<IContinuationResult> IAuthorisationEnforcer<DeletePractitionerInputPort, IDeletePractitionerOutputPort>.AuthoriseAsync(
            DeletePractitionerInputPort inputPort,
            IDeletePractitionerOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            if (await this._authorisationClaimsProvider.DoesUserHaveClaimAsync(AuthorisationClaim.CanAlterPractitioners))
                return new ContinuationResult();
            await outputPort.PresentUnauthorisedAsync(GenericErrorMessageProvider.GetUnauthorisedMessage(AuthorisationClaim.CanAlterPractitioners), cancellationToken);
            return new ContinuationResult(ContinuationResultBehavior.Bail);
        }

        #endregion

    }

}
