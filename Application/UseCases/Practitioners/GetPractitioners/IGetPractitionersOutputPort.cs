using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Practitioners.GetPractitioners
{

    public interface IGetPractitionersOutputPort : IOutputPort
    {

        #region Methods

        Task PresentPractitionersAsync(IQueryable<PractitionerDto> Practitioners, CancellationToken cancellationToken);

        #endregion

    }

}
