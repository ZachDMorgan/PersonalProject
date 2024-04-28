using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Professions.GetProfessions
{

    public interface IGetProfessionsOutputPort : IOutputPort
    {

        #region Methods

        Task PresentProfessionsAsync(IQueryable<ProfessionDto> Professions, CancellationToken cancellationToken);

        #endregion

    }

}
