using Domain.Entities;

namespace Application.Dtos
{

    public class ServiceDto
    {

        #region Properties

        public Guid ServiceID { get; set; }

        public double Cost { get; set; }

        public int Duration { get; set; }

        public string Name { get; set; }

        #endregion

        #region Methods

        public static implicit operator ServiceDto(Service service) => new()
        {
            Cost = service.Cost,
            Duration = service.Duration,
            Name = service.Name,
            ServiceID = service.ID
        };

        #endregion

    }

}
