namespace Domain.Enumerations
{

    public enum AuthorisationClaim
    {
        Unauthorised, //cheat
        CanAlterPractices,
        CanCreatePractices,
        CanCreatePractitioners,
        CanCreateProfessions,
        CanCreateServices,
        CanCreateUsers,
        CanCreateAdmins,
        CanCreatePractitionerUsers,
        CanCreateSuperAdmins,
    }


    //TODO: Make AuthClaims Smart Enum
    //public class AuthorisationClaim : Enum
    //{
    //    #region Fields
    //    #endregion
    //    #region Constructors

    //    public AuthorisationClaim() : base() { this. }

    //    #endregion
    //    #region Methods
    //    #endregion
    //}

}
