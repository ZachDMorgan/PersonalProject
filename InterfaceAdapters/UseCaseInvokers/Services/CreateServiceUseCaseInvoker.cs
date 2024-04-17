using Application.UseCases.Services.CreateService;
using CleanArchitecture;

namespace InterfaceAdapters.UseCaseInvokers.Services
{

    public class CreateServiceUseCaseInvoker : UseCaseInvoker<CreateServiceInputPort, ICreateServiceOutputPort>
    {

        #region Constructors

        public CreateServiceUseCaseInvoker(
            IAuthorisationEnforcer<CreateServiceInputPort, ICreateServiceOutputPort> authorisationEnforcer,
            IInteractor<CreateServiceInputPort, ICreateServiceOutputPort> interactor,
            IValidator<CreateServiceInputPort, ICreateServiceOutputPort> validator) : base(authorisationEnforcer, interactor, validator) { }

        #endregion

    }

}
