using CleanArchitecture;

namespace Application.UseCases.Professions.CreateProfession
{

    public class CreateProfessionInputPort : IInputPort<ICreateProfessionOutputPort>
    {

        #region Properties

        public string Name { get; set; }

        #endregion

    }

}
