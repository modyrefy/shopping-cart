using Microsoft.Extensions.Localization;
using Server.Core.BaseClasses;
using Server.Core.Manager;
using Server.Model.Dto;
using Server.Model.Dto.User;
using Server.Model.Interfaces.Context;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Processor.User
{
    public class AuthincateUserProcessor : ProcessorBase<UserAuthenticateRequestModel ,ActiveUserContext>
    {
        public AuthincateUserProcessor(IRequestContext context) : base(context)
        {
        }

        public override  ResponseBase<ActiveUserContext> DoProcess(UserAuthenticateRequestModel request)
        {
            return null;
        }

        public override async Task<ResponseBase<ActiveUserContext>> DoProcessAsync(UserAuthenticateRequestModel request)
        {
            ActiveUserContext activeUserContext = null;
            List<ValidationError> validationList = DoValidation(request);
            string token = null;
            if (validationList == null || validationList.Count == 0)
            {
                activeUserContext = await this.RequestContext.Repositories.TestRepository.AuthincateUser(request);
               // throw new System.Exception("general error occurred");
                if (activeUserContext != null)
                {
                  token=  JwtManager.Instance.GenerateJSONWebToken(
                        new List<KeyValuePair<string, string>>() { 
                         new KeyValuePair<string, string>("UserId",activeUserContext.UserId.ToString())  
                        },
                        this.RequestContext.Configuration);
                    this.RequestContext.DistributedCacheManager.Set($"user-{activeUserContext.UserId.ToString()}", activeUserContext);
                }
                else
                {
                    validationList ??= new List<ValidationError>();
                    validationList.Add(new ValidationError()
                    {
                        ErrorMessage = this.ValidationMessages.GetString("user_Unauthorized")
                    });
                }
            };
            return  new ResponseBase<ActiveUserContext>()
            {
                Result = activeUserContext,
                Errors = validationList,
                Token=token,
            };
           
        }

        public override List<ValidationError> DoValidation(UserAuthenticateRequestModel request)
        {
            List<ValidationError> validationList = new();
            //request = null;
            if (request == null)
            {
                validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("request_not_valid") });
            }
            else
            {
                if (string.IsNullOrEmpty(request.Username))
                {
                    validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("user_name_missing") });
                }
                if(string.IsNullOrEmpty(request.Password))
                {
                    validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("password_missing") });
                }
            }
            return validationList;
        }
    }
}
