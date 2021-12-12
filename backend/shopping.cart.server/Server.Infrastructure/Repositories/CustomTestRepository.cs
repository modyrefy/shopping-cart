using Microsoft.EntityFrameworkCore;
using Server.Infrastructure.Data;
using Server.Infrastructure.Repositories.EFCore;
using Server.Model.Dto.User;
using Server.Model.Interfaces.Repositories;
using Server.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories
{
    public class CustomTestRepository : EfCoreRepository<Object>, ITestRepository
    {
        public CustomTestRepository(DefaultDBContext dBContext) : base(dBContext)
        {
        }

        public Task<ActiveUserContext>  AuthincateUser(UserAuthenticateRequestModel request)
        {
            return (from user in Context.Users
                    join role in Context.UserRoles
                    on user.UserRoleId equals role.UserRoleId
                    join state in Context.UserStates
                    on user.UserStateId equals state.UserStateId
                    where user.UserStateId==1
                    && user.UserName.Trim().ToLower()== request.Username.Trim().ToLower()
                    && user.Password.Trim()==request.Password.Trim()
                    select new ActiveUserContext
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        ParticipantName = user.ParticipantName,
                        Phone = user.Phone,
                        Mobile = user.Mobile,
                        Email = user.Email,
                        CountryId = user.CountryId,
                        City = user.City,
                        State = user.State,
                        Address = user.Address,
                        UserRoleId = user.UserRoleId,
                        UserStateId = user.UserStateId,
                        UserRole = role.UserRole,
                        UserState= state.UserState
                    }).FirstOrDefaultAsync();
        }

        public async Task<List<Get_User_InformationResult>> SearchAsync(string UserNmae, string Password)
        {

            return await Context.Procedures.Get_User_InformationAsync("ahmed", "123");
        }
        public dynamic SearchContext()
        {

            return (from p in Context.Users
                    join p1 in Context.UserRoles
on p.UserRoleId equals p1.UserRoleId
                    select new
                    {
                        UserId = p.UserId,
                        UserName = p.UserName + ' ' + Guid.NewGuid().ToString(),
                        RoleId = p1.UserRoleId,
                        RoleName = p1.UserRole
                    }).ToList();

        }
        public dynamic SearchContextExpression()
        {
            var query = from p in Context.Users
                        join p1 in Context.UserRoles
    on p.UserRoleId equals p1.UserRoleId
                        select new
                        {
                            UserId = p.UserId,
                            UserName = p.UserName + ' ' + Guid.NewGuid().ToString(),
                            RoleId = p1.UserRoleId,
                            RoleName = p1.UserRole
                        };
            query = query.Where(p => p.UserId == 1);
            query = query.Where(p => p.UserName != "abc");
            //query=this.CreateExpression(query, "UserName", "democustomer1");
            return query.ToList();
        }
    }
}
