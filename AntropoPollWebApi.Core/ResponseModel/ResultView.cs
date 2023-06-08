using AntropoPollWebApi.Core.Models;
using System;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class ResultView : BaseView
    {
        public ResultStatus Status { get; set; }
        public Guid InviteId { get; set; }
        public Guid EventId { get; set; }
        //public virtual IQueryable<ResultQuestion> ResultQuestions { get; set; }
        public object FormAnalytics { get; set; }
    }
}
