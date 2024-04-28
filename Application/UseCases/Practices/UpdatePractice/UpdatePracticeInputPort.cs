using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Practices.UpdatePractice
{

    public class UpdatePracticeInputPort : IInputPort<IUpdatePracticeOutputPort>
    {

        #region Properties

        public ChangeTracker<string> Address { get; set; }

        public UpdateContactDetailsDto ContactDetails { get; set; }

        public ChangeTracker<string> Description { get; set; }

        public ChangeTracker<bool> IsActive { get; set; }

        public ChangeTracker<string> Name { get; set; }

        public Guid PracticeID { get; set; }

        #endregion

    }

}
