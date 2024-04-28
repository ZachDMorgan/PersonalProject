using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Services.UpdateService
{

    public class UpdateServiceInputPort : IInputPort<IUpdateServiceOutputPort>
    {

        #region Properties

        public ChangeTracker<double> Cost { get; set; }

        public ChangeTracker<int> Duration { get; set; }

        public ChangeTracker<string> Name { get; set; }

        public Guid ServiceID { get; set; }

        #endregion

    }

}
