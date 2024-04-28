using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Professions.DeleteProfession
{

    public interface IDeleteProfessionOutputPort : IOutputPort
    {

        #region Methods

        Task PresentDeletedProfessionAsync(ProfessionDto Profession, CancellationToken cancellationToken);

        Task PresentProfessionInUseAsync(CancellationToken cancellationToken);

        Task PresentProfessionNotFoundAsync(Guid ProfessionID, CancellationToken cancellationToken);

        Task PresentUnauthorisedAsync(string message, CancellationToken cancellationToken);

        #endregion

    }

}
