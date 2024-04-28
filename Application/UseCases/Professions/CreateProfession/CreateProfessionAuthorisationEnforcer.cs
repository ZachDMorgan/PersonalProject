using Application.Services;
using Application.Services.Authorisation;
using CleanArchitecture;
using Domain.Enumerations;

namespace Application.UseCases.Professions.CreateProfession
{

    public class CreateProfessionAuthorisationEnforcer : IAuthorisationEnforcer<CreateProfessionInputPort, ICreateProfessionOutputPort>
    {
        #region Fields

        private readonly IAuthorisationClaimsProvider _authorisationClaimsProvider;

        #endregion

        #region Constructors

        public CreateProfessionAuthorisationEnforcer(IAuthorisationClaimsProvider authorisationClaimsProvider)
            => this._authorisationClaimsProvider = authorisationClaimsProvider ?? throw new ArgumentNullException(nameof(authorisationClaimsProvider));

        #endregion

        #region Methods

        async Task<IContinuationResult> IAuthorisationEnforcer<CreateProfessionInputPort, ICreateProfessionOutputPort>.AuthoriseAsync(
            CreateProfessionInputPort inputPort,
            ICreateProfessionOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            if (await this._authorisationClaimsProvider.DoesUserHaveClaimAsync(AuthorisationClaim.CanCreateProfession))
                return new ContinuationResult();
            await outputPort.PresentUnauthorisedAsync(GenericErrorMessageProvider.GetUnauthorisedMessage(AuthorisationClaim.CanCreateProfession), cancellationToken);
            return new ContinuationResult(ContinuationResultBehavior.Bail);
        }

        #endregion

    }

}
