using CleanArchitecture;

namespace Application.UseCases.Professions.DeleteProfession
{

    public class DeleteProfessionInputPort : IInputPort<IDeleteProfessionOutputPort>
    {

        #region Properties

        public Guid ProfessionID { get; set; }

        #endregion

    }

}
