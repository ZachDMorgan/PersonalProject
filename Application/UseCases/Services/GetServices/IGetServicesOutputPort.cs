using Application.Dtos;
using CleanArchitecture;

namespace Application.UseCases.Services.GetServices
{

    public interface IGetServicesOutputPort : IOutputPort
    {

        #region Methods

        Task PresentServicesAsync(IQueryable<ServiceDto> Services, CancellationToken cancellationToken);

        #endregion

    }

}
