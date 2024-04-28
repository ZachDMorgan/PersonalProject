namespace Domain.Enumerations
{

    public enum AuthorisationClaim
    {
        //Cheat
        Unauthorised,

        //Practices
        CanAlterPractices,
        CanCreatePractices,

        //Practitioners
        CanAlterPractitioners,
        CanCreatePractitioners,

        //Professions
        CanAlterProfessions,
        CanCreateProfessions,

        //Services
        CanAlterServices,
        CanCreateServices,

        //Users
        CanAlterOtherUsers,
        CanAlterUserRoles,
        CanCreateAdmins,
        CanCreatePractitionerUsers,
        CanCreateSuperAdmins,
        CanCreateUsers,
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
