using Application.Services.Validation;
using System.Text;

namespace Application.DtoValidators
{

    public abstract class DtoValidator
    {

        #region Methods

        //TODO: Delimit at caps with spaces and make sure caps
        protected ValidationError PropertyIsEmpty(string properyInError)
            => new(properyInError, $"{properyInError} cannot be empty.");

        //TODO: Delimit at caps with spaces and make sure caps
        protected ValidationError PropertyIsInvalid(string properyInError)
            => new(properyInError, $"{properyInError} is invalid.");

        //TODO: Delimit at caps with spaces and make sure caps
        protected ValidationError PropertyIsLessThanZero(string properyInError)
            => new(properyInError, $"{properyInError} cannot be less than 0.");

        //TODO: Delimit at caps with spaces and make sure caps
        protected ValidationError PropertyIsRequired(string properyInError)
            => new(properyInError, $"{properyInError} is required.");

        //TODO: Delimit at caps with spaces and make sure caps
        protected ICollection<ValidationError> PropertyIsRequired(IEnumerable<string> properiesInError)
        {
            var _PropCount = properiesInError.Count();
            var _StringBuilder = new StringBuilder();
            for (int _Index = 0; _Index < _PropCount; _Index++)
            {
                _StringBuilder.Append(properiesInError.ElementAt(_Index));
                _StringBuilder.Append(' ');
                if (_Index < _PropCount - 1)
                    _StringBuilder.Append("or ");
            }

            _StringBuilder.Append("is required.");
            return properiesInError.Select(pie => new ValidationError(pie, _StringBuilder.ToString())).ToList();
        }

        #endregion

    }

}
