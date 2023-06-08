using System.Collections.Generic;

namespace AntropoPollWebApi.Core.Models
{
    public class Schema : BaseModel
    {
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        public int StanValue { get; set; }

        public bool IsActive { get; set; }

        public virtual IList<BaseQuestion> BaseQuestions { get; set; }

        public virtual IList<SchemaVariable> SchemaVariables { get; set; }
    }
}
