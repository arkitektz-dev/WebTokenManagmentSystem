using System.Collections.Generic;
using WebAppQueueManagmentSystem.ApiHelpers.Response;

namespace WebAppQueueManagmentSystem.BLL.Counter
{
    public interface ICounterRepository
    {
        CounterTokenBody AssignTokenToCounter(string TokenNumber, string UserId, int StatusId);
        CounterDetailBody CounterDetail(string UserID);
        Models.Token GetLastPendingTicket(string UserId);
        IList<ApiHelpers.Response.Counter> ListCounter();
    }
}