namespace CleanArchitecture
{
    public class ContinuationResult : IContinuationResult
    {

        #region Fields

        private readonly ContinuationResultBehavior _continuationResult;

        #endregion Fields

        #region Constructors

        public ContinuationResult(ContinuationResultBehavior continuationResult)
            => this._continuationResult = continuationResult;

        public ContinuationResult() : this(ContinuationResultBehavior.Continue) { }

        #endregion Constructors

        #region Properties

        ContinuationResultBehavior IContinuationResult.ContinuationResult { get => this._continuationResult; }

        #endregion Properties

    }
}
