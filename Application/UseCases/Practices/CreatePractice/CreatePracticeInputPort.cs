using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Practices.CreatePractice
{

    public class CreatePracticeInputPort : IInputPort<ICreatePracticeOutputPort>
    {

        #region Properties

        public string Address { get; set; }

        public ContactDetailsDto ContactDetails { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        #endregion

    }

}
