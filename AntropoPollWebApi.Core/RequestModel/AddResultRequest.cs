using System;
using System.Collections.Generic;

namespace AntropoPollWebApi.Core.RequestModel
{
    public class AddResultRequest
    {
        public Guid InviteId { get; set; }
        public IList<AddResultQuestion> AddResultQuestion { get; set; }
        public Object FormAnalytics { get; set; }
    }
}
