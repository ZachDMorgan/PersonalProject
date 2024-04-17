using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Validation
{

    internal interface IDtoValidator<TDto> where TDto : class
    {

        #region Methods

        bool Validate(TDto dto, out ICollection<ValidationError> errors);

        #endregion

    }

}
