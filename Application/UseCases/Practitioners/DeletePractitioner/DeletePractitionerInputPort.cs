using CleanArchitecture;

namespace Application.UseCases.Practitioners.DeletePractitioner
{

    public class DeletePractitionerInputPort : IInputPort<IDeletePractitionerOutputPort>
    {

        #region Properties

        public Guid PractitionerID { get; set; }

        #endregion

    }

}
