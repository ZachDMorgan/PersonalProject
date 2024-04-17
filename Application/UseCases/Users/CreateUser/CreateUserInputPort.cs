using Application.Dtos;
using CleanArchitecture;
using Domain.Enumerations;

namespace Application.UseCases.Users.CreateUser
{

    public class CreateUserInputPort : IInputPort<ICreateUserOutputPort>
    {

        #region Properties

        public ContactDetailsDto ContactDetails { get; set; }

        public string FirstName { get; set; }

        public string Password { get; set; }

        public Guid PractitionerID { get; set; }

        public UserRole Role { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        public string Username { get; set; }

        #endregion

    }

}
