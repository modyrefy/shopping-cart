using Microsoft.Extensions.Localization;
using Server.BusinessValidation.Validations.RegularExpression;
using Server.Core.BaseClasses;
using Server.Model.Dto;
using Server.Model.Dto.User;
using Server.Model.Interfaces.Context;
using Server.Model.Models;
using Server.Resources.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Processor.User
{
    public class RegisterUserProcessor : ProcessorBase<UserModel, ActiveUserContext>
    {
        #region constructor
        public RegisterUserProcessor(IRequestContext context) : base(context)
        {
        }
        #endregion
        #region public
        public override ResponseBase<ActiveUserContext> DoProcess(UserModel request)
        {
            throw new NotImplementedException();
        }

        public override async Task<ResponseBase<ActiveUserContext>> DoProcessAsync(UserModel request)
        {
            ActiveUserContext activeUserContext = null;
            List<ValidationError> validationList = DoValidation(request);
            //throw new Exception("xxxxxx");
;            if (validationList == null || validationList.Count == 0)
            {
                Users userEntity = RequestContext.Mapper.Map<Users>(request);
                userEntity = await this.RequestContext.Repositories.UsersRepository.InsertAsync(userEntity);
                await this.RequestContext.Repositories.UsersRepository.SaveChangesAsync();
                activeUserContext=this.RequestContext.Mapper.Map<ActiveUserContext>(userEntity); 
            }
            return new ResponseBase<ActiveUserContext>()
            {
                Result = activeUserContext,
                Errors = validationList,
            };
        }

        public override List<ValidationError> DoValidation(UserModel request)
        {
            List<ValidationError> validationList = new();
            if (request == null)
            {
                validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("request_not_valid") });
            }
            else
            {
                if (string.IsNullOrEmpty(request.UserName))
                {
                    validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("user_name_missing") });
                }
                else
                { 
                var user=this.RequestContext.Repositories.UsersRepository.SingleOrDefault(p=>p.UserName.ToLower().Trim() == request.UserName.ToLower());
                    if (user != null)
                    {
                        validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("user_name_already_exist") });
                    }
                    if (string.IsNullOrEmpty(request.Password))
                    {
                        validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("password_missing") });
                    }
                    if (string.IsNullOrEmpty(request.ParticipantName))
                    {
                        validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("ParticipantName_missing") });
                    }
                    if (string.IsNullOrEmpty(request.City))
                    {
                        validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("city_missing") });
                    }
                    if (string.IsNullOrEmpty(request.State))
                    {
                        validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("state_missing") });
                    }
                    if (string.IsNullOrEmpty(request.Address))
                    {
                        validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("address_missing") });
                    }
                    if (request.CountryId.GetValueOrDefault() <= 0)
                    {
                        validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("country_missing") });
                    }
                    if (request.UserRoleId<=0)
                    {
                        validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("UserRole_missing") });
                    }

                    if (request.UserStateId <= 0)
                    {
                        validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("UserState_missing") });
                    }

                    if (RegularExpressionValidation.Instance.Validate(request.Email, RegExResource.EmailRegEx, true))
                    {
                        validationList.Add(new ValidationError() {
                        ErrorMessage=this.ValidationMessages.GetString("email_missing_or_not_valid"),
                        });
                    }
                    if (RegularExpressionValidation.Instance.Validate(request.Mobile, RegExResource.MobileRegEx, true))
                    {
                        validationList.Add(new ValidationError()
                        {
                            ErrorMessage = this.ValidationMessages.GetString("mobile_missing_or_not_valid"),
                        });
                    }
                    

                }
            }
            return validationList;
        }
        #endregion
    }
}
