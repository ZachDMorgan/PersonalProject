using CleanArchitecture;

namespace Application.UseCases.Practices.DeletePractice
{

    public class DeletePracticeInputPort : IInputPort<IDeletePracticeOutputPort>
    {

        #region Properties

        public Guid PracticeID { get; set; }

        #endregion

    }

}
