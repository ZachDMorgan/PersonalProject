using Application.Dtos;
using CleanArchitecture;
using Domain.Enumerations;

namespace Application.UseCases.Users.UpdateUser
{

    public class UpdateUserInputPort : IInputPort<IUpdateUserOutputPort>
    {

        #region Properties

        public UpdatePersonDto Person { get; set; }

        public ChangeTracker<string> Password { get; set; }

        public ChangeTracker<UserRole> Role { get; set; }

        public Guid UserID { get; set; }

        #endregion

    }

}
