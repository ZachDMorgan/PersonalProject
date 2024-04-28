using Application.Services;
using Application.Services.Authorisation;
using CleanArchitecture;
using Domain.Enumerations;

namespace Application.UseCases.Professions.DeleteProfession
{

    public class DeleteProfessionAuthorisationEnforcer : IAuthorisationEnforcer<DeleteProfessionInputPort, IDeleteProfessionOutputPort>
    {

        #region Fields

        private readonly IAuthorisationClaimsProvider _authorisationClaimsProvider;

        #endregion

        #region Constructors

        public DeleteProfessionAuthorisationEnforcer(IAuthorisationClaimsProvider authorisationClaimsProvider)
            => this._authorisationClaimsProvider = authorisationClaimsProvider ?? throw new ArgumentNullException(nameof(authorisationClaimsProvider));

        #endregion

        #region Methods

        async Task<IContinuationResult> IAuthorisationEnforcer<DeleteProfessionInputPort, IDeleteProfessionOutputPort>.AuthoriseAsync(
            DeleteProfessionInputPort inputPort,
            IDeleteProfessionOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            if (await this._authorisationClaimsProvider.DoesUserHaveClaimAsync(AuthorisationClaim.CanAlterProfessions))
                return new ContinuationResult();
            await outputPort.PresentUnauthorisedAsync(GenericErrorMessageProvider.GetUnauthorisedMessage(AuthorisationClaim.CanAlterProfessions), cancellationToken);
            return new ContinuationResult(ContinuationResultBehavior.Bail);
        }

        #endregion

    }

}
