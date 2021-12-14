using Server.Model.Dto;
using Server.Model.Interfaces.Context;
using Server.Model.Interfaces.Validation;
using System;
using System.Text.RegularExpressions;

namespace Server.BusinessValidation.Validations.RegularExpression
{
    public class RegularExpressionValidation : IRegularExpressionValidation
    {
        #region Singleton
        private static RegularExpressionValidation _instance = null;
        private RegularExpressionValidation()
        {

        }

        public static RegularExpressionValidation Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RegularExpressionValidation();

                }
                return _instance;
            }
        }

        #endregion
        #region public
        public bool Validate(string stringToMatch, string validationRegEx, bool isMandatory)
        {
            bool isValid;
            try
            {
                if (string.IsNullOrEmpty(stringToMatch) && isMandatory == true)
                { isValid = false; }
                else if (string.IsNullOrEmpty(stringToMatch) && isMandatory == false)
                { isValid = true; }
                else
                {
                    Regex rgx = new Regex(validationRegEx);
                    isValid = rgx.IsMatch(stringToMatch.Trim());
                }
            }
            catch (Exception ex)
            {
                isValid = false;
            }
            return isValid;
        }
        public ValidationError Validate(string stringToMatch, string validationRegEx, string errorMessage, bool isMandatory)
        {
            bool isValid=Validate(stringToMatch,validationRegEx,isMandatory);
            return isValid ? null : new ValidationError() { 
            ErrorMessage=errorMessage,
            };
        }

        public ValidationError Validate(IRequestContext requestContext, string stringToMatch, string validationRegEx, string errorMessage, bool isMandatory)
        {
            var px = requestContext.ActiveUserContext;
            return null;
        }
        #endregion
    }
}
