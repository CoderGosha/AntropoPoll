using System.Collections.Generic;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class ResultDetailsView : ResultView
    {
        /// <summary>
        /// Относительная ссылка на отчет
        /// </summary>
        public string PathReport { get; set; }

        public List<ResultQuestionView> ResultQuestions { get; set; }

        public List<SystemVariableReportView> SystemVariableReportView { get; set; }

        public List<InterpretationView> InterpretationView { get; set; }
        /// <summary>
        /// Данные отчетов
        /// </summary>
        public IList<ResultTemplateView> ResultTemplateViews { get; set; }
    }
}
