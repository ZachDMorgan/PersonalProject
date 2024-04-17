using Application.Dtos;
using CleanArchitecture;
using Domain.Enumerations;

namespace Application.UseCases.Users.CreateUser
{

    public class CreateUserInputPort : IInputPort<ICreateUserOutputPort>
    {

        #region Properties

        public PersonDto Person { get; set; }

        public string Password { get; set; }

        public Guid? PractitionerID { get; set; }

        public UserRole Role { get; set; }

        public string Username { get; set; }

        #endregion

    }

}
