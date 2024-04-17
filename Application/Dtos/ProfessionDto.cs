using Domain.Entities;

namespace Application.Dtos
{

    public class ProfessionDto
    {

        #region Properties

        public Guid ProfessionID { get; set; }

        public string Name { get; set; }

        #endregion

        #region Methods

        public static implicit operator ProfessionDto(Profession profession) => new()
        {
            Name = profession.Name,
            ProfessionID = profession.ID,
        };

        #endregion

    }

}
