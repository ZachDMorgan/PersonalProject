using Application.Services.Persistence;
using CleanArchitecture;
using Domain.Entities;

namespace Application.UseCases.Users.CreateUser
{

    public class CreateUserInteractor : IInteractor<CreateUserInputPort, ICreateUserOutputPort>
    {

        #region Fields

        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Contructors

        public CreateUserInteractor(IPersistenceContext persistenceContext)
            => this._persistenceContext = persistenceContext;

        #endregion

        #region Methods

        async Task<IContinuationResult> IInteractor<CreateUserInputPort, ICreateUserOutputPort>.InteractAsync(CreateUserInputPort inputPort, ICreateUserOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _User = new User()
            {
                Password = inputPort.Password,
                Role = inputPort.Role,
                Username = inputPort.Username,
            };

            if (inputPort.PractitionerID.HasValue)
            {
                var _Practitioner = this._persistenceContext.GetEntities<Practitioner>()
                    .Where(p => p.ID == inputPort.PractitionerID)
                    .Select(p => new
                    {
                        Practitioner = p,
                        p.Person
                    })
                    .Single().Practitioner;
                _User.Practitioner = _Practitioner;
                _User.Person = _Practitioner.Person;
            }
            else
                _User.Person = inputPort.Person;

            this._persistenceContext.Add(_User);

            await outputPort.PresentUserAsync(_User, cancellationToken);
            return new ContinuationResult();
        }

        #endregion

    }

}
