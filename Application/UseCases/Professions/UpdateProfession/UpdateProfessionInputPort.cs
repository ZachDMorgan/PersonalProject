using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Professions.UpdateProfession
{

    public class UpdateProfessionInputPort : IInputPort<IUpdateProfessionOutputPort>
    {

        #region Properties

        public ChangeTracker<string> Name { get; set; }

        public Guid ProfessionID { get; set; }

        #endregion

    }

}
