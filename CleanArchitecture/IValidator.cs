namespace CleanArchitecture
{

    public interface IValidator<TInputPort, TOutputPort>
        where TInputPort : IInputPort<TOutputPort>
        where TOutputPort : IOutputPort
    {

        #region Methods

        Task<IContinuationResult> ValidateAsync(TInputPort inputPort, TOutputPort outputPort, CancellationToken cancellationToken);

        #endregion Methods

    }

}
