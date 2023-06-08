using AntropoPollWebApi.Core.Interfaces;
using AntropoPollWebApi.Core.RequestModel;
using AntropoPollWebApi.Core.ResponseModel;
using AntropoPollWebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AntropoPollWebApi.Core.Models;

namespace AntropoPollWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly IPoll _pool;
        private readonly IUserService _userService;

        public PollController(IPoll pool, IUserService userService)
        {
            _pool = pool;
            _userService = userService;
        }

        /// <summary>
        /// Возвращает текущего пользователя определеного по токену 
        /// </summary>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/Poll/Whoami")]
        public async Task<UserClaims> Whoami()
        {
            var userClaims = _userService.GetUserClaims(HttpContext.User);

            if (userClaims == null)
                throw new UnauthorizedAccessException();

            return userClaims;
        }

        /// <summary>
        /// Получить список схем (тестов)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Poll/GetSchemaList")]
        public IEnumerable<SchemaView> GetSchemeList(bool? includeArchive)
        {
            return _pool.GetSchemeList(includeArchive);
        }

        /// <summary>
        /// Получить детали для схемы (теста)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [NotFoundResultFilter]
        [Route("/Poll/GetSchemaDetails")]
        public SchemaView GetSchemaDetails(Guid id)
        {
            return _pool.GetSchemaDetails(id);
        }

        /// <summary>
        /// Добавить схему (тест)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Poll/AddSchema")]
        public SchemaView AddSchema([FromBody] AddOrUpdateSchemaRequest request)
        {
            if (ModelState.IsValid)
                return _pool.AddSchema(request);
            return null;
        }

        /// <summary>
        /// Обновить схему
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/UpdateSchema")]
        public SchemaView UpdateSchema([FromQuery] Guid schemaId, [FromBody] AddOrUpdateSchemaRequest request)
        {
            if (ModelState.IsValid)
                return _pool.UpdateSchema(schemaId, request);
            return null;
        }

        /// <summary>
        /// Удалить схему если у нее нет результатов тестирования, или переносит в архив
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/RemoveSchema")]
        public ActionResult RemoveSchema(Guid schemaId)
        {
            _pool.RemoveSchema(schemaId);
            return new OkResult();
        }

        /// <summary>
        /// Получить список мероприятий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Poll/GetEventList")]
        public IEnumerable<EventView> GetEventList(Guid? userId)
        {
            return _pool.GetEventList(userId);
        }

        /// <summary>
        /// Получить детали для мероприятия
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [NotFoundResultFilter]
        [Route("/Poll/GetEventDetails")]
        public EventView GetEventDetails(Guid id)
        {
            return _pool.GetEventDetails(id);
        }

        /// <summary>
        /// Добавить мероприятие
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Poll/AddEvent")]
        public EventView AddEvent([FromBody] AddEventRequest request)
        {
            if (ModelState.IsValid)
                return _pool.AddEvent(request);
            return null;
        }

        /// <summary>
        /// TODO Удалить мероприятие
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/RemoveEvent")]
        public ActionResult RemoveEvent(Guid guid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить список переменных
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Poll/GetSchemaVariableList")]
        public IEnumerable<SchemaVariableView> GetSchemaVariableList(Guid? schemeId)
        {
            // _logger.LogDebug($"Enter {nameof(PollController)}.{nameof(GetQuestionList)}");

            return _pool.GetSchemaVariableList(schemeId);
        }

        /// <summary>
        /// Добавить переменную
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Poll/AddSchemaVariable")]
        public SchemaVariableView AddSchemaVariable([FromBody] AddSchemaVariableRequest request)
        {
            // _logger.LogDebug($"Enter {nameof(PollController)}.{nameof(GetQuestionList)}");
            if (ModelState.IsValid)
                return _pool.AddSchemaVariableRequest(request);
            return null;
        }
        /// <summary>
        /// TODO Удалить переменную
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/RemoveSchemaVariable")]
        public ActionResult RemoveSchemaVariable(Guid guid)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Получить список вопросов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Poll/GetQuestionList")]
        public IEnumerable<QuestionView> GetQuestionList(Guid? schemeId, bool includeArchive = false)
        {
            return _pool.GetQuestionList(schemeId, includeArchive);
        }

        /// <summary>
        /// Получить список вопросов по инвайту
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [NotFoundResultFilter]
        [Route("/Poll/GetQuestionListByInvite")]
        public IEnumerable<QuestionInviteView> GetQuestionListByInvite(Guid inviteId)
        {
            return _pool.GetQuestionListByInvite(inviteId);
        }

        /// <summary>
        /// Добавить вопрос
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Poll/AddQuestion")]
        public QuestionView AddQuestion([FromBody] AddQuestionRequest addQuestionRequest)
        {
            // _logger.LogDebug($"Enter {nameof(PollController)}.{nameof(GetQuestionList)}");
            if (ModelState.IsValid)
                return _pool.AddQuestion(addQuestionRequest);
            return null;
        }
        /// <summary>
        /// Удалить вопрос
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/RemoveQuestion")]
        public async Task<int> RemoveQuestion(Guid questionId)
        {
            return await _pool.RemoveQuestion(questionId);
        }

        /// <summary>
        /// Обновить вопрос
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/UpdateQuestion")]
        public IActionResult UpdateQuestion([FromQuery] Guid questionId, [FromBody] UpdateQuestionRequest request)
        {
            if (ModelState.IsValid)
                if (_pool.UpdateQuestion(questionId, request))
                    return Ok();
            return new BadRequestResult();
        }

        /// <summary>
        /// Обновить ответ у вопроса
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/UpdateAnswer")]
        public IActionResult UpdateAnswer([FromQuery] Guid answerId, [FromBody] AddClosedQuestionAnswerRequest request)
        {
            if (ModelState.IsValid)
                if (_pool.UpdateAnswer(answerId, request))
                    return Ok();
            return new BadRequestResult();
        }

        /// <summary>
        /// Получить список интерпретаций
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [NotFoundResultFilter]
        [Route("/Poll/GetInterpretationList")]
        public async Task<IEnumerable<InterpretationView>> GetInterpretationList(Guid? schemeId, bool? includeArchive)
        {
            return await _pool.GetInterpretationList(schemeId, includeArchive);
        }

        /// <summary>
        /// Добавить интерпретацию
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Poll/AddInterpretation")]
        public async Task<InterpretationView> AddInterpretation([FromBody] AddOrUpdateInterpretationRequest request)
        {
            if (ModelState.IsValid)
                return await _pool.AddInterpretation(request);
            return null;
        }
        /// <summary>
        /// Удалить интерпретацию
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/RemoveInterpretation")]
        public async Task RemoveInterpretation(Guid interpretationId)
        {
            await _pool.RemoveInterpretation(interpretationId);
        }

        /// <summary>
        /// Обновить интерпретацию
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/Poll/UpdateInterpretation")]
        public async Task<InterpretationView> UpdateInterpretationAsync([FromQuery] Guid interpretationId, [FromBody] AddOrUpdateInterpretationRequest request)
        {
            return await _pool.UpdateInterpretation(interpretationId, request);
        }

        /// <summary>
        /// Получить список шаблонов для отчета
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [NotFoundResultFilter]
        [Route("/Poll/GetReportTemplateList")]
        public async Task<IEnumerable<ReportTemplateView>> GetReportTemplateList(Guid? schemeId)
        {
            return await _pool.GetReportTemplateList(schemeId);
        }

        /// <summary>
        /// Добавить шаблон для отчета
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Poll/AddReportTemplate")]
        public async Task<ReportTemplateView> AddReportTemplate([FromBody] AddOrUpdateReportTemplateRequest request)
        {
            if (ModelState.IsValid)
                return await _pool.AddReportTemplate(request);
            return null;
        }
        /// <summary>
        /// TODO Удалить шаблон для отчета
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/RemoveReportTemplate")]
        public ActionResult RemoveReportTemplate(Guid guid)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Добавить результат теста
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("/Poll/AddResult")]
        public ResultView AddResult([FromBody] AddResultRequest request)
        {
            if (ModelState.IsValid)
                return _pool.AddResult(request);
            return null;
        }


        /// <summary>
        /// Получить список результатов тестов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Poll/GetResultList")]
        public IEnumerable<ResultView> GetResultList([FromQuery] Guid? eventId)
        {
            return _pool.GetResultList(eventId);
        }

        /// <summary>
        /// Получить детали результата теста
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/GetResultDetails")]
        public ResultDetailsView GetResultDetails(Guid resultId)
        {
            return _pool.GetResultDetails(resultId);
        }

        /// <summary>
        /// Изменить статус отчета для перевычисления
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/ResetResultReport")]
        public void ResetResultReport(Guid resultId)
        {
            _pool.ReEvalResult(resultId);
        }

        /// <summary>
        /// Обновить данные результата теста
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/UpdateDataResultReport")]
        public ResultDetailsView UpdateDataResultReport([FromQuery] Guid resultId, [FromBody] UpdateDataReportRequest request)
        {
            return _pool.UpdateDataResultReport(resultId, request);
        }

        /// <summary>
        /// Завершить редактирование результата теста
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/CompletedResultReport")]
        public ResultDetailsView CompletedResultReport([FromQuery] Guid resultId)
        {
            return _pool.CompletedResultReport(resultId);
        }

        /// <summary>
        /// Получить детали шаблона результата теста
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/Poll/GetResultTemplateDetails")]
        public ResultTemplateView GetResultTemplateDetails(Guid resultTemplateId)
        {
            return _pool.GetResultTemplateDetails(resultTemplateId);
        }

        /// <summary>
        /// Получить токен для пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/Poll/GetUserToken")]
        public UserView GetUserToken(Guid userId)
        {
            return _userService.GetUserToken(userId);
        }


        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/Poll/AddUser")]
        public UserView AddUser([FromBody] AddOrUpdateUserRequest request)
        {
            return _userService.AddUser(request);
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/Poll/UpdateUser")]
        public UserView UpdateUser([FromQuery] Guid userId, [FromBody] AddOrUpdateUserRequest request)
        {
            return _userService.UpdateUser(userId, request);
        }
        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/Poll/GetUserList")]
        public IEnumerable<UserView> GetUserList()
        {
            return _userService.GetUserList();
        }

        /// <summary>
        /// Добавить инвайты для пользователей
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/Poll/AddInvites")]
        public List<InviteView> AddInvites([FromBody] AddInviteClientsRequest request)
        {
            return _pool.AddInvites(request);
        }


        /// <summary>
        /// Получить инвайты для пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [NotFoundResultFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/Poll/GetInviteList")]
        public List<InviteView> GetInviteList(Guid eventId)
        {
            return _pool.GetInviteList(eventId);
        }
    }
}