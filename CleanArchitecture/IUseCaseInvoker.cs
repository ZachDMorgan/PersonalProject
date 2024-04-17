namespace CleanArchitecture
{

    internal interface IUseCaseInvoker<TInputPort, TOutputPort>
        where TInputPort : IInputPort<TOutputPort>
        where TOutputPort : IOutputPort
    {

        #region Methods

        Task InvokeAsync(TInputPort inputPort, TOutputPort outputPort, CancellationToken cancellationToken);

        #endregion Methods

    }

}
