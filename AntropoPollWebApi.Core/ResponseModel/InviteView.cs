using System;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class InviteView : BaseView
    {
        public Guid EventId { get; set; }

        public Guid? ResultId { get; set; }
    }
}
