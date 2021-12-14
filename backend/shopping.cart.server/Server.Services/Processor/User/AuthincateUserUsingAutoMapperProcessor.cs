using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Server.Core.BaseClasses;
using Server.Model.Dto;
using Server.Model.Dto.User;
using Server.Model.Interfaces.Context;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Processor.User
{
    public class AuthincateUserUsingAutoMapperProcessor : ProcessorBase<UserAuthenticateRequestModel, ActiveUserContext>
    {
        public AuthincateUserUsingAutoMapperProcessor(IRequestContext context) : base(context)
        {
        }

        public override ResponseBase<ActiveUserContext> DoProcess(UserAuthenticateRequestModel request)
        {
            return null;
        }

        public override async Task<ResponseBase<ActiveUserContext>> DoProcessAsync(UserAuthenticateRequestModel request)
        {
            ActiveUserContext activeUserContext = null;
            List<ValidationError> validationList = DoValidation(request);
            if (validationList == null || validationList.Count == 0)
            {
                var query = (from p in this.RequestContext.Repositories.UserRepository.GetAllQueryable().Include(p => p.UserRole).Include(p => p.UserState) select p);
                query = query.Where(p => p.UserName.ToLower().Trim() == request.Username.ToLower().Trim());
                query = query.Where(p => p.Password.Trim() == request.Password.Trim());
                var result = query.FirstOrDefaultAsync();
                if (result != null)
                {
                    activeUserContext = RequestContext.Mapper.Map<ActiveUserContext>(result);
                    if (activeUserContext != null)
                    {
                        this.RequestContext.DistributedCacheManager.Set(activeUserContext.UserId.ToString(), activeUserContext);
                    }
                    else
                    {
                        validationList ??= new List<ValidationError>();
                        validationList.Add(new ValidationError()
                        {
                            ErrorMessage = this.ValidationMessages.GetString("user_Unauthorized")
                        });
                    }
                }
            }
            //var user = this.RequestContext.Repositories.UsersRepository.SingleOrDefault(p => p.UserName == request.Username && p.Password == request.Password);
            return new ResponseBase<ActiveUserContext>()
            {
                Result = activeUserContext,
                Errors = validationList,
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
                if (string.IsNullOrEmpty(request.Password))
                {
                    validationList.Add(new ValidationError() { ErrorMessage = this.ValidationMessages.GetString("password_missing") });
                }
            }
            return validationList;
        }
    }
}
