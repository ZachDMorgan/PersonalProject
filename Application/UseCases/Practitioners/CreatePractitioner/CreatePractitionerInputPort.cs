using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Practitioners.CreatePractitioner
{

    public class CreatePractitionerInputPort : IInputPort<ICreatePractitionerOutputPort>
    {

        #region Properties

        public PersonDto Person { get; set; }

        public Guid ProfessionID { get; set; }

        public HashSet<Guid> ServiceIDs { get; set; }

        #endregion

    }

}
