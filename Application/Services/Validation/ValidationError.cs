namespace Application.Services.Validation
{

    public class ValidationError
    {

        #region Fields

        private readonly string _message;
        private readonly string _propertyInError;

        #endregion

        #region Constructors

        public ValidationError(string propertyInError, string message)
        {
            this._message = message;
            this._propertyInError = propertyInError;
        }

        #endregion

        #region Properties

        public string Message { get => this._message; }

        public string PropertyInError { get => this._propertyInError; }

        #endregion

    }

}
