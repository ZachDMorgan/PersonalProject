using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Users.UpdateUser
{

    public class UpdateUserInteractor : IInteractor<UpdateUserInputPort, IUpdateUserOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public UpdateUserInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<UpdateUserInputPort, IUpdateUserOutputPort>.InteractAsync(UpdateUserInputPort inputPort, IUpdateUserOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _User = this._persistenceContext.GetEntities<User>()
                .Where(u => u.ID == inputPort.UserID)
                .Select(u => new
                {
                    User = u,
                    u.Person,
                    u.Person.ContactDetails
                })
                .SingleOrDefault()!.User;

            if (inputPort.Password.HasBeenSet)

                if (inputPort.Person != null)
                    inputPort.Person.ApplyChanges(_User.Person);
            _User.Password = inputPort.Password.Value;

            if (inputPort.Role.HasBeenSet)
                _User.Role = inputPort.Role.Value;

            await outputPort.PresentUpdatedUserAsync(_User, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
