using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Practitioners.UpdatePractitioner
{

    public class UpdatePractitionerInputPort : IInputPort<IUpdatePractitionerOutputPort>
    {

        #region Properties

        public ChangeTracker<bool> IsActive { get; set; }

        public UpdatePersonDto Person { get; set; }

        public ChangeTracker<Guid> ProfessionID { get; set; }

        public Guid PractitionerID { get; set; }

        public ChangeTracker<ICollection<Guid>> ServiceIDs { get; set; }

        #endregion

    }

}
