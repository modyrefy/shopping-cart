using Server.Model.Dto;
using Server.Model.Interfaces.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Interfaces.Validation
{
    public interface IRequestValidation<T>
    {
        #region Validate
        List<ValidationError> Validate(T request, Dictionary<string, object> otherParamters);
        #endregion
    }

    public interface IRegularExpressionValidation
    {
        bool Validate(string stringToMatch, string validationRegEx, bool isMandatory);
        ValidationError Validate(string stringToMatch, string validationRegEx,string errorMessage, bool isMandatory);
        ValidationError Validate(IRequestContext requestContext, string stringToMatch, string validationRegEx, string errorMessage, bool isMandatory);
    }
}
