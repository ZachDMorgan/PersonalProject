namespace CleanArchitecture
{
    public abstract class UseCaseInvoker<TInputPort, TOutputPort> : IUseCaseInvoker<TInputPort, TOutputPort>
        where TInputPort : IInputPort<TOutputPort>
        where TOutputPort : IOutputPort
    {

        #region Fields

        private readonly IAuthorisationEnforcer<TInputPort, TOutputPort>? _authorisationEnforcer;
        private readonly IInteractor<TInputPort, TOutputPort> _interactor;
        private readonly IValidator<TInputPort, TOutputPort>? _validator;

        #endregion Fields

        #region Constructors

        protected UseCaseInvoker(IInteractor<TInputPort, TOutputPort> interactor)
            => this._interactor = interactor;

        protected UseCaseInvoker(
            IAuthorisationEnforcer<TInputPort, TOutputPort> authorisationEnforcer,
            IInteractor<TInputPort, TOutputPort> interactor) : this(interactor)
            => this._authorisationEnforcer = authorisationEnforcer;

        protected UseCaseInvoker(
            IInteractor<TInputPort, TOutputPort> interactor,
            IValidator<TInputPort, TOutputPort> validator) : this(interactor)
            => this._validator = validator;

        protected UseCaseInvoker(
            IAuthorisationEnforcer<TInputPort, TOutputPort> authorisationEnforcer,
            IInteractor<TInputPort, TOutputPort> interactor,
            IValidator<TInputPort, TOutputPort> validator) : this(authorisationEnforcer, interactor)
            => this._validator = validator;

        #endregion Constructors

        #region Methods

        async Task IUseCaseInvoker<TInputPort, TOutputPort>.InvokeAsync(TInputPort inputPort, TOutputPort outputPort, CancellationToken cancellationToken)
        {//TODO: work out if I like this or I want to use mediator
            var _ContinuationState = ContinuationResultBehavior.Continue;
            if (this._authorisationEnforcer != null)
            {
                var _Result = await this._authorisationEnforcer.AuthoriseAsync(inputPort, outputPort, cancellationToken);
                _ContinuationState = _Result.ContinuationResult;
            }
            if (this._validator != null && _ContinuationState == ContinuationResultBehavior.Continue)
            {
                var _Result = await this._validator.ValidateAsync(inputPort, outputPort, cancellationToken);
                _ContinuationState = _Result.ContinuationResult;
            }
            if (_ContinuationState == ContinuationResultBehavior.Continue)
            {
                var _Result = await this._interactor.InteractAsync(inputPort, outputPort, cancellationToken);
                _ContinuationState = _Result.ContinuationResult;
            }
        }

        #endregion Methods

    }
}
