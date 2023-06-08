namespace AntropoPollWebApi.Core.RequestModel
{
    public class AddClosedQuestionAnswerRequest
    {
        /// <summary>
        /// Номер по порядку для сортировки
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Текст ответа
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Значение переменной для отчета
        /// </summary>
        public decimal? VariableValue { get; set; }
    }
}
