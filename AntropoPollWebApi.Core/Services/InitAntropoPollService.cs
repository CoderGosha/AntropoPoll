using AntropoPollWebApi.Core.Contexts;
using AntropoPollWebApi.Core.Models;
using AntropoPollWebApi.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;

namespace AntropoPollWebApi.Core.Services
{
    public class InitAntropoPollService
    {
        private AntropoPollSettings _options;
        private readonly ILogger _logger;
        private readonly TokenService _tokenService;

        public InitAntropoPollService(IOptions<AntropoPollSettings> options, ILogger<InitAntropoPollService> logger, TokenService tokenService)
        {
            _options = options.Value;
            _logger = logger;
            _tokenService = tokenService;
        }

        public void Init()
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                try
                {
                    context.Database.Migrate();


                    //Создадим нулевого пациента 
                    var superUser = context.Users.FirstOrDefault(x => x.IsSuperUser);
                    if (superUser == null)
                        throw new DbUpdateException("Super user not fount");

                    var accessToken = _tokenService.GetAccessToken(new UserClaims()
                    {
                        Guid = superUser.Guid,
                        IsModerator = superUser.IsModerator,
                        IsSuperUser = superUser.IsSuperUser
                    });

                    //Сохраним в файл
                    string path = "token_" + superUser.Guid.ToString();
                    File.WriteAllText(path, accessToken);

                }
                catch (TimeoutException ex)
                {
                    _logger.LogError(message: "Migrate error", exception: ex);
                }

            }
        }
    }
}
