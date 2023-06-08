using System;

namespace AntropoPollWebApi.Core.RequestModel
{
    public class AddInviteClientsRequest
    {
        public Guid EventId { get; set; }
        public int InviteCount { get; set; }
    }
}
