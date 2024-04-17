namespace CleanArchitecture
{

    public interface IInteractor<TInputPort, TOutputPort>
        where TInputPort : IInputPort<TOutputPort>
        where TOutputPort : IOutputPort
    {

        #region Methods

        Task<IContinuationResult> InteractAsync(TInputPort inputPort, TOutputPort outputPort, CancellationToken cancellationToken);

        #endregion Methods

    }

}
