using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Practitioners.CreatePractitioner
{

    public class CreatePractitionerInputPort : IInputPort<ICreatePractitionerOutputPort>
    {

        #region Properties

        public ContactDetailsDto ContactDetails { get; set; }

        public string FirstName { get; set; }

        public Guid ProfessionID { get; set; }

        public HashSet<Guid> ServiceIDs { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        #endregion

    }

}
