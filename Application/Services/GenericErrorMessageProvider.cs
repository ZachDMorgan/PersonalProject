using Domain.Enumerations;

namespace Application.Services
{

    internal static class GenericErrorMessageProvider
    {

        #region Methods

        //TODO: Use display string method once this is a SmartEnum
        public static string GetUnauthorisedMessage(AuthorisationClaim missingClaim)
            => $"User does not have permission to {missingClaim}.";

        #endregion

    }

}
