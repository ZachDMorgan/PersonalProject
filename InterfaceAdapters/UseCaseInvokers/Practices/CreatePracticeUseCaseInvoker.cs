using Application.UseCases.Practices.CreatePractice;
using CleanArchitecture;

namespace InterfaceAdapters.UseCaseInvokers.Practices
{

    public class CreatePracticeUseCaseInvoker : UseCaseInvoker<CreatePracticeInputPort, ICreatePracticeOutputPort>
    {

        #region Constructors

        public CreatePracticeUseCaseInvoker(
            IAuthorisationEnforcer<CreatePracticeInputPort, ICreatePracticeOutputPort> authorisationEnforcer,
            IInteractor<CreatePracticeInputPort, ICreatePracticeOutputPort> interactor,
            IValidator<CreatePracticeInputPort, ICreatePracticeOutputPort> validator) : base(authorisationEnforcer, interactor, validator) { }

        #endregion

    }

}
