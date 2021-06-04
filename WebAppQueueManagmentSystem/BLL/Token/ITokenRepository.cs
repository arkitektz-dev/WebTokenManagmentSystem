using System.Collections;
using System.Collections.Generic;
using WebAppQueueManagmentSystem.ApiHelpers.Response;
using WebAppQueueManagmentSystem.Models;

namespace WebAppQueueManagmentSystem.BLL.Token
{
    public interface ITokenRepository
    {
        GenerateTokenBody GenerateTicket(string CustomerType);
        Auth GenerateToken();
        IList<ListCounterTokenBody> ListCounterToken();
        IList<CounterListBody> ListToken(int token_status, int customer_Type);
        SubmittedTicketBody Submitted_Token(string TokenNumber, string Comment, int ServiceOptionId, byte StatusId);
    }
}