using CleanArchitecture;

namespace Application.UseCases.Services.CreateService
{

    public class CreateServiceInputPort : IInputPort<ICreateServiceOutputPort>
    {

        #region Properties

        public double Cost { get; set; }

        public int Duration { get; set; }

        public string Name { get; set; }

        #endregion

    }

}
