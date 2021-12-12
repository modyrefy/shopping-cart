using Server.Model.Dto.User;
using Server.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Interfaces.Repositories
{
    public interface ITestRepository
    {
       Task<List<Get_User_InformationResult>> SearchAsync(string UserNmae, string Password);
        public dynamic SearchContext();
        public dynamic SearchContextExpression();

        Task<ActiveUserContext> AuthincateUser(UserAuthenticateRequestModel request);
    }
}
