using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using AntropoPollWebApi.Core.Models;
using AntropoPollWebApi.Core.RequestModel;
using AntropoPollWebApi.Core.ResponseModel;

namespace AntropoPollWebApi.Core.Interfaces
{
    public interface IUserService
    {
        UserClaims GetUserClaims(ClaimsPrincipal user);
        UserView UpdateUser(Guid userId, AddOrUpdateUserRequest request);
        IEnumerable<UserView> GetUserList();
        UserView AddUser(AddOrUpdateUserRequest request);
        UserView GetUserToken(Guid userId);
    } 
}
