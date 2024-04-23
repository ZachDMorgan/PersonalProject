using Application.Dtos;
using Application.Services.Authentication;
using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;
using Domain.Enumerations;

namespace Application.UseCases.Users.GetUsers
{

    public class GetUsersInteractor : IInteractor<GetUsersInputPort, IGetUsersOutputPort>
    {

        #region Fields

        private readonly IAuthenticatedUserProvider _authenticatedUserProvider;
        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public GetUsersInteractor(IAuthenticatedUserProvider authenticatedUserProvider, IPersistenceContext persistenceContext)
        {
            this._authenticatedUserProvider = authenticatedUserProvider ?? throw new ArgumentNullException(nameof(authenticatedUserProvider));
            this._persistenceContext = persistenceContext;
        }

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<GetUsersInputPort, IGetUsersOutputPort>.InteractAsync(GetUsersInputPort inputPort, IGetUsersOutputPort outputPort, CancellationToken cancellationToken)
        {
            //TODO: this shouldn't be in the interactor. This is lazy.
            var _AuthenticatedUser = this._authenticatedUserProvider.GetCurrentlyAuthenticatedUser();
            if (_AuthenticatedUser == null)
            {
                await outputPort.PresentUnauthenticatedAsync(cancellationToken);
                return new ContinuationResult(ContinuationResultBehavior.Bail);
            }

            var _UserQuery = this._persistenceContext.GetEntities<User>().Select(u => (UserDto)u);
            if (_AuthenticatedUser.Role == UserRole.Public)
                _UserQuery = _UserQuery.Where(u => u.UserID == _AuthenticatedUser.ID);

            await outputPort.PresentUsersAsync(_UserQuery, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
