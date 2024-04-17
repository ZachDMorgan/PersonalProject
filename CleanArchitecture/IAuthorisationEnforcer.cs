namespace CleanArchitecture
{

    public interface IAuthorisationEnforcer<TInputPort, TOutputPort>
        where TInputPort : IInputPort<TOutputPort>
        where TOutputPort : IOutputPort
    {

        #region Methods

        Task<IContinuationResult> AuthoriseAsync(TInputPort inputPort, TOutputPort outputPort, CancellationToken cancellationToken);

        #endregion Methods

    }

}
