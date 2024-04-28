using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Practices.GetPractices
{

    public interface IGetPracticesOutputPort : IOutputPort
    {

        #region Methods

        Task PresentPracticesAsync(IQueryable<PracticeDto> Practices, CancellationToken cancellationToken);

        #endregion

    }

}
