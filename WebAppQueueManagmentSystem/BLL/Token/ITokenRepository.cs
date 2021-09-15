using System;
using System.Collections;
using System.Collections.Generic;
using WebAppQueueManagmentSystem.ApiHelpers.Request;
using WebAppQueueManagmentSystem.ApiHelpers.Response;
using WebAppQueueManagmentSystem.Models;

namespace WebAppQueueManagmentSystem.BLL.Token
{
    public interface ITokenRepository
    {
        GenerateTokenBody GenerateTicket(string CustomerType);
        Auth GenerateToken(); 
        TokenStatusBody GetTokenStatus(string TokenNumber);
        IList<ListCounterTokenBody> ListCounterToken();
        IList<CounterListBody> ListToken(int token_status, int customer_Type);
        SubmittedTicketBody Submitted_Token(string TokenNumber, string Comment, int ServiceOptionId, byte StatusId);
        IList<CounterTokenBody> ViewCounterActivity(int counterId);
        IList<StatusListBody> StatusList();
        IList<CurrentCounterTokenDto> CurrentList(DateTime TicketDate, int TicketStatus, int CustomerType);
        StatusChangeBody ChangeTokenStatus(string TokenNumber, byte Status);
        int GetAverageTime();
        AddTicketToQueueBody InsertAnncoumentInQueue(int CounterId, string TokenNumber);
        List<CounterListResponse> GetCounterList();
        MaintainCounterHistoryResponse ControlCounter(int CounterId, string UserId);
        QueueCardBody GetTicketStatuses();
        ChartBody GetAllChartValues();
        IList<CounterValueBody> GetCountTicketByCounter();
        HoldTokenBody HoldTicket(string TokenNumber);
        IList<ListTokenBody> ListHoldTicket(string CounterId);
      



    }
}