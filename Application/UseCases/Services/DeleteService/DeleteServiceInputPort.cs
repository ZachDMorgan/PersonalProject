using CleanArchitecture;

namespace Application.UseCases.Services.DeleteService
{

    public class DeleteServiceInputPort : IInputPort<IDeleteServiceOutputPort>
    {

        #region Properties

        public Guid ServiceID { get; set; }

        #endregion

    }

}
