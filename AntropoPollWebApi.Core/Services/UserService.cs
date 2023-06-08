using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AntropoPollWebApi.Core.Contexts;
using AntropoPollWebApi.Core.Interfaces;
using AntropoPollWebApi.Core.Models;
using AntropoPollWebApi.Core.RequestModel;
using AntropoPollWebApi.Core.ResponseModel;
using AntropoPollWebApi.Core.Settings;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AntropoPollWebApi.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private AntropoPollSettings _options;

        public UserService(IMapper mapper, IOptions<AntropoPollSettings> options, TokenService tokenService)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _options = options.Value;
        }

        public UserView UpdateUser(Guid userId, AddOrUpdateUserRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var user = context.Users.FirstOrDefault(x => x.Guid == userId);
                if (user == null)
                    return null;

                var userUpdate = _mapper.Map(request, user);
                user.LastUpdate = DateTime.UtcNow;

                context.Users.Update(userUpdate);
                context.SaveChanges();

                var userView = _mapper.Map<UserView>(userUpdate);
                if (userView.IsActive)
                {
                    userView.AccessToken = _tokenService.GetAccessToken(new UserClaims()
                    {
                        Guid = user.Guid,
                        IsModerator = user.IsModerator,
                        IsSuperUser = user.IsSuperUser
                    });
                }
                return userView;
            }
        }

        public UserClaims GetUserClaims(ClaimsPrincipal user)
        {
            return _tokenService.GetUserClaims(user);
        }

        public IEnumerable<UserView> GetUserList()
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var users = context.Users.ToList();
                var usersView = _mapper.Map<List<UserView>>(users);
                return usersView;
            }
        }

        public UserView AddUser(AddOrUpdateUserRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var user = _mapper.Map<User>(request);
                user.LastUpdate = DateTime.UtcNow; ;

                context.Users.Add(user);
                context.SaveChanges();

                var userView = _mapper.Map<UserView>(user);
                if (userView.IsActive)
                    userView.AccessToken = GetAccessToken(user);

                return userView;
            }
        }

        public UserView GetUserToken(Guid userId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var user = context.Users.FirstOrDefault(x => x.Guid == userId);
                if (user == null)
                    return null;

                var userView = _mapper.Map<UserView>(user);
                if (userView.IsActive)
                    userView.AccessToken = GetAccessToken(user);

                return userView;
            }
        }

        private string GetAccessToken(User user)
        {
            return _tokenService.GetAccessToken(new UserClaims()
            {
                Guid = user.Guid,
                IsModerator = user.IsModerator,
                IsSuperUser = user.IsSuperUser
            });
        }
    }
}
