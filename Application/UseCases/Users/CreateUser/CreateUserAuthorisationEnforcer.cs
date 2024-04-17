using Application.Services;
using Application.Services.Authorisation;
using CleanArchitecture;
using Domain.Enumerations;

namespace Application.UseCases.Users.CreateUser
{

    public class CreateUserAuthorisationEnforcer : IAuthorisationEnforcer<CreateUserInputPort, ICreateUserOutputPort>
    {
        #region Fields

        private readonly IAuthorisationClaimsProvider _authorisationClaimsProvider;

        #endregion

        #region Constructors

        public CreateUserAuthorisationEnforcer(IAuthorisationClaimsProvider authorisationClaimsProvider)
            => this._authorisationClaimsProvider = authorisationClaimsProvider ?? throw new ArgumentNullException(nameof(authorisationClaimsProvider));

        #endregion

        #region Methods

        async Task<IContinuationResult> IAuthorisationEnforcer<CreateUserInputPort, ICreateUserOutputPort>.AuthoriseAsync(
            CreateUserInputPort inputPort,
            ICreateUserOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            var _RequiredClaim = AuthorisationClaim.CanCreateUsers;
            if (inputPort.Role == UserRole.Admin)
                _RequiredClaim = AuthorisationClaim.CanCreateAdmins;
            if (inputPort.Role == UserRole.Practitioner)
                _RequiredClaim = AuthorisationClaim.CanCreatePractitionerUsers;
            if (inputPort.Role == UserRole.SuperAdmin)
                _RequiredClaim = AuthorisationClaim.CanCreateSuperAdmins;

            if (await this._authorisationClaimsProvider.DoesUserHaveClaimAsync(_RequiredClaim))
                return new ContinuationResult();
            await outputPort.PresentUnauthorisedAsync(GenericErrorMessageProvider.GetUnauthorisedMessage(_RequiredClaim), cancellationToken);
            return new ContinuationResult(ContinuationResultBehavior.Bail);
        }

        #endregion

    }

}
