using Application.Dtos;
using Application.DtoValidators;
using Application.Services.Persistence;
using Application.Services.Validation;
using CleanArchitecture;
using Domain.Entities;
using Domain.Enumerations;

namespace Application.UseCases.Users.UpdateUser
{

    public class UpdateUserValidator : DtoValidator, IValidator<UpdateUserInputPort, IUpdateUserOutputPort>
    {

        #region Fields

        private readonly IDtoValidator<UpdatePersonDto> _personValidator;
        private readonly IPersistenceContext _persistenceContext;

        #endregion

        #region Constructors

        public UpdateUserValidator(IPersistenceContext persistenceContext)
        {
            this._personValidator = new UpdatePersonValidator(new UpdateContactDetailsValidator());
            this._persistenceContext = persistenceContext;
        }

        #endregion

        #region Methods

        async Task<IContinuationResult> IValidator<UpdateUserInputPort, IUpdateUserOutputPort>.ValidateAsync(
            UpdateUserInputPort inputPort,
            IUpdateUserOutputPort outputPort,
            CancellationToken cancellationToken)
        {
            var _HasErrors = false;
            if (!this.ValidateInputPort(inputPort, out var validationErrors))
            {
                await outputPort.PresentInvalidInputAsync(validationErrors, cancellationToken);
                _HasErrors = true;
            }

            if (!_HasErrors)
            {
                var _User = this._persistenceContext.GetEntities<User>().SingleOrDefault(u => u.ID == inputPort.UserID);
                if (_User == null)
                {
                    await outputPort.PresentUserDoesNotExistAsync(inputPort.UserID, cancellationToken);
                    _HasErrors = true;
                }
                else if (inputPort.Role.HasBeenSet
                    && _User.Role == UserRole.SuperAdmin
                    && this._persistenceContext.GetEntities<User>().Count(u => u.Role == Domain.Enumerations.UserRole.SuperAdmin) < 2)
                {
                    _HasErrors = true;
                    await outputPort.PresentCannotRemoveLastSuperAdminAsync(cancellationToken);
                }
            }

            return new ContinuationResult(_HasErrors ? ContinuationResultBehavior.Bail : ContinuationResultBehavior.Continue);
        }

        private bool ValidateInputPort(UpdateUserInputPort inputPort, out ICollection<ValidationError> errors)
        {
            errors = new List<ValidationError>();
            var _IsValid = this._personValidator.Validate(inputPort.Person, out errors);

            if (inputPort.UserID == Guid.Empty)
            {
                errors.Add(this.PropertyIsInvalid(nameof(inputPort.UserID)));
                _IsValid = false;
            }

            if (inputPort.Password.HasBeenSet && string.IsNullOrWhiteSpace(inputPort.Password.Value))
            {
                errors.Add(this.PropertyIsEmpty(nameof(inputPort.Password)));
                _IsValid = false;
            }
            return _IsValid;
        }

        #endregion

    }

}
